namespace Mozart
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Networking;

    public class WebServices : MonoBehaviour
    {
        public static string serverRoot = "https://api.mozart.xyz";

        public delegate void HandleError(MozartError error);
        public event HandleError HandleErrorEvent;

        void Start()
        {
        }

        public void GetRequest<T>(string url, UnityAction<T> callback)
        {
            StartCoroutine(DoGetRequest<T>(url, callback));
        }

        public void PostRequest<T>(string url, WWWForm postVars, UnityAction<T> callback)
        {
            StartCoroutine(DoGetRequest<T>(url, callback, "POST", postVars));
        }


        private IEnumerator DoGetRequest<T>(string url, UnityAction<T> completeCallback, string method = "GET", WWWForm postVars = null)
        {
            UnityWebRequest www = null;
            www.timeout = 20;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.url + "::" + www.error + "__" + www.responseCode.ToString());
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
                Debug.Log(www.url + ":::" + output);
                T vo = (T)Activator.CreateInstance(typeof(T));
                vo = JsonConvert.DeserializeObject<T>(output,
                new JsonSerializerSettings
                {
                    Error = delegate (object sender, ErrorEventArgs args)
                    {
                        Debug.LogWarning(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    }
                });
                completeCallback(vo);
            }
        }
    }
}