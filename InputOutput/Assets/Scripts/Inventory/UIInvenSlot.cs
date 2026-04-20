using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInvenSlot : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;

    public SaveItemData saveItemData { get; private set; }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        SetEmpty();
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        var saveItemData = new SaveItemData();
    //        saveItemData.ItemData = DataTableManager.ItemTable.Get("Item1");

    //        SetItem(saveItemData);
    //    }
    //}

    public void SetEmpty()
    {
        iconImage.sprite = null;
        nameText.text = string.Empty;
        saveItemData = null;
    }

    public void SetItem(SaveItemData data)
    {
        saveItemData = data;

        iconImage.sprite = saveItemData.ItemData.SpriteIcon;
        nameText.text = saveItemData.ItemData.StringName;
    }
}