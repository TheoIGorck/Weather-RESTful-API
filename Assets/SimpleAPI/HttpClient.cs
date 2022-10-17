using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HttpClient 
{
    private readonly ISerializeType _serializeType;

    public HttpClient(ISerializeType serializeType)
    {
        _serializeType = serializeType;
    }

    public async Task<TResultType> Get<TResultType>(string url)
    {
        try
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type", _serializeType.ContentType);

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            while(!operation.isDone)
            {
                await Task.Yield();
            }

            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Failed: {request.error}");
            }

            string response = request.downloadHandler.text;
            TResultType result = _serializeType.Deserialize<TResultType>(response);
            return result;
        }
        catch(Exception exception)
        {
            Debug.LogError($"{nameof(Get)} failed: {exception.Message}");
            return default;
        }
    }
}
