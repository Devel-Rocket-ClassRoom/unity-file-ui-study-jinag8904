using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTableTextBig : ItemTableText
{
    public TextMeshProUGUI descText;

    public override void OnChangedId()
    {
        base.OnChangedId();
        descText.text = DataTableManager.ItemTable.Get(id).Desc;
    }

    public void UpdateItem(string id)
    {
        this.id = id;
        OnChangedId();
    }
}