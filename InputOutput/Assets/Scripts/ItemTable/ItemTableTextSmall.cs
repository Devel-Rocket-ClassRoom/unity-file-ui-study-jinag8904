using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTableTextSmall : ItemTableText
{
    public ItemTableTextBig bigText;

    public void OnClick()
    {
        bigText.SetData(id);
    }
}