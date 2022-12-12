namespace Mozart
{ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MozartAddFundsWidgetController : MozartBehaviorBase
    {
        public TMPro.TextMeshProUGUI fundsLabel;

        private void OnEnable()
        {
            GetManager().onUserChangedEvent -= SyncData;
            GetManager().onUserChangedEvent += SyncData;
            SyncData();
        }

        private void SyncData()
        {
            if(GetManager().userData != null && GetManager().userData.extraData != null && GetManager().userData.extraData.balances != null && GetManager().userData.extraData.balances.Count > 0)
            {
                fundsLabel.text = GetManager().userData.extraData.balances[0].balance;
            }
        }

        public void AddFunds()
        {
            GetManager().AddFunds();
        }
    }
}