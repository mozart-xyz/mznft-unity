namespace Mozart
{ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MozartSampleSelector : MonoBehaviour
{
    public GameObject login;
    public GameObject inventory;
    public GameObject store;

    public void LoginClicked()
    {
        login.SetActive(true);
        inventory.SetActive(false);
        store.SetActive(false);
    }

    public void InventoryClicked()
    {
        Debug.Log("Show Inventory");
        login.SetActive(false);
        inventory.SetActive(true);
        store.SetActive(false);
    }

    public void StoreClicked()
    {
        Debug.Log("Show Store");
        login.SetActive(false);
        inventory.SetActive(false);
        store.SetActive(true);
    }
}

}