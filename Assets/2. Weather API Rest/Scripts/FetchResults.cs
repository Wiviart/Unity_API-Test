using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FetchResults : MonoBehaviour
{
    static readonly string DefaultIcon = "01d";
    bool isRunningQuery;
    Button button;
    OpenWeatherMapAPI api;
    Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    public GameObject loadingMessage;
    public TMP_InputField cityInputField;
    public DayCard[] dayCards;
    public Sprite[] spriteIcons;
    public CanvasGroup panel;

    void Awake()
    {
        button = GetComponent<Button>();
        api = GetComponent<OpenWeatherMapAPI>();

        foreach (Sprite sprite in spriteIcons)
        {
            sprites[sprite.name] = sprite;
        }

        button.onClick.AddListener(delegate
        {
            if (!string.IsNullOrEmpty(cityInputField.text.Trim()) && !isRunningQuery)
            {
                StartCoroutine(FetchData(cityInputField.text));
            }
        });
    }

    private IEnumerator FetchData(string text)
    {
        isRunningQuery = true;
        panel.alpha = 0f;
        loadingMessage.SetActive(true);

        // yield return api.GetForecast(text);
        yield return api.GetForecast(text, FillDays);

        loadingMessage.SetActive(false);
        isRunningQuery = false;

        // if (api.Response != null)
        // {
        //     FillDays(api.Response);
        //     panel.alpha = 1f;
        // }
    }

    private void FillDays(ResponseContainer response)
    {
        panel.alpha = 1f;

        for (int i = 0; i < dayCards.Length; i++)
        {
            var icon = response.list[i].weather[0].icon;

            if (!sprites.ContainsKey(icon))
            {
                icon = DefaultIcon;
            }

            Sprite sprite = sprites[icon];

            DayCardModel day = new DayCardModel(response.list[i], sprite);
            DayCard dayCard = dayCards[i];

            dayCard.SetModel(day);
        }
    }
}
