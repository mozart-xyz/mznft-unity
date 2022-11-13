namespace Mozart
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class MozartNFTInfoPanelController : MozartBehaviorBase
    {
        public NFTItem currentItem;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI imageText;
        public virtual void SetData(NFTItem item)
        {
            nameText.text = item.name;
            descriptionText.text = item.description;
            imageText.text = item.image;
        }
    }
}