namespace Mozart
{
    using System;
    using System.Collections;
    using QRCoder;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;

    /// <summary>
    /// Used for OAUTH request to the mozart servers
    /// </summary>
    [Serializable]
    public class MozartOAUTHRequest
    {
        public string facebookUrl;
        public string githubUrl;
        public string googleUrl;
        public string mzUrl;
        public string oauthState;
    }

    /// <summary>
    /// Captures the response of the OAUTH request from google
    /// </summary>
    [Serializable]
    public class MozartOAUTHState
    {
        public string email;
        public string familyName;
        public string givenName;
        public string jwtToken;
        public string locale;
        public string status;
    }

    /// <summary>
    /// Turnkey OAUTH login button for Mozart.  It is best to drag and drop the
    /// prefab to use this instead of using it directly. curl -H "Authorization: Bearer $token" https://staging-api-ij1y.onrender.com/v1/client/me?gameId=9mAc1yrUArR
    /// </summary>
    public class MozartSDKLoginButton : MozartBehaviorBase
    {
        private const string AUTH_URL_BASE = "https://staging-api-ij1y.onrender.com/v1/auth";
        [SerializeField]
        private Button LoginButton;
        [SerializeField]
        private RawImage QRCode;
        /// <summary>
        /// This is where the MozartSettings file is linked to the button for API key
        /// </summary>
        public SettingsTemplate mozartSettings;

        public string SessionToken = "";
        public string loginToken = "";
        /// <summary>
        /// This controls how many times we retry checking for OAUTH success.
        /// Multiply this by timeBetweenRetry to see how long we will wait for a response.
        /// </summary>
        public int maxRetry = 90;
        /// <summary>
        /// Time in seconds between retries to check for OAUTH complete success
        /// </summary>
        public float timeBetweenRetry = 1f;

        /// <summary>
        /// Activate this if you want to use a QR code to authenticate your users
        /// instead of a direct link to the web browser.
        /// </summary>
        public bool enableQRCodeAuthentication = false;
        public delegate void LOGIN_COMPLETE(string token);
        public event LOGIN_COMPLETE LoginComplete = null;

        private enum LOGIN_STATE
        {
            WAITING_FOR_LOGIN,
            LOGIN_TIMEOUT,
            LOGIN_SUCCESS
        }

        private LOGIN_STATE state = LOGIN_STATE.WAITING_FOR_LOGIN;

        /// <summary>
        /// This function starts the login process.
        /// If you want to run this manually you can hide the button and call this function.
        /// </summary>
        public void LoginClicked()
        {
            state = LOGIN_STATE.WAITING_FOR_LOGIN;
            LoginButton.enabled = false;
            StartCoroutine(GetLoginToken());
        }

        IEnumerator GetLoginToken()
        {
            var request = UnityWebRequest.Get(AUTH_URL_BASE + "/login?gameId=" + mozartSettings.GameIdentifier);

            // Wait for the response and then get our data
            yield return request.SendWebRequest();
            var data = request.downloadHandler.text;
            MozartOAUTHRequest response = JsonUtility.FromJson<MozartOAUTHRequest>(data);
            loginToken = response.oauthState;
            string uri = response.googleUrl;
            Debug.LogWarning("GOOGLE URL:" + uri);
            if (enableQRCodeAuthentication == false ||
                Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Application.OpenURL(uri);
            }
            else
            {
                Texture2D qrTex = QRCodeHelper.GetQRCode(uri, 8);
                QRCode.texture = qrTex;
                QRCode.gameObject.SetActive(true);
            }
            this.StartCoroutine(CheckForAuthentication());
        }

        IEnumerator CheckForAuthentication()
        {
            int tryCount = 0;
            string oauthURL = AUTH_URL_BASE + "/login_status?oauthState=" + loginToken;
            Debug.Log("OAUTH URL::" + oauthURL);
            Debug.Log("Token:" + loginToken);
            while (state == LOGIN_STATE.WAITING_FOR_LOGIN)
            {
                tryCount++;
                if (tryCount > maxRetry)
                {
                    state = LOGIN_STATE.LOGIN_TIMEOUT;
                    LoginButton.enabled = true;
                    QRCode.gameObject.SetActive(false);
                    yield break;
                }
                yield return new WaitForSeconds(timeBetweenRetry);
                var request = UnityWebRequest.Get(oauthURL);

                // Wait for the response and then get our data
                yield return request.SendWebRequest();
                var data = request.downloadHandler.text;
                Debug.Log("Response:" + data);
                MozartOAUTHState status = JsonUtility.FromJson<MozartOAUTHState>(data);
                if(status.status.ToLower() == "ok")
                {
                    SessionToken = status.jwtToken;
                    state = LOGIN_STATE.LOGIN_SUCCESS;
                    LoginButton.enabled = false;
                    QRCode.gameObject.SetActive(false);
                    base.GetManager().userData.email = status.email;
                    base.GetManager().SetSessionToken(SessionToken);
                    if (LoginComplete != null) LoginComplete(SessionToken);
                    Debug.Log("Login Succeessful " + status.jwtToken);
                }

            }
        }
    }

}