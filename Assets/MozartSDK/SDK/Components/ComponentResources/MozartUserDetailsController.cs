namespace Mozart
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    /// <summary>
    /// Automatically hooks into the MozartManager to show user details when the user logs in with OAuth successfully
    /// You can modify DetailsChanged or extend this class and implement your own DetailsChanged function to keep your code
    /// safe for updates in the future.
    /// </summary>
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

        public virtual void DetailsChanged()
        {
            emailLabel.text = GetManager().userData.email;
            loginStatusLabel.text = "Signed In: " + GetManager().IsLoggedIn();
        }
    }
}