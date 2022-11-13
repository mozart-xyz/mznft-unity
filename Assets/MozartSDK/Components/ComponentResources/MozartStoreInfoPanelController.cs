namespace Mozart
{ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MozartStoreInfoPanelController : MozartNFTInfoPanelController
{
        [SerializeField]
        public void BuySelectedItem()
        { 
            base.GetManager().BuyItem(this.currentItem);
        }

        public override void SetData(NFTItem item)
        {
            nameText.text = item.name;
            descriptionText.text = item.description;
        }
    }
}