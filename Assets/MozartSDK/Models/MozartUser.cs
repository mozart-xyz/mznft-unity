namespace Mozart
{ 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class MozartUser
    {
        public string auth_token;
        public string auth_request_id;
        public string user_id;
        public string username;
        public string profile_image_url;
        public List<NFTItem> inventory;
        public decimal balance_usd;
    }
}