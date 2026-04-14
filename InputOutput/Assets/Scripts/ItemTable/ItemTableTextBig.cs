using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTableTextBig : ItemTableText
{
    public TextMeshProUGUI descText;
    public string itemDescId;

    public override void OnChangedId()
    {
        base.OnChangedId();
        itemDescId = DataTableManager.ItemTable.Get(id).Desc;
        descText.text = DataTableManager.StringTable.Get(itemDescId);
    }

    public void SetData(string id)
    {
        this.id = id;
        OnChangedId();
    }
}