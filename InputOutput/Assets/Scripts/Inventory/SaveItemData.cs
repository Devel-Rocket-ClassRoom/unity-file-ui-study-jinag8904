using Newtonsoft.Json;
using System;

public class SaveItemData
{
    public Guid instnaceId { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData { get; set; }   // reference

    public DateTime creationTime { get; set; }

    public SaveItemData()
    {
        instnaceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }

    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new() { ItemData = DataTableManager.ItemTable.GetRandom() };
        //newItem.ItemData = DataTableManager.ItemTable.GetRandom();
        return newItem;
    }
}