using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "BDD", menuName = "Traduction", order = 0)]
public class LocalizationSystem : ScriptableObject
{
    public string currentLang;
    public Dictionary<string, Dictionary<string, string>> langagesTraduction;
    public delegate void OnValueChange();
    public event OnValueChange onValueChange;

    public string CurrentLang
    {
        get
        {
            return currentLang;
        }
        set
        {
            currentLang = value;
            if(onValueChange!=null)
                onValueChange.Invoke();
        }
    }
    public string GetValue(string key)
    {
        string value = null;
        if (langagesTraduction[CurrentLang].TryGetValue("[" + key + "]", out value))
        {
            return value;
        }
        else
        {
            return key + " [NOT FOUND]";
        }
    }
    public string GetValueFormat(string key, params string[] values)
    {
        string value = null;
        value = GetValue(key);
        return string.Format(value, values);
    }
}
