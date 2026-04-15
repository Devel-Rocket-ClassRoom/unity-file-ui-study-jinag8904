
using UnityEngine;
using System.Text;
using TMPro;

public class CharacterTableTextBig : CharacterTableText
{
    public TextMeshProUGUI descText;
    protected string characterDescId;
    protected string characterClassId;

    private int characterAP;
    private int characterDP;

    public override void OnChangedId()
    {
        base.OnChangedId();

        var characterData = DataTableManager.CharacterTable.Get(id);
        characterDescId = characterData.Desc;
        characterClassId = characterData.Class.ToString();
        characterAP = characterData.AttackPower;
        characterDP = characterData.DefensePower;

        StringBuilder sb = new();
        var stringData = DataTableManager.StringTable;

        sb.Append($"{stringData.Get(characterDescId)}");
        sb.Append($"\n{stringData.Get("Class")}: {stringData.Get(characterClassId)}");
        sb.Append($"\n{stringData.Get("AP")}: {characterAP}");
        sb.Append($"\n{stringData.Get("DP")}: {characterDP}");

        descText.text = sb.ToString();
    }

    public void SetData(string id)
    {
        this.id = id;
        OnChangedId();
    }
}