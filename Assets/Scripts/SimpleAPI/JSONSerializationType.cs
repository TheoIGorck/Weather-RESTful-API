using Newtonsoft.Json;
using System;
using UnityEngine;

public class JSONSerializationType : ISerializeType
{
    public T Deserialize<T>(string text)
    {
        try
        {
            T result = JsonConvert.DeserializeObject<T>(text);
            Debug.Log($"Sucess: {text}");
            return result;
        }
        catch(Exception exception)
        {
            Debug.LogError($"Could not parse JSON {text}. {exception.Message}");
            return default;
        }
    }

    public string ContentType => "application/json";
}
