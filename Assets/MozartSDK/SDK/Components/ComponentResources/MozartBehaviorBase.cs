namespace Mozart
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// This class offers automatic access to the MozartManager from anywhere in the program.
    /// Extend this class if you want to make your own components that use data from the manager.
    /// You can access the manager using GetManager()
    /// </summary>
    public class MozartBehaviorBase : MonoBehaviour
    {
        private MozartManager manager;
        /// <summary>
        /// Try every possible way imaginable to find the MozartManager in the scene.
        /// Some development teams do not like Singletons due to tight coupling problems, so this is a replacement
        /// for just calling MozartManager.instance all over the codebase.  If you want to replace MozartManager
        /// with a custom manager you can change how GetManager works here and implement an interface instead.
        /// </summary>
        /// <returns></returns>
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
                if(manager == null && findManager != null)
                {
                    manager = findManager.GetComponent<MozartManager>();
                }
                if(manager == null)
                {
                    Debug.LogError("MozartManager is not added to the scene, Mozart components will not function until one is found, please add the MozartManager prefab to your scene");
                }
            }
            return manager;
        }
    }
}