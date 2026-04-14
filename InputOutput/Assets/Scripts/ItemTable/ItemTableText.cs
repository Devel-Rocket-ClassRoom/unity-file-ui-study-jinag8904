using UnityEngine.UI;

public class ItemTableText : StringTableText
{
    public Image Icon;
    public string itemNameId;

    public override void OnValidate()
    {
        localizationText.OnLanguageChanged += OnChangedId;
        OnChangedId();
    }

    public override void OnChangedId()
    {
        ItemData data = DataTableManager.ItemTable.Get(id);
        if (data == null) return;

        Icon.sprite = data.SpriteIcon;
        itemNameId = data.Name;

        text.text = DataTableManager.StringTable.Get(itemNameId);
    }

    public void SetEmpty()
    {
        Icon.sprite = null;
        text.text = string.Empty;
    }
}