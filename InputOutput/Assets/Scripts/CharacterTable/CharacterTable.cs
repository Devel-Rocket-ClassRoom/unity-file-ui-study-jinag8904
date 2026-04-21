using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CharacterData
{
    public string Id { get; set; }
    public Classes Class { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int AttackPower { get; set; }
    public int DefensePower { get; set; }
    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString() => $"{Id} / {Class} / {Name} / {Desc} / {AttackPower} / {DefensePower} / {Icon}";
}

public class CharacterTable : DataTable
{
    private readonly Dictionary<string, CharacterData> table = new();
    private List<string> keyList;

    public override void Load(string fileName)
    {
        table.Clear();

        string path = string.Format(FormatPath, fileName);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCSV<CharacterData>(textAsset.text);

        foreach (var item in list)
        {
            if (!table.ContainsKey(item.Id))
                table.Add(item.Id, item);

            else
                Debug.LogError("캐릭터 아이디 중복");
        }

        keyList = table.Keys.ToList();
    }

    public CharacterData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError("캐릭터 아이디 없음");
            return null;
        }

        return table[id];
    }

    public string GetRandomId()
    {
        System.Random rand = new();
        int index = rand.Next(table.Count);
        var randomPair = table.ElementAt(index);

        return randomPair.Key;
    }

    public CharacterData GetRandom()
    {
        return Get(keyList[UnityEngine.Random.Range(0, keyList.Count)]);
    }
}