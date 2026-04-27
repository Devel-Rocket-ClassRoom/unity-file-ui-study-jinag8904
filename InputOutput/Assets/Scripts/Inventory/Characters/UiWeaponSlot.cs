using UnityEngine;

public class UiWeaponSlot : UIInvenSlot
{
    public UIInvenSlotList UIInvenSlotList;
    public UiCharacterInfo characterInfo;

    private void Awake()
    {
        slotIndex = 1;
    }

    public override void SetEmpty()
    {
        base.SetEmpty();
        nameText.text = "무기";
    }

    public override void SetItem(SaveItemData saveChData)
    {
        if (saveChData != null)
        {
            base.SetItem(saveChData);
            characterInfo.SaveWeaponItem();
        }

        else
        {
            SetEmpty();
            characterInfo.ClearWeaponItem();
        }
    }

    public void OnClick()
    {
        if (saveItemData != null)
        {
            SetEmpty();
            characterInfo.ClearWeaponItem();
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
            characterInfo.SaveWeaponItem();
        }
    }
}
