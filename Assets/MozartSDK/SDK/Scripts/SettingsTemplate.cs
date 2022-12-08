namespace Mozart
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Settings", menuName = "MozartSDK/CreateSettings", order = 1)]
    public class SettingsTemplate : ScriptableObject
    {
        public string APIPublicKey;
        public string GameIdentifier;
        public bool logging = false;
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