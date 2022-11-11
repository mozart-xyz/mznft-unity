namespace Mozart
{ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MozartStoreInfoPanelController : MozartNFTInfoPanelController
{
        public MozartManager manager;
        [SerializeField]
        public void BuySelectedItem()
        {
            if (!manager) manager = MozartManager.instance;
            manager.BuyItem(this.currentItem);
        }

        public override void SetData(NFTItem item)
        {
            nameText.text = item.name;
            descriptionText.text = item.description;
        }
    }
}