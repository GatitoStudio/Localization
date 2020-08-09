using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalizationManager :  MonoBehaviour
{

    public LocalizationSystem traductionSystem;
    private static LocalizationManager instance;   // GameSystem local instance
    public SystemLanguage systemLanguageTest;
   
    // Start is called before the first frame update
    void Start()
    {
        TextAsset[] testResources = Resources.LoadAll<TextAsset>("Traduction").Cast<TextAsset>().ToArray();
        traductionSystem.langagesTraduction = new Dictionary<string, Dictionary<string, string>>();
        for (int i = 0; i < testResources.Length; ++i)
        {
            LoadJson(testResources[i].name);
        }
        Test();
        //LoadKey();
    }
    public void Test()
    {
        //langue par défaut anglais;
        if (traductionSystem.langagesTraduction.ContainsKey(systemLanguageTest.ToString()))
        {
            traductionSystem.CurrentLang = systemLanguageTest.ToString();
        }
        else
        {
            //donnez le nom de votre langue par defaut
            traductionSystem.CurrentLang = "English";
        }
    }
    public void LoadKey()
    {
        //langue par défaut anglais;
        if (traductionSystem.langagesTraduction.ContainsKey(systemLanguageTest.ToString()))
        {
            if (!PlayerPrefs.HasKey("lang"))
            {
                traductionSystem.CurrentLang = Application.systemLanguage.ToString();
                PlayerPrefs.SetString("lang", traductionSystem.CurrentLang);
            }
            else
                traductionSystem.CurrentLang = PlayerPrefs.GetString("lang");
        }
        else
        {
            //donnez le nom de votre langue par defaut
            traductionSystem.CurrentLang = "English";
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadJson(string lang)
    {
        LocalizationData loadedData = new LocalizationData();
        TextAsset text = Resources.Load<TextAsset>("Traduction/" + lang);
        traductionSystem.langagesTraduction.Add(lang.Split('_')[1], new Dictionary<string, string>());
        if (text != null)
        {
            loadedData = JsonUtility.FromJson<LocalizationData>(text.text);
        }
        if (loadedData.items != null)
        {
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                traductionSystem.langagesTraduction[lang.Split('_')[1]].Add(loadedData.items[i].key, loadedData.items[i].value);
            }          
        }
    }

    [System.Serializable]
    public class LocalizationData
    {
        public LocalizationItem[] items;
    }

    [System.Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
    }
}