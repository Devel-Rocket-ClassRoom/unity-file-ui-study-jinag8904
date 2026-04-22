using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInvenSlot : MonoBehaviour
{
    public int slotIndex = -1;

    public Image iconImage;
    public TextMeshProUGUI nameText;

    public SaveItemData saveItemData { get; set; }

    public Button button;

    public virtual void SetEmpty()
    {
        iconImage.sprite = Resources.Load<Sprite>($"Icon/Icon_Close01");
        nameText.text = string.Empty;
        saveItemData = null;
    }

    public virtual void SetItem(SaveItemData data)
    {
        saveItemData = data;

        iconImage.sprite = saveItemData.ItemData.SpriteIcon;
        nameText.text = saveItemData.ItemData.StringName;
    }
}