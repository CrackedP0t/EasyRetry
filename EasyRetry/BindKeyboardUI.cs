using Celeste;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;
using System.Collections.Generic;

namespace Celeste.Mod.EasyRetry
{
    public class BindKeyboardUI : Entity
    {
        private RetrySettings settings;

        private TextMenu menu;

        private TextMenu.Setting setting;

        public BindKeyboardUI(RetrySettings nsettings, TextMenu nmenu, TextMenu.Setting nsetting)
        {
            settings = nsettings;
            menu = nmenu;
            setting = nsetting;

            base.Tag = ((int)Tags.PauseUpdate | (int)Tags.HUD);
        }

        public override void Update()
        {
            Keys[] pressedKeys = MInput.Keyboard.CurrentState.GetPressedKeys();
            if (pressedKeys != null && pressedKeys.Length != 0 && MInput.Keyboard.Pressed(pressedKeys[pressedKeys.Length - 1]))
            {
                var key = pressedKeys[pressedKeys.Length - 1];

                settings.KeyboardBinding = key;

                setting.Set(new List<Keys> { key });

                menu.Focused = true;

                RemoveSelf();
            }
        }

        public override void Render()
        {
            base.Render();
            Vector2 vector = new Vector2(1920f, 1080f) * 0.5f;

            Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * 0.95f);
            ActiveFont.Draw(Dialog.Get("KEY_CONFIG_CHANGING"), vector + new Vector2(0f, -8f), new Vector2(0.5f, 1f), Vector2.One * 0.7f, Color.LightGray * 0.95f);
            ActiveFont.Draw("RESET", vector + new Vector2(0f, 8f), new Vector2(0.5f, 0f), Vector2.One * 2f, Color.White * 0.95f);
        }
    }
}
