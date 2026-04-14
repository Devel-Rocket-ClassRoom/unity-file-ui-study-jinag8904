
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

        characterDescId = DataTableManager.CharacterTable.Get(id).Desc;
        characterClassId = DataTableManager.CharacterTable.Get(id).Class.ToString();

        characterAP = DataTableManager.CharacterTable.Get(id).AttackPower;
        characterDP = DataTableManager.CharacterTable.Get(id).DefensePower;

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