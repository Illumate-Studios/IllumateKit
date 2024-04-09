using UnityEngine;

namespace Illumate.Kit
{
    /// <summary>
    /// General data holder
    /// </summary>
    [CreateAssetMenu(fileName = resourcesName, menuName = "Illumate/Config")]
    public class IllumateConfigSC : ScriptableObject
    {
        #region Singleton
        private const string resourcesName = "IllumateConfig";
        private static IllumateConfigSC instance;
        internal static IllumateConfigSC Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<IllumateConfigSC>(resourcesName);
                    Debug.Assert(instance != null, $"{nameof(IllumateConfigSC)} not found in Resources by {resourcesName} name");
                }
                return instance;
            }
        }
        #endregion

        public IllumateKitSingleton kitSingletonPrefab;

        [SerializeField] internal API.IllumateApiSettings apiSettings;
        [SerializeField] internal RuntimeDebugPanel.RDPPanelsSettings rdpSettings;
        [SerializeField] internal ReporterSettings reporterSettings;
    }
}
