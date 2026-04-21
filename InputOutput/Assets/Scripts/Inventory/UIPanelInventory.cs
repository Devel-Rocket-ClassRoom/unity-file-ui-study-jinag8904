using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UIInvenSlotList uiInvenSlotList;

    public TextMeshProUGUI saveBtnText;
    public TextMeshProUGUI loadBtnText;
    public TextMeshProUGUI createBtnText;
    public TextMeshProUGUI deleteBtnText;

    private void OnEnable()
    {
        OnLoad();
        OnChangeSorting(sorting.value);
        OnChangeFiltering(filtering.value);

        saveBtnText.text = DataTableManager.StringTable.Get("SAVE");
        loadBtnText.text = DataTableManager.StringTable.Get("LOAD");
        createBtnText.text = DataTableManager.StringTable.Get("CREATEITEM");
        deleteBtnText.text = DataTableManager.StringTable.Get("DELETEITEM");

        List<string> sortingOptions = new()
        {
            DataTableManager.StringTable.Get("DATEASCENDING"),
            DataTableManager.StringTable.Get("DATEDESCENDING"),
            DataTableManager.StringTable.Get("NAMEASCENDING"),
            DataTableManager.StringTable.Get("NAMEDESCENDING"),
            DataTableManager.StringTable.Get("COSTASCENDING"),
            DataTableManager.StringTable.Get("COSTASCENDING"),
            DataTableManager.StringTable.Get("VALUEASCENDING"),
            DataTableManager.StringTable.Get("VALUEASCENDING")
        };

        for (int i = 0; i < sorting.options.Count; i++)
        {
            sorting.options[i].text = sortingOptions[i];
        }

        List<string> filteringOptions = new()
        {
            DataTableManager.StringTable.Get("NONE"),
            DataTableManager.StringTable.Get("WEAPON"),
            DataTableManager.StringTable.Get("EQUIP"),
            DataTableManager.StringTable.Get("CONSUMABLE"),
            DataTableManager.StringTable.Get("NONCONSUMABLE")
        };

        for (int i = 0; i < filtering.options.Count; i++)
        {
            filtering.options[i].text = filteringOptions[i];
        }
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UIInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UIInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }

    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }

    public void OnDeleteItem()
    {
        uiInvenSlotList.RemoveItem();
    }
}