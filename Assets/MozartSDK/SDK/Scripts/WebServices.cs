namespace Mozart
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Networking;

    public class WebServices : MonoBehaviour
    {
        public static string serverRoot = "https://staging-api-ij1y.onrender.com";
        public SettingsTemplate mozartSettings;
        public MozartManager manager;
        public bool logging = false;
        public delegate void HandleError(MozartError error);
        public event HandleError HandleErrorEvent;

        private void Awake()
        {
            serverRoot = mozartSettings.apiBaseUrl;
        }

        public void GetRequest<T>(string url, UnityAction<T> callback)
        {
            StartCoroutine(DoGetRequest<T>(url, callback));
        }

        public void PostRequest<T>(string url, string postData, UnityAction<T> callback)
        {
            StartCoroutine(DoGetRequest<T>(url, callback, "POST", postData));
        }

        private IEnumerator DoGetRequest<T>(string url, UnityAction<T> completeCallback, string method = "GET", string postData = null)
        {
            UnityWebRequest www = null;
            if (method == "GET") www = UnityWebRequest.Get(serverRoot + url);
            if (method == "POST")
            {
                www = new UnityWebRequest(serverRoot + url, "POST");
                byte[] bodyRaw = Encoding.UTF8.GetBytes(postData);
                UploadHandler handler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                handler.contentType = "application/json";
                www.uploadHandler = handler;
                www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            }
            
            www.timeout = 20;
            string bearerToken = "Bearer " + manager.SessionToken;
            www.SetRequestHeader("Authorization", bearerToken);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                mozartSettings.Error(www.url + "::" + www.error + "__" + www.responseCode.ToString());
                MozartError newError = new MozartError
                {
                    url = www.url
                };
                if (www.downloadHandler != null && www.downloadHandler.text != null && www.downloadHandler.text.Length > 3)
                {
                    newError.data = www.downloadHandler.text;
                }
                else
                {
                    newError.data = www.error;
                }

                if (HandleErrorEvent != null) HandleErrorEvent(newError);
            }
            else
            {
                string output = www.downloadHandler.text;
                if(logging) mozartSettings.Log(www.url + ":::" + output);
                T vo = (T)Activator.CreateInstance(typeof(T));
                vo = JsonConvert.DeserializeObject<T>(output,
                new JsonSerializerSettings
                {
                    Error = delegate (object sender, ErrorEventArgs args)
                    {
                        mozartSettings.Warn(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    }
                });
                completeCallback(vo);
            }
        }
    }
}