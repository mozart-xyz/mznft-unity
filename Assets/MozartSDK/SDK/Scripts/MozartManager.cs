namespace Mozart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// The MozartManager is the brain of the entire Mozart system.  You should
    /// create one of these when your application starts and keep it alive for
    /// the duration of the application with DoNotDestroy.  This will happen
    /// automatically if you use the Prefab.
    /// </summary>
    public partial class MozartManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton access for the manager, used as a convenience, not by default.
        /// By default you can extend MozartBehavior for your own classes and they will find the manager.
        /// </summary>
        public static MozartManager instance;
        public SettingsTemplate settings;
        /// <summary>
        /// This is a list of items loaded from the store for this app
        /// </summary>
        public List<NFTItem> storeItems = new List<NFTItem>();
        /// <summary>
        /// This is a list of items the user actually has in their inventory for this app
        /// </summary>
        public List<NFTItem> inventoryItems = new List<NFTItem>();
        /// <summary>
        /// This is the users temporary session token, it will change every time they sign in with SSO
        /// </summary>
        public string SessionToken = "";
        /// <summary>
        /// This is user data specific to this user
        /// </summary>
        public MozartUser userData = new MozartUser();

        /// <summary>
        /// This is the web services helper for making web services calls in a generic way
        /// </summary>
        public WebServices webs;

        public delegate void ON_USER_CHANGE();
        public ON_USER_CHANGE onUserChangedEvent;

        public delegate void ON_LOGIN();
        /// <summary>
        /// This event will fire when the user logs in, there is no data passed.
        /// If you want the user object read it from MozartManager.userData.
        /// </summary>
        public ON_LOGIN onLoggedInEvent;

        public delegate void ON_INVENTORY_LOADED();
        /// <summary>
        /// This event will fire after the inventory response comes back from the server.
        /// </summary>
        public ON_INVENTORY_LOADED onInventoryLoadedEvent;

        public delegate void ON_STORE_LOADED();
        /// <summary>
        /// This event will fire after the store data is loaded from the server and populated.
        /// </summary>
        public ON_STORE_LOADED onStoreLoadedEvent;

        public delegate void ON_PURCHASE_COMPLETE();
        /// <summary>
        /// This will fire after a purchase has been successfully completed.
        /// </summary>
        public ON_PURCHASE_COMPLETE onPurchaseCompleteEvent;

        /// <summary>
        /// IsLoggedIn tells us if a user is logged in or not,
        /// can be used to control session specific ui state.
        /// </summary>
        /// <returns>Bool is user logged in</returns>
        public bool IsLoggedIn()
        {
            return SessionToken != "";
        }

        // Start is called before the first frame update
        void Awake()
        {
            if (!instance) instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Sets the session token for the user after the login is completed.
        /// The MozartSDKLoginButton has logic to automatically call this.
        /// </summary>
        /// <param name="sessionToken"></param>
        public void SetSessionToken(string sessionToken)
        {
            SessionToken = sessionToken;
            RequestUserData();
            LoadStore();
            if (onLoggedInEvent != null) onLoggedInEvent.Invoke();
        }

        /// <summary>
        /// Requests additional user data like balances, nfts, and user info
        /// The MozartSDKLoginButton has logic to automatically call this.
        /// </summary>
        public void RequestUserData()
        {
            webs.GetRequest<MeResponse>("/v1/client/me?gameId=" + webs.mozartSettings.GameIdentifier, (MeResponse response) =>
            {
                userData.extraData = response;
                if(settings.logging) Debug.Log(JsonUtility.ToJson(response));
                if (onUserChangedEvent != null) onUserChangedEvent();
                PopulateInventory();
            });
        }

        public bool GetItemIsOwned(string itemTemplateId)
        {
            foreach (NFTItem item in inventoryItems)
            {
                if (item.itemTemplateId == itemTemplateId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetItemIsOwnedByName(string itemName)
        {
            foreach (NFTItem item in inventoryItems)
            {
                if (item.name == itemName)
                {
                    return true;
                }
            }
            return false;
        }


        public NFTItem GetItemByItemName(string itemName)
        {
            foreach (NFTItem item in storeItems)
            {
                if (item.name == itemName)
                {
                    return item;
                }
            }
            return null;
        }


        public NFTItem GetItemByItemTemplateId(string itemTemplateId)
        {
            foreach(NFTItem item in storeItems)
            {
                if(item.itemTemplateId == itemTemplateId)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Buy a specific item from the store and give it to the user
        /// </summary>
        /// <param name="item"></param>
        public void BuyItem(string ItemTemplateID)
        {
            string postData = "{\"nftTemplateListingId\":\"" + ItemTemplateID + "\"}";
            webs.PostRequest<BuyResponse>("/v1/client/template_items/buy", postData, (BuyResponse response) =>
            {
                RequestUserData();
                if (onPurchaseCompleteEvent != null) onPurchaseCompleteEvent();
            });
        }

        /// <summary>
        /// Fills inventory from data on the user object passed down from the /me server call
        /// </summary>
        private void PopulateInventory()
        {
            if (userData.extraData == null) return;
            inventoryItems.Clear();
            foreach (Nft nft in userData.extraData.nfts)
            {
                NFTItem newItem = new NFTItem { name = nft.name, image = nft.imageUrl, description = nft.description };
                inventoryItems.Add(newItem);
            }
            onInventoryLoadedEvent?.Invoke();
        }

        /// <summary>
        /// Load the store for the current app
        /// </summary>
        public void LoadStore()
        {
            // /v1/client/factory_items/for_sale
            webs.GetRequest<List<ForSaleFactoryNft>>("/v1/client/template_items/for_sale?gameId=" + webs.mozartSettings.GameIdentifier, (List<ForSaleFactoryNft> forSale) =>
            {
                storeItems.Clear();
               
                foreach (ForSaleFactoryNft nft in forSale)
                {

                    NFTItem newItem = new NFTItem { name = nft.name, image = nft.imageUrl, price = nft.price, priceTokenName = nft.priceTokenName, priceTokenId = nft.priceTokenId, itemTemplateId=nft.nftTemplateListingId};
                    if (settings.logging) Debug.Log(JsonUtility.ToJson(newItem));
                    storeItems.Add(newItem);
                }
                onStoreLoadedEvent?.Invoke();
            });
        }

        /// <summary>
        /// Add funds calls on the AddFunds system to add more funds to the application
        /// </summary>
        public void AddFunds()
        {
            string jwt = SessionToken;
            string gameId = settings.GameIdentifier;
            Application.OpenURL(settings.dashboardUrl + "/checkout?gameId=" + gameId + "&ftId=" + settings.GameCurrencyIdentifier + "&jwt=" + jwt);
            StartCoroutine(PollForFundsChange());
        }

        IEnumerator PollForFundsChange()
        {
            int startingBalance = userData.GetBalance();
            int retryCount = 0;
            while (startingBalance == userData.GetBalance() && retryCount < 50)
            {
                retryCount++;
                yield return new WaitForSeconds(5f);
                RequestUserData();
            }
        }
    }

}
