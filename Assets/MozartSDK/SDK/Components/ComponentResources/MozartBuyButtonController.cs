﻿namespace Mozart
{ 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MozartBuyButtonController : MozartBehaviorBase
    {
        public string ItemTemplateID;
        public void TriggerBuy()
        {
            GetManager().BuyItem(ItemTemplateID);
        }
    }
}