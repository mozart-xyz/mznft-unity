namespace Mozart
{
    using System;
    using System.Collections;
    using QRCoder;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;
    public class MozartSDKLoginButton : MonoBehaviour
    {
        public Button LoginButton;
        public RawImage QRCode;
        public SettingsTemplate mozartSettings;
        public string SessionToken = "";

        public delegate void LOGIN_COMPLETE(string token);
        public event LOGIN_COMPLETE LoginComplete = null;
        private enum LOGIN_STATE
        {
            WAITING_FOR_LOGIN,
            LOGIN_TIMEOUT,
            LOGIN_SUCCESS
        }

        private LOGIN_STATE state = LOGIN_STATE.WAITING_FOR_LOGIN;

        private string authID = "";

        public void LoginClicked()
        {
            state = LOGIN_STATE.WAITING_FOR_LOGIN;
            LoginButton.enabled = false;
            authID = Guid.NewGuid().ToString();
            string uri = "https://mozart.xyz/login/request?id=" + authID + "&p=" + Application.platform;
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Application.OpenURL(uri);
            }
            else
            {
                Texture2D qrTex = QRCodeHelper.GetQRCode(uri + authID, 8);
                QRCode.texture = qrTex;
                QRCode.gameObject.SetActive(true);
            }
            this.StartCoroutine(CheckForAuthentication());
        }

        IEnumerator CheckForAuthentication()
        {
            int tryCount = 0;
            while (state == LOGIN_STATE.WAITING_FOR_LOGIN)
            {
                tryCount++;
                if (tryCount > 90)
                {
                    state = LOGIN_STATE.LOGIN_TIMEOUT;
                    QRCode.gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(1f);
                var request = UnityWebRequest.Get("https://mozart.xyz/validate?id=" + authID);

                // Wait for the response and then get our data
                yield return request.SendWebRequest();
                var data = request.downloadHandler.text;
                /*if(data != "X")
                {
                    SessionToken = data;
                    state = LOGIN_STATE.LOGIN_SUCCESS;
                    LoginButton.enabled = false;
                    QRCode.gameObject.SetActive(false);
                    if (LoginComplete != null) LoginComplete(SessionToken);
                }*/
            }
        }
    }

}