using UnityEngine;

public class UiWeaponSlot : UIInvenSlot
{
    public UIInvenSlotList UIInvenSlotList;

    private void Awake()
    {
        slotIndex = 1;
    }

    public override void SetEmpty()
    {
        base.SetEmpty();
        nameText.text = "무기";
    }

    public void OnClick()
    {
        if (saveItemData != null)
        {
            SetEmpty();
            return;
        }

        var item = UIInvenSlotList.GetSelectedItem();

        if (item == null || item.ItemData.Type != ItemTypes.Weapon)
        {
            return;
        }

        else
        {
            SetItem(item);
        }
    }
}
