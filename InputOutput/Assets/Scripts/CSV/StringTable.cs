using System.Collections.Generic;
using UnityEngine;

public class StringTable : DataTable
{
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }

    private readonly Dictionary<string, string> table = new();

    public override void Load(string fileName)
    {
        table.Clear();

        var path = string.Format(FormatPath, fileName);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<Data>(textAsset.text);

        foreach (var data in list)
        {
            if (!table.ContainsKey(data.Id))
                table.Add(data.Id, data.String);
            
            else            
                Debug.LogError($"키 중복: {data.Id}");           
        }
    }

    public static readonly string Unknown = "키 없음";

    public string Get(string key)
    {
        if (!table.ContainsKey(key)) return Unknown;

        return table[key];
    }
}