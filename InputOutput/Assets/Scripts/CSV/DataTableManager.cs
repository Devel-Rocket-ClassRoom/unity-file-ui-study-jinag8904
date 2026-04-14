using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);
    public static ItemTable ItemTable => Get<ItemTable>(DataTableIds.Item);

    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);

        var itemTable = new ItemTable();
        itemTable.Load(DataTableIds.Item);
        tables.Add(DataTableIds.Item, itemTable);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))    // 아이템별, String별 초기화 처리 필요할 듯
        {
            Init();
        }

        return tables[id] as T;
    }
}