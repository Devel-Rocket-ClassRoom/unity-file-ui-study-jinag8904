using UnityEngine;
using TMPro;

public class StringTableText : MonoBehaviour
{    
    public string id;
    public string itemNameId;

    public TextMeshProUGUI text;

    public LocalizationText localizationText;

    private void OnEnable()
    {
        localizationText.OnLanguageChanged += OnChangedId;
    }

    private void OnDisable()
    {
        localizationText.OnLanguageChanged -= OnChangedId;
    }

    public virtual void OnValidate()
    {
        localizationText.OnLanguageChanged += OnChangedId;
        OnChangedId();
    }

    private void Start()
    {
        OnChangedId();
    }

    public virtual void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(itemNameId);
    }
}
