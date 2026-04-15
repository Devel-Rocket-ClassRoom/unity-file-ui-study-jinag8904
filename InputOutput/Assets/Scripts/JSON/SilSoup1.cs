using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

    public override string ToString() => $"{pos} / {rot} / {scale} / {color}";
}

[Serializable]
public class ObjectSave
{
    public PrimitiveType type;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

    public override string ToString() => $"{type} / {pos} / {rot} / {scale} / {color}";
}

public class SilSoup1 : MonoBehaviour
{
    public List<GameObject> objects;

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
        var saveObjs = new List<ObjectSave>();

        for (int i = 0; i < objects.Count; i++)
        {
            var typeName = objects[i].GetComponent<MeshFilter>().sharedMesh.name;
            bool saveType = Enum.TryParse<PrimitiveType>(typeName, true, out PrimitiveType result);

            var saveObj = new ObjectSave()
            {
                type = result,
                pos = objects[i].transform.position,
                rot = objects[i].transform.rotation,
                scale = objects[i].transform.localScale,
                color = objects[i].GetComponent<Renderer>().material.color
            };
            
            saveObjs.Add(saveObj);
        }

        var json = JsonConvert.SerializeObject(saveObjs, jsonSettings);
        File.WriteAllText(FileFullPath, json);
    }

    public void Load()
    {
        Clear();

        var jsons = File.ReadAllText(FileFullPath);
        var loadObj = JsonConvert.DeserializeObject<List<ObjectSave>>(jsons, jsonSettings);

        for (int i = 0; i < loadObj.Count; i++)
        {
            var typeName = loadObj[i].type;

            var obj = GameObject.CreatePrimitive(typeName);
            obj.transform.position = loadObj[i].pos;
            obj.transform.rotation = loadObj[i].rot;
            obj.transform.localScale = loadObj[i].scale;
            obj.GetComponent<Renderer>().material.color = loadObj[i].color;
                        
            objects.Add(obj);
        }

        Debug.Log($"로드: {objects.Count}개");
    }

    public void Create()
    {
        for (int i = 0; i < 10; i++)    // 10개씩
        {
            System.Random rand = new();
            var newObj = GameObject.CreatePrimitive((PrimitiveType)rand.Next(0, 4));
            newObj.transform.position = new Vector3(rand.Next(-10, 11), rand.Next(-3, 4), transform.position.z);
            newObj.transform.rotation = UnityEngine.Random.rotation;
            newObj.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.5f, 3f);
            newObj.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();

            objects.Add(newObj);
        }
    }

    public void Clear()
    {
        foreach (var obj in objects)
        {
            Destroy(obj);
        }

        objects.Clear();
    }
}