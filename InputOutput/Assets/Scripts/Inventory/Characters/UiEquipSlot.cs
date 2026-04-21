using UnityEngine;

public class UiEquipSlot : UIInvenSlot
{
    UIInvenSlotList UIInvenSlotList = new();

    private void Awake()
    {
        slotIndex = 0;
    }

    public void OnClick()
    {
        if (saveItemData != null)
        {
            SetEmpty();
            return;
        }

        var item = UIInvenSlotList.GetSelectedItem();

        if (item == null || item.ItemData.Type != ItemTypes.Equip)
        {
            return;
        }

        else
        {
            SetItem(item);
        }
    }
}
