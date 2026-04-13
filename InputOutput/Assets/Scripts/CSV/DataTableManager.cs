using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);

    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Init();
        }

        return tables[id] as T;
    }
}