using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;

public class OpenWeatherMapAPI : MonoBehaviour
{
    private static readonly string ApiBaseUrl = "https://api.openweathermap.org/data/2.5/forecast/daily?q={0}&cnt=5&appid={1}";

    [Tooltip("The key that allows access to the OpenWeatherMap API")]
    public string apiKey;
    // public ResponseContainer Response { get; private set; }

    public IEnumerator GetForecast(string city, Action<ResponseContainer> onSuccess)
    {
        // Response = null; 
        // string urlEncodedCity = HttpUtility.UrlEncode(city);
        // string url = string.Format(ApiBaseUrl, urlEncodedCity, apiKey);
        // UnityWebRequest webRequest = UnityWebRequest.Get(url);

        // yield return webRequest.SendWebRequest();

        // if (webRequest.result == UnityWebRequest.Result.Success)
        // {
        //     string json = webRequest.downloadHandler.text;
        //     Response = JsonUtility.FromJson<ResponseContainer>(json);
        // }
        // else
        // {
        //     Debug.Log(webRequest.error);
        // }

        string urlEncodedCity = HttpUtility.UrlEncode(city);
        string url = string.Format(ApiBaseUrl, urlEncodedCity, apiKey);
        yield return RestfulHelper.Fetch(url, onSuccess, print);
    }
}
