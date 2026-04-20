using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
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

            var itemList = SaveLoadManager.Data.ItemList;

            foreach (var item in itemList)
            {
                Debug.Log($"{item.ItemData.Name}");  // 하나씩 출력
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new() 
            { 
                Name = "TEST1234", 
                Gold = 4321, 
                ItemList = SaveLoadManager.Data.ItemList
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

            var itemList = SaveLoadManager.Data.ItemList;

            foreach (var item in itemList)
            {
                Debug.Log($"{item.ItemData.Name}");  // 하나씩 출력
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //var newItemId = DataTableManager.ItemTable.GetRandomId();
            SaveLoadManager.Data.ItemList.Add(new SaveItemData());

            Debug.Log($"아이템 추가: {SaveLoadManager.Data.ItemList[SaveLoadManager.Data.ItemList.Count -1]}");
        }
    }
}
