using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;

    public Dictionary<string, int> scores = new()
    {
        { "stage1", 100 },
        { "stage2", 200 }
    };
}

public class JsonUtilityTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))   // 저장
        {
            var obj = new PlayerInfo()
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)               
            };

            var folderPath = Path.Combine(Application.persistentDataPath, "JsonTest");
            
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, "player.json");
            var json = JsonUtility.ToJson(obj, prettyPrint: true);

            File.WriteAllText(filePath, json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))   // 로드
        {
            var path = Path.Combine(Application.persistentDataPath, "JsonTest", "player.json");
            var json = File.ReadAllText(path);

            //var obj = JsonUtility.FromJson<PlayerInfo>(json);
            var obj = new PlayerInfo();
            JsonUtility.FromJsonOverwrite(json, obj);   // 덮어쓰기

            Debug.Log(json);
            Debug.Log($"{obj.playerName} / {obj.health}");
        }
    }
}
