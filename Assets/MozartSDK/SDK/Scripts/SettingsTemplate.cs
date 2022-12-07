namespace Mozart
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Settings", menuName = "MozartSDK/CreateSettings", order = 1)]
    public class SettingsTemplate : ScriptableObject
    {
        public string APIPublicKey;
        public string GameIdentifier;
        public string apiBaseUrl = "https://staging-api-ij1y.onrender.com";
    }
    //Selection.activeObject=AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/"+prefabName+".prefab");

}