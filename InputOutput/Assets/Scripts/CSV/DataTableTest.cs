using UnityEngine;

public class DataTableTest : MonoBehaviour
{
    StringTable table = new();

    public string NameStringTableKr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    public void Korean()
    {
        table.Load(NameStringTableKr);
        Debug.Log(table.Get("YOU DIE"));
    }

    public void English()
    {
        table.Load(NameStringTableEn);
        Debug.Log(table.Get("YOU DIE"));
    }

    public void Japanese()
    {
        table.Load(NameStringTableJp);
        Debug.Log(table.Get("YOU DIE"));
    }
}