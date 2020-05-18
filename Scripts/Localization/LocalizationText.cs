using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizationText : MonoBehaviour
{
    [SerializeField]
    public string key;
    [SerializeField]
    private TextMeshProUGUI mText;
    [SerializeField]
    LocalizationSystem localizationSystem=null;
    private void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
        localizationSystem.onValueChange += Load;
        Load();
    }
    public void Load()
    {
        mText.text=localizationSystem.GetValue(key);
    }


}
