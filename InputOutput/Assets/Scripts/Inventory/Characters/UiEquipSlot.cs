using UnityEngine;

public class UiEquipSlot : UIInvenSlot
{
    public UIInvenSlotList UIInvenSlotList;

    private void Awake()
    {
        slotIndex = 0;
    }

    public override void SetEmpty()
    {
        base.SetEmpty();
        nameText.text = "장비";
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
