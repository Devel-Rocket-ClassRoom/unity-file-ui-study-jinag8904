using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterSlot : MonoBehaviour
{
    public int slotIndex = -1;

    public Image iconImage;
    public TextMeshProUGUI nameText;

    public SaveCharacterData saveCharacterData { get; private set; }

    public Button button;

    public void SetEmpty()
    {
        iconImage.sprite = null;
        nameText.text = string.Empty;
        saveCharacterData = null;
    }

    public void SetItem(SaveCharacterData data)
    {
        saveCharacterData = data;

        iconImage.sprite = saveCharacterData.CharacterData.SpriteIcon;
        nameText.text = saveCharacterData.CharacterData.StringName;
        
        if (data.EquipItem != null)
            Debug.Log(data.EquipItem.ItemData.Name);

        if (data.WeaponItem != null)
            Debug.Log(data.WeaponItem.ItemData.Name);
    }
}
