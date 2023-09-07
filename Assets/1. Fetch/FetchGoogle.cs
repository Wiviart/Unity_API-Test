using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class FetchGoogle : MonoBehaviour
{
    IEnumerator Start()
    {
        string url = "https://www.google.com/";
     
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.error);
        }
    }
}