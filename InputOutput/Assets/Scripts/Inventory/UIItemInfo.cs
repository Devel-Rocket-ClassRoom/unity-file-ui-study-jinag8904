using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";

    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI costText;

    public void SetEmpty()
    {
        iconImage.sprite = null;
        nameText.text = string.Empty;
        descText.text = string.Empty;
        typeText.text = string.Empty;
        valueText.text = string.Empty;
        costText.text = string.Empty;
    }

    public void SetSaveItemData(SaveItemData saveItemData)
    {
        ItemData data = saveItemData.ItemData;

        iconImage.sprite = data.SpriteIcon;
        nameText.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("NAME"), data.StringName);
        descText.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("DESC"), data.StringDesc);

        string id = data.Type.ToString().ToUpper();

        typeText.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("TYPE"), DataTableManager.StringTable.Get(id));
        valueText.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("VALUE"), data.Value);
        costText.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("COST"), data.Cost);
    }
}