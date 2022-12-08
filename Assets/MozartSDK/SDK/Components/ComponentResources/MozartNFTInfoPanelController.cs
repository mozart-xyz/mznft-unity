namespace Mozart
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    /// <summary>
    /// This is the default NFTInfoPanel widget that can be dragged from the prefabs list into your project
    /// You can assign this as a target for onItemCellClicked for MozartInventoryController or MozartStoreController
    /// using the Unity UI
    /// </summary>
    public class MozartNFTInfoPanelController : MozartBehaviorBase
    {
        public NFTItem currentItem;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI imageText;
        public virtual void SetData(NFTItem item)
        {
            currentItem = item;
            nameText.text = item.name;
            descriptionText.text = item.description;
            imageText.text = item.image;
        }
    }
}