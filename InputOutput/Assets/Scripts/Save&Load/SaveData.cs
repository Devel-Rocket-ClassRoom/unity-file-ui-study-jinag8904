using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}

[Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1() => Version = 1;
    

    public override SaveData VersionUp()
    {
        var newSaveData = new SaveDataV2();
        newSaveData.Name = PlayerName;

        return newSaveData;
    }
}

[Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;

    public SaveDataV2() => Version = 2;

    public override SaveData VersionUp()
    {
        var newSaveData = new SaveDataV3();
        newSaveData.Name = Name;
        newSaveData.Gold = Gold;

        return newSaveData;
    }
}

[Serializable]
public class SaveDataV3 : SaveDataV2
{
    public List<string> ItemIDs = new();

    public SaveDataV3() => Version = 3;

    public override SaveData VersionUp()
    {
        throw new NotImplementedException();
    }
}