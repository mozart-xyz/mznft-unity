namespace Mozart
{ 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MozartBuyButtonController : MozartBehaviorBase
    {
        public string ItemTemplateID;
        public TMPro.TextMeshProUGUI costLabel;
        public void TriggerBuy()
        {
            GetManager().BuyItem(ItemTemplateID);
        }

        public void OnEnable()
        {
            GetManager().onStoreLoadedEvent -= Sync;
            GetManager().onStoreLoadedEvent += Sync;
            Sync();
        }

        private void Sync()
        {
            NFTItem item = GetManager().GetItemByItemTemplateId(ItemTemplateID);
            if (item != null)
            {
                costLabel.text = item.price + " " + item.priceTokenName;
            }
        }
    }
}