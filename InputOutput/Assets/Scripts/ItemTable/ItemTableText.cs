using UnityEngine;
using UnityEngine.UI;

public class ItemTableText : StringTableText
{
    public Image Icon;

    public override void OnValidate()
    {
        OnChangedId();
    }

    public override void OnChangedId()
    {
        ItemData data = DataTableManager.ItemTable.Get(id);
        if (data == null) return;

        Icon.sprite = data.SpriteIcon;
        itemNameId = data.Name;

        base.OnChangedId();
    }

    public void SetEmpty()
    {
        Icon.sprite = null;
        text.text = string.Empty;
    }
}