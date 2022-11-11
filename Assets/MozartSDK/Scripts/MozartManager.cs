namespace Mozart
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MozartManager : MonoBehaviour
    {
        public static MozartManager instance;
        public List<NFTItem> storeItems;
        public List<NFTItem> inventoryItems;
        // Start is called before the first frame update
        void Start()
        {
            if (!instance) instance = this;
            Object.DontDestroyOnLoad(this.gameObject);
        }

        public void BuyItem(NFTItem item)
        {

        }

        public void LoadInventory()
        {

        }

        public void LoadStore()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
