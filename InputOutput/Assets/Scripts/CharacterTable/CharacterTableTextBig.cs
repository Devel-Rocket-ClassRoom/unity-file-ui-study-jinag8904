using TMPro;

public class CharacterTableTextBig : CharacterTableText
{
    public TextMeshProUGUI descText;
    public string characterDescId;

    public override void OnChangedId()
    {
        base.OnChangedId();
        characterDescId = DataTableManager.CharacterTable.Get(id).Desc;
        descText.text = DataTableManager.StringTable.Get(characterDescId);
    }

    public void SetData(string id)
    {
        this.id = id;
        OnChangedId();
    }
}