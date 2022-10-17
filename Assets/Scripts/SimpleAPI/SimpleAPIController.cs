using UnityEngine;

public class SimpleAPIController : MonoBehaviour
{
    public string Url; 

    [ContextMenu("Get Data")]
    public async void GetData()
    {
        HttpClient client = new HttpClient(new JSONSerializationType());
        User data = await client.Get<User>(Url);

        Debug.Log(data.ID);
    } 
}
