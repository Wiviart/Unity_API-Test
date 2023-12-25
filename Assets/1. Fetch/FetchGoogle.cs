using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class FetchGoogle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject textContainer;

    IEnumerator Start()
    {
        string url = "https://www.google.com/";

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);

            string html = request.downloadHandler.text;

            string[] splits = SplitContent(html, 1000);

            foreach (string split in splits)
            {
                TextMeshProUGUI newText = Instantiate(text, textContainer.transform);
                newText.text = split;
            }
        }
        else
        {
            Debug.Log(request.error);
        }
    }

    private string[] SplitContent(string content, int chunkSize)
    {
        List<string> chunks = new List<string>();
        for (int i = 0; i < content.Length; i += chunkSize)
        {
            chunks.Add(content.Substring(i, Mathf.Min(chunkSize, content.Length - i)));
        }
        return chunks.ToArray();
    }
}