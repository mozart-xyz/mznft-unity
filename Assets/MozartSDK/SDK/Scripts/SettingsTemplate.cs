namespace Mozart
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Settings", menuName = "MozartSDK/CreateSettings", order = 1)]
    public class SettingsTemplate : ScriptableObject
    {
        public string APIPublicKey;
        public string GameIdentifier;
        public string GameCurrencyIdentifier;
        public bool logging = false;
        public string dashboardUrl = "https://staging-dashboard-4we0.onrender.com";
        public string apiBaseUrl = "https://staging-api-ij1y.onrender.com";
        public void Log(string message)
        {
            if (logging) Debug.Log(message);
        }

        public void Warn(string message)
        {
            if (logging) Debug.LogWarning(message);
        }

        public void Error(string message)
        {
            if (logging) Debug.LogError(message);
        }
    }
}