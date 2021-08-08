using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizationTextFormatExemple : MonoBehaviour
{
    [SerializeField]
    private string key;
    [SerializeField]
    private string prenom;
    [SerializeField]
    private LocalizationSystem localizationSystem = null;
    private TextMeshProUGUI mText;
    // Start is called before the first frame update
    //Il faut lire le fichier mot dans l'exemple.
    //HELLO deux a comme traduction "je m'appelle {0}
    //Il a donc besoin d'un paramètre 
    //La fonction GetValueFormat(key, parametre[]) permet d'ajouter un nombre n de paramètre pour la traduction;
    //IL fonctionne avec string.Format (voir la doc)
    private void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
        localizationSystem.onValueChange += Load;
        Load();
    }
    private void Load()
    {
        if (mText != null)
            mText.text = localizationSystem.GetValueFormat(key, prenom);
    }
    private void OnDestroy() => localizationSystem.onValueChange -= Load;

}
