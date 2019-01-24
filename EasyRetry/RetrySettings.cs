using Celeste;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste.Mod.EasyRetry
{
    [SettingName("modoptions_easyretry_title")]
    public class RetrySettings : EverestModuleSettings
    {
        public bool Enabled {get; set;} = true;

        public Buttons ButtonBinding {get; set;} = Buttons.Y;

        public void CreateButtonBindingEntry(TextMenu menu, bool inGame) {
            var setting = new TextMenu.Setting("Button Binding", ButtonBinding);
            setting.Pressed(() => {
                menu.Focused = false;
                Engine.Scene.Add(new BindButtonUI(this, menu, setting));
            });
            menu.Add(setting);
        }

        public Keys KeyboardBinding {get; set;} = Keys.K;

        public void CreateKeyboardBindingEntry(TextMenu menu, bool inGame) {
            var setting = new TextMenu.Setting("Keyboard Binding", KeyboardBinding);
            setting.Pressed(() => {
                menu.Focused = false;
                Engine.Scene.Add(new BindKeyboardUI(this, menu, setting));
            });
            menu.Add(setting);
        }
    }
}