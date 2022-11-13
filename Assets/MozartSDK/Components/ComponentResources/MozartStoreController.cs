namespace Mozart
{ 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MozartStoreController : MozartInventoryController
    {

        protected override void MozartOnEnable()
        {
            if (!GetManager().IsLoggedIn()) return;
            GetManager().onStoreLoadedEvent += StoreLoaded;
            GetManager().LoadStore();
        }

        protected override void MozartOnDisable()
        {
            GetManager().onStoreLoadedEvent -= StoreLoaded;
        }

        private void StoreLoaded()
        {
            items = GetManager().storeItems;
            scroller.DataChanged();
        }
    }
}