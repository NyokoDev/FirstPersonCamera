using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using Game.UI.Widgets;
using System;
using System.Collections.Generic;

namespace FirstPersonCamera
{
    [FileLocation(nameof(FirstPersonCamera))]
    [SettingsUIGroupOrder(kButtonGroup, kToggleGroup, kSliderGroup, kDropdownGroup)]
    [SettingsUIShowGroupName(kButtonGroup, kToggleGroup, kSliderGroup, kDropdownGroup)]
    public class Setting : ModSetting
    {
        public const string kSection = "Main";

        public const string kButtonGroup = "Button";
        public const string kToggleGroup = "Toggle";
        public const string kSliderGroup = "Slider";
        public const string kDropdownGroup = "Dropdown";

        public Setting(IMod mod) : base(mod)
        {

        }

        [SettingsUIButton]
        [SettingsUISection(kSection, kToggleGroup)]
        public bool Support
        {
            set
            {
                OpenDiscordInvite();

            }
        }

        public void OpenDiscordInvite()
        {
            try
            {
                string discordInviteLink = "https://discord.gg/5JcaKwDBHn";

                // Use Process.Start to open the URL in the default web browser
                System.Diagnostics.Process.Start(discordInviteLink);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public override void SetDefaults()
        {

        }

        public class LocaleEN : IDictionarySource
        {
            private readonly Setting m_Setting;
            public LocaleEN(Setting setting)
            {
                m_Setting = setting;
            }
            public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
            {
                return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "First Person Camera" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Main" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kButtonGroup), "Buttons" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kToggleGroup), "Toggle" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kSliderGroup), "Sliders" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kDropdownGroup), "Dropdowns" },

                  { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Support)), "Support" },
       { m_Setting.GetOptionDescLocaleID(nameof(Setting.Support)), "Opens Support discord server in new tab." },



            };
            }

            public void Unload()
            {

            }
        }
    }
}

