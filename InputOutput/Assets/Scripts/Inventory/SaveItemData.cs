using Newtonsoft.Json;
using System;

public class SaveItemData
{
    public Guid instanceId { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData { get; set; }   // reference

    public DateTime creationTime { get; set; }

    public SaveItemData()
    {
        instanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }

    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new() { ItemData = DataTableManager.ItemTable.GetRandom() };
        return newItem;
    }

    public override string ToString()
    {
        return $"instanceId: {instanceId}\ncreationTime: {creationTime}\nItemData.Id: {ItemData.Id}";
    }
}