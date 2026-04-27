using UnityEngine;

public class UiEquipSlot : UIInvenSlot
{
    public UIInvenSlotList uiInvenSlotList;
    public UiCharacterInfo characterInfo;

    private void Awake()
    {
        slotIndex = 0;
    }

    public override void SetEmpty()
    {
        base.SetEmpty();
        nameText.text = "장비";
    }

    public override void SetItem(SaveItemData saveChData)
    {
        if (saveChData != null)
        {
            base.SetItem(saveChData);
            characterInfo.SaveEquipItem();
        }

        else
        {
            SetEmpty();
            characterInfo.ClearEquipItem();
        }
    }

    public void OnClick()
    {
        if (saveItemData != null)
        {
            SetEmpty();
            characterInfo.ClearEquipItem();
            return;
        }

        var item = uiInvenSlotList.GetSelectedItem();

        if (item == null || item.ItemData.Type != ItemTypes.Equip)
        {
            return;
        }

        else
        {
            SetItem(item);            
            characterInfo.SaveEquipItem();
        }
    }
}
