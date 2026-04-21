using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharacterSlotList : MonoBehaviour
{
    public UiEquipSlot equipSlot;
    public UiWeaponSlot weaponSlot;

    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending
    }

    public enum FilteringOptions
    {
        None,
        Warrior,
        Archer,
        Healer
    }

    public readonly Comparison<SaveCharacterData>[] comparisons =
{
        (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs, rhs) => rhs.creationTime.CompareTo(lhs.creationTime),
        (lhs, rhs) => lhs.CharacterData.StringName.CompareTo(rhs.CharacterData.StringName),
        (lhs, rhs) => rhs.CharacterData.StringName.CompareTo(lhs.CharacterData.StringName)
    };

    public readonly Func<SaveCharacterData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.CharacterData.Class == Classes.Warrior,
        (x) => x.CharacterData.Class == Classes.Archer,
        (x) => x.CharacterData.Class == Classes.Healer
    };

    public UiCharacterSlot CharacterSlotPrefab;
    public ScrollRect scrollRect;

    public UiCharacterInfo uiCharacterInfo;

    private List<UiCharacterSlot> uiSlotList = new();
    private List<SaveCharacterData> saveCharacterDataList = new();

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
    public UnityEvent<SaveCharacterData> onSelectSlot;

    private string sortingOptionPath;    // 옵션 저장
    private string filteringOptionPath;    // 옵션 저장

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);

        sortingOptionPath = Path.Combine(Application.persistentDataPath, "UiCharacter", "sortingOptions.json");
        filteringOptionPath = Path.Combine(Application.persistentDataPath, "UiCharacter", "filteringOptions.json");

        var folderPath = Path.Combine(Application.persistentDataPath, "UiCharacter");

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
        var list = saveCharacterDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(CharacterSlotPrefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    Debug.Log($"selectedSlotIndex: {selectedSlotIndex}");
                    onSelectSlot.Invoke(newSlot.saveCharacterData);
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

    private void OnSelectSlot(SaveCharacterData saveCharacterData)
    {
        uiCharacterInfo.SetSaveCharacterData(saveCharacterData);

        if (saveCharacterData.EquipItemData != null)
        {
            SetEquip(saveCharacterData.EquipItemData);
        }

        else
        {
            equipSlot.SetEmpty();
        }

        if (saveCharacterData.WeaponItemData != null)
        {
            SetEquip(saveCharacterData.WeaponItemData);
        }

        else
        {
            weaponSlot.SetEmpty();
        }
    }

    public void SetSaveCharacterDataList(List<SaveCharacterData> source)
    {
        saveCharacterDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveCharacterData> GetSaveCharacterDataList()
    {
        return saveCharacterDataList;
    }

    public void AddRandomCharacter()
    {
        saveCharacterDataList.Add(SaveCharacterData.GetRandomCharacter());
        UpdateSlots();
    }

    public void RemoveCharacter()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        Debug.Log(selectedSlotIndex);
        saveCharacterDataList.Remove(uiSlotList[selectedSlotIndex].saveCharacterData);
        uiCharacterInfo.SetEmpty();
        UpdateSlots();
    }

    public void OnDisable()
    {
        var folderPath = Path.Combine(Application.persistentDataPath, "UiCharacter");

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

    public void SetEquip(SaveItemData equip)
    {
        equipSlot.SetItem(equip);
        saveCharacterDataList[selectedSlotIndex].EquipItemData = equip;
    }

    public void SetWeapon(SaveItemData weapon)
    {
        weaponSlot.SetItem(weapon);
        saveCharacterDataList[selectedSlotIndex].WeaponItemData = weapon;
    }
}