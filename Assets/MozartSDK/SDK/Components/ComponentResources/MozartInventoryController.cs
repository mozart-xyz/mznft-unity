namespace Mozart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// UI for the included Inventory grid component.  This is intended to be used as a drag and drop prefab.
    /// If you want to change how it works, you can but you will have to read and understand it.
    /// Also keep in mind store uses the same code, but extends this, which is why there are virtual methods
    /// in this class.
    /// </summary>
    public class MozartInventoryController : MozartBehaviorBase
    {
        public List<NFTItem> items = new List<NFTItem>();
        private List<NFTGridCellController> uiCells = new List<NFTGridCellController>();

        public virtual List<NFTItem> GetItems()
        {
            return GetManager().inventoryItems;
        }

        [Serializable]
        public class GridCellClickEvent : UnityEvent<NFTItem> { }
        /// <summary>
        /// This is fired when a cell is clicked on in the scroller.  You can assign this event to a target
        /// with the Unity UI.  In the demo it is pre-wired to work with NFTInfoPanelController
        /// </summary>
        public GridCellClickEvent onItemCellClicked;

        public MozartInventoryScroller scroller;
        // Start is called before the first frame update

        private void OnEnable() { MozartOnEnable(); }
        private void OnDisable() { MozartOnDisable(); }

        /// <summary>
        /// Fires when the component is enabled.  We added a MozartOnEnable so that we are not overriding unity OnEnable directly.
        /// Adds an Inventory Loaded listener.
        /// </summary>
        protected virtual void MozartOnEnable()
        {
            if (!GetManager().IsLoggedIn()) return;
            GetManager().onInventoryLoadedEvent += InventoryLoaded;
            GetManager().LoadInventory();
        }

        /// <summary>
        /// Fired when component is disabled.  We added this so that we are not overriding Unity OnDisable directly.
        /// Removes the Inventory Loaded Listener
        /// </summary>
        protected virtual void MozartOnDisable()
        {
            GetManager().onInventoryLoadedEvent -= InventoryLoaded;
        }

        /// <summary>
        /// Fires when inventory is loaded
        /// </summary>
        private void InventoryLoaded()
        {
            items = GetManager().inventoryItems;
            scroller.DataChanged();
        }

        void Start()
        {
            scroller = this.GetComponentInChildren<MozartInventoryScroller>();
            //TODO:Add web request here to go load inventory

        }

        public void SetCells(List<NFTGridCellController> cells)
        {
            uiCells = cells;
        }

        /// <summary>
        /// Fires when a cell is clicked.  This also dispatches an event with data about the cell that was clicked.  This is used with
        /// other components like the MozartNFTInfoPanelController to display information about the clicked object.
        /// </summary>
        /// <param name="cell"></param>
        public void CellClicked(NFTGridCellController cell)
        {
            if (onItemCellClicked != null) onItemCellClicked.Invoke(cell.cellData);
        }
    }
}