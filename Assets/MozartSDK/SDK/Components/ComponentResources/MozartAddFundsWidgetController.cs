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
            string jwt = GetManager().SessionToken;
            string gameId = GetManager().settings.GameIdentifier;
            Application.OpenURL("https://mz-app-staging.onrender.com/" + gameId + "/" + jwt);
            StartCoroutine(PollForFundsChange());
        }

        IEnumerator PollForFundsChange()
        {
            int startingBalance = GetManager().userData.extraData.balances[0].GetBalance();
            int retryCount = 0;
            while(startingBalance == GetManager().userData.extraData.balances[0].GetBalance() && retryCount < 50)
            {
                retryCount++;
                yield return new WaitForSeconds(5f);
                GetManager().RequestUserData();
            }
        }
    }
}