using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManagerFinal : MonoBehaviour
{

    public static LocalizationManagerFinal instance;
    public LocalizationLangFinal language;

    public LocalizationDataFinal[] data;

    Dictionary<LocalizationLangFinal, Dictionary<string, string>> _translate;

    public event Action EventChangeLang;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // opcional
            _translate = LanguageUFinal.GetTranslate(data);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }



    public void ChangeLang(LocalizationLangFinal newLang)
    {

        if(language == newLang) return;

        language = newLang;

        if (EventChangeLang != null)
            EventChangeLang();

    }

    public void SetSpanish()
    {

        ChangeLang(LocalizationLangFinal.Spanish);

    }

    public void SetEnglish()
    {

        ChangeLang(LocalizationLangFinal.English);

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

public enum LocalizationLangFinal
{

    Spanish,
    English

}
