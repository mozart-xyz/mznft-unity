namespace Mozart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class MozartManager : MonoBehaviour
    {
        public static MozartManager instance;
        public List<NFTItem> storeItems;
        public List<NFTItem> inventoryItems;
        public string SessionToken = "";
        public MozartUser userData = new MozartUser();
        public WebServices webs;

        public delegate void ON_LOGIN();
        public ON_LOGIN onLoggedInEvent;

        public delegate void ON_INVENTORY_LOADED();
        public ON_INVENTORY_LOADED onInventoryLoadedEvent;

        public delegate void ON_STORE_LOADED();
        public ON_STORE_LOADED onStoreLoadedEvent;

        public delegate void ON_PURCHASE_COMPLETE();
        public ON_PURCHASE_COMPLETE onPurchaseCompleteEvent;

        public bool IsLoggedIn()
        {
            return SessionToken != "";
        }

        // Start is called before the first frame update
        void Start()
        {
            if (!instance) instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
        }

        public void SetSessionToken(string sessionToken)
        {
            SessionToken = sessionToken;
            if (onLoggedInEvent != null) onLoggedInEvent.Invoke();
        }

        public void SetUserData()
        {

        }

        public void BuyItem(NFTItem item)
        {
            //TODO: Hook up web services
            if (onPurchaseCompleteEvent != null) onPurchaseCompleteEvent();
        }

        public void LoadInventory()
        {
            //TODO: Hook up web services
            if (onInventoryLoadedEvent != null) onInventoryLoadedEvent();
        }

        public void LoadStore()
        {
            //TODO: Hook up web services
            if (onStoreLoadedEvent != null) onStoreLoadedEvent();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
