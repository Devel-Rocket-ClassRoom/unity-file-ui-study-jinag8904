using UnityEngine;

public class CharacterTableTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(DataTableManager.CharacterTable.Get("Character1"));
        }
    }
}