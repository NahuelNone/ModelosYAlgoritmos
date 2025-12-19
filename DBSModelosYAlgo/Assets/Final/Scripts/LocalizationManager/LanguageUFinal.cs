using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LanguageUFinal
{

    public static Dictionary<LocalizationLangFinal, Dictionary<string, string>> GetTranslate(LocalizationDataFinal[] data)
    {

        var tempDic = new Dictionary<LocalizationLangFinal, Dictionary<string, string>>();

        for (int i = 0; i < data.Length; i++)
        {

            var tempData = new Dictionary<string, string>();

            foreach (var item in data[i].data)
            {

                var f = item.text.Split('|');

                foreach (var d in f)
                {

                    var c = d.Replace('{', ' ').
                              Replace('}', ' ').
                              Replace('"', ' ').
                              Split(':');

                    if (c.Length == 2 && !tempData.ContainsKey(c[0].Trim()))
                        tempData.Add(c[0].Trim(), c[1].Trim());

                }

            }

            tempDic.Add(data[i].language, tempData);

        }

        return tempDic;

    }

}
