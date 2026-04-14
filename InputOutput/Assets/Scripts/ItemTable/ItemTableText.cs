using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTableText : MonoBehaviour
{
    public string id;
    public Image Icon;
    public LocalizationText localizationText;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        localizationText.OnLanguageChanged += OnChangedId;
    }

    private void OnDisable()
    {
        localizationText.OnLanguageChanged -= OnChangedId;
    }

    private void OnValidate()
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
        Icon.sprite = DataTableManager.ItemTable.Get(id).SpriteIcon;
        text.text = DataTableManager.ItemTable.Get(id).Name;
    }
}
