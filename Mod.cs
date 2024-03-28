using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.SceneFlow;
using ResolutionExtension;
using System.IO;
using System;
using static ResolutionExtension.Setting;

namespace Resolution_Extension
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(Resolution_Extension)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        private Setting m_Setting;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Mod location at {asset.path}");

            m_Setting = new Setting(this);
            m_Setting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));

            AssetDatabase.global.LoadSettings(nameof(Resolution_Extension), m_Setting, new Setting(this));

            string localLowDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            localLowDirectory = Path.Combine(localLowDirectory, "..", "LocalLow");
            string assemblyDirectory = Path.Combine(localLowDirectory, "Colossal Order", "Cities Skylines II", "Mods", "ColorAdjustments");
            string settingsFilePath = Path.Combine(assemblyDirectory, "ResolutionExtension.xml");
            GlobalVariables.LoadFromFile(settingsFilePath);
            GlobalVariables.CheckForIntegrity();
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }
    }
}
