namespace Mozart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MozartInventoryController : MonoBehaviour
    {
        public List<NFTItem> items = new List<NFTItem>();
        private List<NFTGridCellController> uiCells = new List<NFTGridCellController>();
        // Start is called before the first frame update
        void Start()
        {
            items = new List<NFTItem>
            {
                new NFTItem{name="Ben", image="https://cdn.domestika.org/c_limit,dpr_1.0,f_auto,q_auto,w_820/v1542280317/content-items/002/609/007/ARMADURA-original.jpg?1542280317"},
                new NFTItem{name="Jin", image="https://cdn.domestika.org/c_limit,dpr_1.0,f_auto,q_auto,w_820/v1544721542/content-items/002/665/263/3-B%25C3%2581RBARO-original.jpg?1544721542"},
                new NFTItem{name="Oliver", image="https://cdn.domestika.org/c_limit,dpr_1.0,f_auto,q_auto,w_820/v1544721520/content-items/002/665/254/2-_ARQUERA-original.jpg?1544721520"},
                new NFTItem{name="Saureen", image="https://cdn.domestika.org/c_limit,dpr_1.0,f_auto,q_auto,w_820/v1544718087/content-items/002/665/186/BRUJAtrad-original.jpg?1544718087"},
                new NFTItem{name="Arman", image="https://cdn.domestika.org/c_limit,dpr_1.0,f_auto,q_auto,w_820/v1544721481/content-items/002/665/252/1-MAGO-original.jpg?1544721481"},
            };
        }

        public void SetCells(List<NFTGridCellController> cells)
        {
            uiCells = cells;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void CellClicked(NFTGridCellController nFTGridCellController)
        {
           
        }
    }
}