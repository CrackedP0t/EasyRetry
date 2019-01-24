using System;
using Celeste.Mod;
using Celeste;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste.Mod.EasyRetry
{
    public class EasyRetry : EverestModule
    {
        public static EasyRetry Instance;

        public Keys OldKey;
        public Buttons OldButton;

        public VirtualButton Button;

        public EasyRetry()
        {
            Instance = this;
        }

        // If you don't need to store any settings, => null
        public override Type SettingsType => typeof(RetrySettings);
        public static RetrySettings Settings => (RetrySettings)Instance._Settings;

        // If you don't need to store any save data, => null
        public override Type SaveDataType => null; //typeof(ExampleSaveData);
        //public static ExampleSaveData SaveData => (ExampleSaveData)Instance._SaveData;

        // Set up any hooks, event handlers and your mod in general here.
        // Load runs before Celeste itself has initialized properly.
        public override void Load()
        {
            On.Celeste.Level.Update += (orig, self) =>
            {
                if (Settings.Enabled)
                {
                    if (Settings.ButtonBinding != OldButton || Settings.KeyboardBinding != OldKey)
                    {
                        Button.Nodes.Clear();

                        Button.Nodes.Add(new VirtualButton.PadButton(Input.Gamepad, Settings.ButtonBinding));
                        Button.Nodes.Add(new VirtualButton.KeyboardKey(Settings.KeyboardBinding));
                    }
                    if (Button.Check && !self.Paused)
                    {
                        var player = self.Tracker.GetEntity<Player>();
                        if (player != null)
                        {
                            if (player != null && !player.Dead && !self.InCutscene && !self.SkippingCutscene && player.CanRetry)
                            {
                                Engine.TimeRate = 1f;
                                Distort.GameRate = 1f;
                                Distort.Anxiety = 0f;

                                self.InCutscene = (self.SkippingCutscene = false);
                                player.Die(Vector2.Zero, evenIfInvincible: true);
                                foreach (LevelEndingHook component in self.Tracker.GetComponents<LevelEndingHook>())
                                {
                                    if (component.OnEnd != null)
                                    {
                                        component.OnEnd();
                                    }
                                }
                            }
                        }
                    }

                }


                orig(self);
            };
        }

        // Optional, initialize anything after Celeste has initialized itself properly.
        public override void Initialize()
        {
            Button = new VirtualButton(0.08f);
        }

        // Unload the entirety of your mod's content, remove any event listeners and undo all hooks.
        public override void Unload()
        {
        }
    }
}