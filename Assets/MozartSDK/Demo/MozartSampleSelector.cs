namespace Mozart
{ 
using UnityEngine;

    /// <summary>
    /// This class is a demo class that controls the demo UI
    /// </summary>
public class MozartSampleSelector : MozartBehaviorBase
{
    public GameObject login;
    public GameObject inventory;
    public GameObject store;
    public GameObject loginPanel;

        private void Start()
        {
            GetManager().onLoggedInEvent += LoginCompleted;
        }
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

    public void LoginCompleted()
    {
        loginPanel.SetActive(false);
    }
}

}