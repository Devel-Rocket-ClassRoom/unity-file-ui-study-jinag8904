using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;

    [JsonConverter(typeof(Vector3Converter))]   // 2번: Attribute 사용, 해당 필드는 해당 컨버터로 컨버팅하게 됨.
    public Vector3 position;

    public override string ToString() => $"{playerName} / {lives} / {health} / {position}";
}

public class JsonTest1 : MonoBehaviour
{
    // 3번: settings에 추가
    //private JsonSerializerSettings jsonSetting = new();

    //private void Awake()
    //{
    //    jsonSetting.Converters.Add(new Vector3Converter());
    //    jsonSetting.Formatting = Formatting.Indented;
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))   // 저장
        {
            var obj = new PlayerState()
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
            };

            var folderPath = Path.Combine(Application.persistentDataPath, "JsonTest");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, "player.json");

            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            //var json = JsonConvert.SerializeObject(obj, Formatting.Indented, new Vector3Converter()); // 1번: SerializeObject의 인수로 컨버터를 넘겨줌.
            //var json = JsonConvert.SerializeObject(obj, jsonSetting);   // 3번

            File.WriteAllText(filePath, json);

            Debug.Log("저장됨");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))   // 로드
        {
            var path = Path.Combine(Application.persistentDataPath, "JsonTest", "player.json");
            var json = File.ReadAllText(path);

            var obj = JsonConvert.DeserializeObject<PlayerState>(json);
            //var obj = JsonConvert.DeserializeObject<PlayerState>(json, new Vector3Converter()); // 2번
            //var obj = JsonConvert.DeserializeObject<PlayerState>(json, jsonSetting); // 3번

            Debug.Log("로드됨");
            Debug.Log(json);
            Debug.Log($"{obj.playerName} / {obj.health}");
        }
    }
}
