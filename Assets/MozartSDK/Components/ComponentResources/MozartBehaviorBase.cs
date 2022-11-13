namespace Mozart
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MozartBehaviorBase : MonoBehaviour
    {
        private MozartManager manager;
        protected MozartManager GetManager()
        {
            if (manager == null) manager = MozartManager.instance;
            if (manager == null)
            {
                GameObject findManager = GameObject.Find("MozartManager");
                if(findManager == null)
                {
                    manager = Component.FindObjectOfType<MozartManager>();
                }
                else
                {
                    manager = findManager.GetComponent<MozartManager>();
                }
                if(manager == null)
                {
                    Debug.LogError("MozartManager is not added to the scene, Mozart components will not function until one is found");
                }
            }
            return manager;
        }
    }
}