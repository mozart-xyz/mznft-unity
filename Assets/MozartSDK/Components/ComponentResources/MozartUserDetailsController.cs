namespace Mozart
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class MozartUserDetailsController : MozartBehaviorBase
    {
        public TextMeshProUGUI emailLabel;
        public TextMeshProUGUI loginStatusLabel;


        private void OnEnable()
        {
            GetManager().onLoggedInEvent += DetailsChanged;
        }

        private void OnDisable()
        {
            GetManager().onLoggedInEvent -= DetailsChanged;
        }

        public void DetailsChanged()
        {
            emailLabel.text = GetManager().userData.email;
            loginStatusLabel.text = "Loggedin: " + GetManager().IsLoggedIn();
        }
    }
}