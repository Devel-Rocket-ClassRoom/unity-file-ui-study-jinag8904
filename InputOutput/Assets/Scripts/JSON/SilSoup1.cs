using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class SomeClass
{
    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 pos;

    [JsonConverter(typeof(QuaternionConverter))]
    public Quaternion rot;

    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 scale;

    [JsonConverter(typeof(ColorConverter))]
    public Color color;

    public override string ToString() => $"{pos} / {rot} / {scale} / {color}";
}

public class SilSoup1 : MonoBehaviour
{
    public GameObject cube;

    public string fileName = "test.json";
    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);

    public JsonSerializerSettings jsonSettings = new();

    private void Awake()
    {
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
    }

    public void Save()
    {
        var obj = new SomeClass()
        {
            pos = cube.transform.position,
            rot = cube.transform.rotation,
            scale = cube.transform.localScale,
            color = cube.GetComponent<Renderer>().material.color
        };

        var json = JsonConvert.SerializeObject(obj, jsonSettings);
        
        File.WriteAllText(FileFullPath, json);
        Debug.Log($"저장: {json}");
    }

    public void Load()
    {
        var json = File.ReadAllText(FileFullPath);
        var obj = JsonConvert.DeserializeObject<SomeClass>(json, jsonSettings);

        cube.transform.position = obj.pos;
        cube.transform.rotation = obj.rot;
        cube.transform.localScale = obj.scale;
        cube.GetComponent<Renderer>().material.color = obj.color;

        Debug.Log($"로드: {json}");
    }
}