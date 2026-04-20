using UnityEngine;
using UnityEngine.UI;

public class UIInvenSlotList : MonoBehaviour
{
    public UIInvenSlot InvenSlotPrefab;
    public ScrollRect scrollRect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < 10; i++)
            {
                var randSaveItemData = SaveItemData.GetRandomItem();
                var newInven = Instantiate(InvenSlotPrefab, scrollRect.content);
                newInven.SetItem(randSaveItemData);
            }
        }
    }
}