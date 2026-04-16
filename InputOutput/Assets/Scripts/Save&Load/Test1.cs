using UnityEngine;

public class Test1 : MonoBehaviour
{
    public SaveLoadManager.SaveMode saveMode;

    private void OnValidate()
    {
        SaveLoadManager.Mode = saveMode;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))   // 현 상태 출력
        {
            Debug.Log(SaveLoadManager.Data.Name);
            Debug.Log(SaveLoadManager.Data.Gold);

            var itemIDS = SaveLoadManager.Data.ItemIDs;
            for (int i = 0; i < itemIDS.Count; i++)
            {
                Debug.Log($"{i + 1}번 아이템: {DataTableManager.ItemTable.Get(itemIDS[i])}");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new() 
            { 
                Name = "TEST1234", 
                Gold = 4321, 
                ItemIDs = SaveLoadManager.Data.ItemIDs
            };

            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!SaveLoadManager.Load())
            {
                Debug.LogError("세이브 파일 없음");
                return;
            }

            Debug.Log(SaveLoadManager.Data.Name);
            Debug.Log(SaveLoadManager.Data.Gold);

            var itemIDS = SaveLoadManager.Data.ItemIDs;
            for (int i = 0; i < itemIDS.Count; i++)
            {
                Debug.Log($"{i+1}번 아이템: {DataTableManager.ItemTable.Get(itemIDS[i]).Name}");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var newItemId = DataTableManager.ItemTable.GetRandomId();
            SaveLoadManager.Data.ItemIDs.Add(newItemId);

            Debug.Log($"아이템 추가: {newItemId}");
        }
    }
}
