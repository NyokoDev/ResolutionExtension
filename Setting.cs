using Colossal;
using Colossal.IO.AssetDatabase;
using Colossal.IO.AssetDatabase.Internal;
using Game.Modding;
using Game.Settings;
using Game.UI;
using Game.UI.Localization;
using Game.UI.Widgets;
using Resolution_Extension;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ResolutionExtension
{
    [FileLocation(nameof(ResolutionExtension))]
    [SettingsUIGroupOrder(kButtonGroup, kToggleGroup, kSliderGroup, kDropdownGroup)]
    [SettingsUIShowGroupName(kButtonGroup, kToggleGroup, kSliderGroup, kDropdownGroup)]
    public class Setting : ModSetting
    {
        private const string kSection = "Main";
        private const string kButtonGroup = "Button";
        private const string kToggleGroup = "Toggle";
        private const string kSliderGroup = "Slider";
        private const string kDropdownGroup = "Resolution Extension";
        private int _resolutionRatioHeight;
        private int _resolutionRatioWidth;

        public Setting(IMod mod) : base(mod) { }



        [SettingsUISection(kSection, kDropdownGroup)]
        [SettingsUISlider(min = 1000f, max = 9000f, step = 1f, unit = "integer", scaleDragVolume = true)]
        public int ResolutionRatioHeight
        {
            get { return GlobalVariables.Instance.ResolutionHeight; }
            set
            {
                GlobalVariables.Instance.ResolutionHeight = value;
                SaveToFileIn();
            }
        }

        [SettingsUISection(kSection, kDropdownGroup)]
        [SettingsUISlider(min = 1000f, max = 9000f, step = 1f, unit = "integer", scaleDragVolume = true)]
        public int ResolutionRatioWidth
        {
            get { return GlobalVariables.Instance.ResolutionWidth; }
            set
            {
                GlobalVariables.Instance.ResolutionWidth = value;
                SaveToFileIn();
            }
        }

        public static void SaveToFileIn()
        {
            string localLowDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            localLowDirectory = Path.Combine(localLowDirectory, "..", "LocalLow");
            string assemblyDirectory = Path.Combine(localLowDirectory, "Colossal Order", "Cities Skylines II", "Mods", "Resolution Extension");
            Directory.CreateDirectory(assemblyDirectory);
            string settingsFilePath = Path.Combine(assemblyDirectory, "ResolutionExtension.xml");
            GlobalVariables.SaveToFile(settingsFilePath);
        }


        [SettingsUIButton]
        [SettingsUISection(kSection, kDropdownGroup)]
        public bool Button
        {
            set
            {
                ApplyRes();

            }
        }



        public static int[] GetScreenResolutionValues()
        {
            int[] resolutions = new int[2]; // Assuming there are 2 resolutions initially

            // Adding example screen resolutions
            resolutions[0] = 2560; // First resolution
            resolutions[1] = 1920; // Second resolution
                                   // Add more resolutions as needed...

            return resolutions;
        }


     

        public static ScreenResolution ScreenResolution = new ScreenResolution();

        public void ApplyRes()
        {
            ScreenResolution.width = ResolutionRatioWidth;
            ScreenResolution.height = ResolutionRatioHeight;

            RefreshRate refreshRate = new RefreshRate { numerator = 60, denominator = 1 };



            // Apply the resolution
            Screen.SetResolution(ScreenResolution.width, ScreenResolution.height, FullScreenMode.Windowed, refreshRate);
        }
       
        public override void Apply()
        {
            base.Apply();

            
        }


        public override void SetDefaults()
        {
            // Set default values if needed
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
                    { m_Setting.GetSettingsLocaleID(), "Resolution Extension" },
                    { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Main" },

                    { m_Setting.GetOptionGroupLocaleID(Setting.kButtonGroup), "Buttons" },
                    { m_Setting.GetOptionGroupLocaleID(Setting.kToggleGroup), "Toggle" },
                    { m_Setting.GetOptionGroupLocaleID(Setting.kSliderGroup), "Sliders" },
                    { m_Setting.GetOptionGroupLocaleID(Setting.kDropdownGroup), "Resolution Extension" },

                    { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResolutionRatioHeight)), "Resolution Height" },
                    { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResolutionRatioWidth)), "Resolution Width" },
                    { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResolutionRatioHeight)), "Resolution Height: Adjust the vertical resolution" },
                    { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResolutionRatioWidth)), "Resolution Width: Adjust the horizontal resolution" },

                    { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button)), "Apply" },








                };
            }

            public void Unload()
            {
                // Cleanup resources if needed
            }
        }
    }
}