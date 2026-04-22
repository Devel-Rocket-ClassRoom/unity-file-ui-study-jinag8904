using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterInfo: MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";
    public static readonly string FormatStat = "{0}: {1} (+{2})";

    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public TextMeshProUGUI classText;
    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI defensePowerText;

    public UiEquipSlot equipSlot;
    public UiWeaponSlot weaponSlot;

    public void SetEmpty()
    {
        iconImage.sprite = Resources.Load<Sprite>($"Icon/Icon_Close01");
        nameText.text = string.Empty;
        descText.text = string.Empty;
        classText.text = string.Empty;
        attackPowerText.text = string.Empty;
        defensePowerText.text = string.Empty;

        equipSlot.SetEmpty();
        weaponSlot.SetEmpty();
    }

    public void SetSaveCharacterData(SaveCharacterData saveCharacterData)
    {
        CharacterData data = saveCharacterData.CharacterData;

        iconImage.sprite = data.SpriteIcon;
        nameText.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("NAME"), data.StringName);
        descText.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("DESC"), data.StringDesc);

        string id = data.Class.ToString().ToUpper();

        classText.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("CLASS"), DataTableManager.StringTable.Get(id));
        attackPowerText.text = string.Format(FormatStat, DataTableManager.StringTable.Get("AP"), data.AttackPower, (saveCharacterData.WeaponItem == null) ? 0 : saveCharacterData.WeaponItem.ItemData.Value);
        defensePowerText.text = string.Format(FormatStat, DataTableManager.StringTable.Get("DP"), data.DefensePower, (saveCharacterData.EquipItem == null) ? 0 : saveCharacterData.EquipItem.ItemData.Value);

        equipSlot.SetItem(saveCharacterData.EquipItem);
        weaponSlot.SetItem(saveCharacterData.WeaponItem);
    }
}