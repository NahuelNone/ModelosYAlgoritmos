using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager instance;
    public LocalizationLang language;

    public LocalizationData[] data;

    Dictionary<LocalizationLang, Dictionary<string, string>> _translate;

    public event Action EventChangeLang;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // opcional
            _translate = LanguageU.GetTranslate(data);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }



    public void ChangeLang(LocalizationLang newLang)
    {

        if(language == newLang) return;

        language = newLang;

        if (EventChangeLang != null)
            EventChangeLang();

    }

    public void SetSpanish()
    {

        ChangeLang(LocalizationLang.Spanish);

    }

    public void SetEnglish()
    {

        ChangeLang(LocalizationLang.English);

    }

    public string Translate(string ID)
    {
        if (!_translate.ContainsKey(language))
            return "No lang";

        if (!_translate[language].ContainsKey(ID))
            return "No ID";

        return _translate[language][ID];
    }

}

public enum LocalizationLang
{

    Spanish,
    English

}
