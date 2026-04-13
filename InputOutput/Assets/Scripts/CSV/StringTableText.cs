using UnityEngine;
using TMPro;

public class StringTableText : MonoBehaviour
{    
    public string id;
    public TextMeshProUGUI text;

    private void OnValidate()
    {
        OnChangedId();
    }

    private void Start()
    {
        OnChangedId();
    }

    public void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }
}
