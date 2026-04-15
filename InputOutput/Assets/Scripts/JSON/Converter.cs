using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;

public class Vector3Converter: JsonConverter<Vector3>
{
    public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("X"); writer.WriteValue(value.x);
        writer.WritePropertyName("Y"); writer.WriteValue(value.y);
        writer.WritePropertyName("Z"); writer.WriteValue(value.z);

        writer.WriteEndObject();
    }

    public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var v = Vector3.zero;

        var jObj = JObject.Load(reader);
        v.x = (float)jObj["X"];
        v.y = (float)jObj["Y"];
        v.z = (float)jObj["Z"];

        return v;
    }
}

public class QuaternionConverter : JsonConverter<Quaternion>
{
    public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("X"); writer.WriteValue(value.x);
        writer.WritePropertyName("Y"); writer.WriteValue(value.y);
        writer.WritePropertyName("Z"); writer.WriteValue(value.z);
        writer.WritePropertyName("W"); writer.WriteValue(value.w);

        writer.WriteEndObject();
    }

    public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var q = Quaternion.identity;

        var jObj = JObject.Load(reader);
        q.x = (float)jObj["X"];
        q.y = (float)jObj["Y"];
        q.z = (float)jObj["Z"];
        q.w = (float)jObj["W"];

        return q;
    }
}

public class ColorConverter : JsonConverter<Color>
{
    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("R"); writer.WriteValue(value.r);
        writer.WritePropertyName("G"); writer.WriteValue(value.g);
        writer.WritePropertyName("B"); writer.WriteValue(value.b);
        writer.WritePropertyName("A"); writer.WriteValue(value.a);

        writer.WriteEndObject();
    }

    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var c = Color.white;

        var jObj = JObject.Load(reader);
        c.r = (float)jObj["R"];
        c.g = (float)jObj["G"];
        c.b = (float)jObj["B"];
        c.a = (float)jObj["A"];

        return c;
    }
}