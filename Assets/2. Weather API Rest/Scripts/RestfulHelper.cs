using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class RestfulHelper
{
    public static IEnumerator Fetch<T>(
        string endPoint,
        Action<T> onSuccess,
        Action<string> onError)
        where T :
    class, new()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(endPoint))
        {
            yield return webRequest.SendWebRequest();

            // if (webRequest.isNetworkError || webRequest.isHttpError)
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                onError?.Invoke(webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                onSuccess?.Invoke(JsonUtility.FromJson<T>(json));
            }
        }
    }
}
