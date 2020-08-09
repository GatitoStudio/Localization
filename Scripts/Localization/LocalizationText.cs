using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizationText : MonoBehaviour
{
    [SerializeField]
    public string key;
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
        if(mText!=null)
            mText.text=localizationSystem.GetValue(key);
    }


}
