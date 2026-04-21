using System;
using System.Collections;
using UnityEngine;

public class SaveCharacterData
{
    public Guid instanceId { get; set; }
    public DateTime creationTime { get; set; }

    public CharacterData CharacterData { get; set; }

    public SaveCharacterData()
    {
        instanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }

    public static SaveCharacterData GetRandomCharacter()
    {
        SaveCharacterData newCharacter = new() { CharacterData = DataTableManager.CharacterTable.GetRandom() };
        return newCharacter;
    }
}