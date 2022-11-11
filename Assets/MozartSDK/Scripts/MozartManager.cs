namespace Mozart
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MozartManager : MonoBehaviour
    {
        public static MozartManager instance;
        // Start is called before the first frame update
        void Start()
        {
            if (!instance) instance = this;
            Object.DontDestroyOnLoad(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
