namespace Mozart
{ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MozartSampleSelector : MozartBehaviorBase
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
        if(!GetManager().IsLoggedIn()) return;
        login.SetActive(false);
        inventory.SetActive(true);
        store.SetActive(false);
    }

    public void StoreClicked()
    {
        if(!GetManager().IsLoggedIn()) return;
        login.SetActive(false);
        inventory.SetActive(false);
        store.SetActive(true);
    }
}

}