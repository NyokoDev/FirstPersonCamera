using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.Rendering.CinematicCamera;
using Game.Rendering;
using Game.SceneFlow;
using Game.UI.InGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using FirstPersonCamera;
using static FirstPersonCamera.Setting;

namespace FirstPersonCamera
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(FirstPersonCamera)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        private Setting m_Setting;
        private Harmony? _harmony;

        public void OnLoad(UpdateSystem updateSystem)
        {

            _harmony = new($"{nameof(FirstPersonCamera)}.{nameof(Mod)}");
            _harmony.PatchAll(typeof(Mod).Assembly);
            Mod.log.Info("Ran PatchAll.");
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Mod asset location {asset.path}");

            m_Setting = new Setting(this);
            m_Setting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));

            _harmony?.UnpatchAll($"{nameof(FirstPersonCamera)}.{nameof(Mod)}");

            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }
    }
}

   