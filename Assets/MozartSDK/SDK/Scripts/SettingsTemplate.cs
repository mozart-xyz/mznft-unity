namespace Mozart
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Settings", menuName = "MozartSDK/CreateSettings", order = 1)]
    public class SettingsTemplate : ScriptableObject
    {
        public string APIPublicKey;
    }
    //Selection.activeObject=AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/"+prefabName+".prefab");

}