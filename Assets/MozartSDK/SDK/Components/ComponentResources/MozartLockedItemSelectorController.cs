namespace Mozart
{ 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;

    public class MozartLockedItemSelectorController : MozartBehaviorBase
    {
        public RawImage itemImage;
        public Image lockImage;
        public string itemName;
        public TMPro.TextMeshProUGUI itemLabel;
        public NFTItem nftItem;
        public delegate void ON_SELECTION(NFTItem item);
        public ON_SELECTION onItemSelected;

        private void OnEnable()
        {
            //Resync after any changes to any of the storages
            itemImage.CrossFadeAlpha(0, 0, true);
            GetManager().onInventoryLoadedEvent -= Sync;
            GetManager().onInventoryLoadedEvent += Sync;
            GetManager().onStoreLoadedEvent -= Sync;
            GetManager().onStoreLoadedEvent += Sync;
            Sync();
        }

        /// <summary>
        /// Item Clicked is a callback that is called when the item is clicked, it is only fired if the item is unlocked
        /// </summary>
        public void ItemClicked()
        {
            if (lockImage.gameObject.activeInHierarchy) return;
            GetManager().settings.Log("Item " + nftItem.name + " clicked");
            if (onItemSelected == null)
            {
                GetManager().settings.Log("Item " + nftItem.name + " event onItemSelected is not mapped to anything.  Connect the delegate to use the selection");
            }
            else
            {
                onItemSelected(nftItem);
            }
        }

        /// <summary>
        /// Draws the state of the button
        /// </summary>
        public void Sync()
        {
            bool itemOwned = GetManager().GetItemIsOwnedByName(itemName);
            itemLabel.text = "Not Loaded";
            GetManager().settings.Log("Item Is Owned : " + itemOwned.ToString());
            NFTItem item = GetManager().GetItemByItemName(itemName);
            if (this.gameObject.activeInHierarchy && item != null)
            {
                nftItem = item;
                StartCoroutine(GetTexture(item));
                itemLabel.text = item.name;
                lockImage.gameObject.SetActive(!itemOwned);
            }
        }

        /// <summary>
        /// Load Image from NFT
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IEnumerator GetTexture(NFTItem item)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(item.image);
            yield return www.SendWebRequest();
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            itemImage.texture = myTexture;
            itemImage.CrossFadeAlpha(1f, 0.5f, true);
        }
    }
}