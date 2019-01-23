using Celeste;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;
using System.Collections.Generic;

namespace Celeste.Mod.EasyRetry
{
    public class BindButtonUI : Entity
    {
        private RetrySettings settings;

        private TextMenu menu;

        private TextMenu.Setting setting;

        private readonly List<Buttons> all = new List<Buttons>
        {
            Buttons.A,
            Buttons.B,
            Buttons.X,
            Buttons.Y,
            Buttons.LeftShoulder,
            Buttons.RightShoulder,
            Buttons.LeftTrigger,
            Buttons.RightTrigger
        };

        public BindButtonUI(RetrySettings nsettings, TextMenu nmenu, TextMenu.Setting nsetting)
        {
            settings = nsettings;
            menu = nmenu;
            setting = nsetting;

            base.Tag = ((int)Tags.PauseUpdate | (int)Tags.HUD);
        }

        public override void Update()
        {
            base.Update();

            GamePadState currentState = MInput.GamePads[Input.Gamepad].CurrentState;
            GamePadState previousState = MInput.GamePads[Input.Gamepad].PreviousState;
            foreach (Buttons item in all)
            {
                if (currentState.IsButtonDown(item) && !previousState.IsButtonDown(item))
                {
                    settings.ButtonBinding = item;

                    setting.Set(new List<Buttons> { item });

                    menu.Focused = true;

                    RemoveSelf();

                    break;
                }
            }
        }

        public override void Render()
        {
            base.Render();
            Vector2 vector = new Vector2(1920f, 1080f) * 0.5f;

            Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * 0.95f);
            ActiveFont.Draw(Dialog.Get("BTN_CONFIG_CHANGING"), vector + new Vector2(0f, -8f), new Vector2(0.5f, 1f), Vector2.One * 0.7f, Color.LightGray * 0.95f);
            ActiveFont.Draw("RESET", vector + new Vector2(0f, 8f), new Vector2(0.5f, 0f), Vector2.One * 2f, Color.White * 0.95f);
        }
    }
}
