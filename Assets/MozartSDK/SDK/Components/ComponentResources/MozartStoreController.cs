using System.Collections.Generic;

namespace Mozart
{
    /// <summary>
    /// Extended version of inventory controller used for store and store callbacks.
    /// </summary>
    public class MozartStoreController : MozartInventoryController
    {
        public override List<NFTItem> GetItems()
        {
            return GetManager().storeItems;
        }

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