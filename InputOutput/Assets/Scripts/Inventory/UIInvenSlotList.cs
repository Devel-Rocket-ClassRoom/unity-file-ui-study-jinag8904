using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInvenSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
        CostAscending,
        CostDescending,
        ValueAscending,
        ValueDescending
    }

    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable
    }

    public readonly Comparison<SaveItemData>[] comparisons =
    {
        (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs, rhs) => rhs.creationTime.CompareTo(lhs.creationTime),
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
        
        (lhs, rhs) => lhs.ItemData.Cost.CompareTo(rhs.ItemData.Cost),
        (lhs, rhs) => rhs.ItemData.Cost.CompareTo(lhs.ItemData.Cost),
        (lhs, rhs) => lhs.ItemData.Value.CompareTo(rhs.ItemData.Value),
        (lhs, rhs) => rhs.ItemData.Value.CompareTo(lhs.ItemData.Value),
    };

    public readonly Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemTypes.Weapon,
        (x) => x.ItemData.Type == ItemTypes.Equip,
        (x) => x.ItemData.Type == ItemTypes.Consumable,
        (x) => x.ItemData.Type != ItemTypes.Consumable
    };

    public UIInvenSlot InvenSlotPrefab;
    public ScrollRect scrollRect;

    public UIItemInfo uiItemInfo;

    private List<UIInvenSlot> uiSlotList = new();

    private List<SaveItemData> saveItemDataList = new();    

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    public TMP_Dropdown sortingDropdown;
    public TMP_Dropdown filteringDropdown;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;

    private string sortingOptionPath;    // 옵션 저장
    private string filteringOptionPath;    // 옵션 저장

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);

        sortingOptionPath = Path.Combine(Application.persistentDataPath, "UiInventory", "sortingOptions.json");
        filteringOptionPath = Path.Combine(Application.persistentDataPath, "UiInventory", "filteringOptions.json");

        var folderPath = Path.Combine(Application.persistentDataPath, "UiInventory");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        try
        {
            var jsonSort = File.ReadAllText(sortingOptionPath);
            var jsonFilter = File.ReadAllText(filteringOptionPath);

            Sorting = JsonConvert.DeserializeObject<SortingOptions>(jsonSort);
            Filtering = JsonConvert.DeserializeObject<FilteringOptions>(jsonFilter);
        }

        catch
        {
            Debug.Log("불러오기 중 예외 발생");
        }

        sortingDropdown.value = (int)Sorting;
        filteringDropdown.value = (int)Filtering;
    }

    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(InvenSlotPrefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.saveItemData);
                });

                uiSlotList.Add(newSlot);
            }            
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }

            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    private void OnSelectSlot(SaveItemData saveItemData)
    {
        uiItemInfo.SetSaveItemData(saveItemData);
    }

    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }

    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].saveItemData);
        uiItemInfo.SetEmpty();
        UpdateSlots();
    }

    public void OnDisable()
    {
        var folderPath = Path.Combine(Application.persistentDataPath, "UiInventory");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        try
        {
            var jsonSort = JsonConvert.SerializeObject(sorting);
            var jsonFilter = JsonConvert.SerializeObject(filtering);

            File.WriteAllText(sortingOptionPath, jsonSort);
            File.WriteAllText(filteringOptionPath, jsonFilter);
        }

        catch
        {
            Debug.Log("저장 중 예외 발생");
        }
    }
}