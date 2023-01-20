namespace Mozart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class MozartUser
    {

        public int GetBalance()
        {
            string currency = MozartManager.instance.settings.GameCurrencyIdentifier.Replace("@", "");
            if (this.extraData != null && this.extraData.balances != null && this.extraData.balances.Count > 0)
            {
                foreach(V1FtsBalances balance in this.extraData.balances)
                {
                    if(currency.Contains(balance.name))
                    {
                        return balance.GetBalance();
                    }
                }
            }
            return 0;
        }

        public MeResponse extraData;
        /*![string] NO DESCRIPTION, PLEASE ADD TO API.YAML*/
        public string id;

        /*![string] NO DESCRIPTION, PLEASE ADD TO API.YAML*/
        public string name;

        /*![string] The wallet address associated with the user.*/
        public string walletAddress;
        public string email;
        public string auth_token;
        public string auth_request_id;
        public string user_id;
        public string username;
        public string profile_image_url;
        public List<NFTItem> inventory;
        public decimal balance_usd;
    }
}