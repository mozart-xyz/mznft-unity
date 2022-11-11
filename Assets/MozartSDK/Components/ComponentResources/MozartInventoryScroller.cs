namespace Mozart
{ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.UI;
    using UnlimitedScrollUI;

public class MozartInventoryScroller : MonoBehaviour
{
    public GameObject cell;
    public bool autoGenerate;
    public int totalCount = 33;
    public MozartInventoryController inventoryController;
    private IUnlimitedScroller unlimitedScroller;
    public ToggleGroup toggleGroup;
    public void Generate()
    {
        if(!toggleGroup)
            {
                toggleGroup = this.gameObject.AddComponent<ToggleGroup>();
            }
        unlimitedScroller.Clear();
            List<NFTGridCellController> cells = new List<NFTGridCellController>();
        unlimitedScroller.Generate(cell, inventoryController.items.Count, (index, iCell) => {
            var regularCell = iCell as NFTGridCellController;
            if (regularCell != null)
            {
                regularCell.SetupCell(inventoryController, inventoryController.items[index]);
                regularCell.selection.group = toggleGroup;
                cells.Add(regularCell);
                regularCell.onGenerated?.Invoke(index);
            }
        });
            inventoryController.SetCells(cells);
    }

    private void Start()
    {
        unlimitedScroller = GetComponent<IUnlimitedScroller>();
        // Wait until the scroller size was set by other layout controllers.
        if (autoGenerate)
        {
            StartCoroutine(DelayGenerate());
        }
    }

    private IEnumerator DelayGenerate()
    {
        yield return new WaitForEndOfFrame();
        Generate();
    }
}
}