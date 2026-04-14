using UnityEngine.UI;

public class CharacterTableText : StringTableText
{
    public Image Icon;
    public string characterNameId;

    public override void OnValidate()
    {
        localizationText.OnLanguageChanged += OnChangedId;
        OnChangedId();
    }

    public override void OnChangedId()
    {
        CharacterData data = DataTableManager.CharacterTable.Get(id);
        if (data == null) return;

        Icon.sprite = data.SpriteIcon;
        characterNameId = data.Name;

        text.text = DataTableManager.StringTable.Get(characterNameId);
    }

    public void SetEmpty()
    {
        Icon.sprite = null;
        text.text = string.Empty;
    }
}