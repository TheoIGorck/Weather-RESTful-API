using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LocationGetter : MonoBehaviour
{   
    public LocationData Location;
    public WeatherGetter WeatherGetter;
    public TextMeshProUGUI LocationText;
    /*public float latitude;
    public float longitude;*/

    private void Start()
    {
        StartCoroutine(GetLocationCoroutine());
    }

    private IEnumerator GetLocationCoroutine()
    {
        UnityWebRequest request = new UnityWebRequest("http://ip-api.com/json/")
        {
            //Stores received data in a native byte buffer
            downloadHandler = new DownloadHandlerBuffer()
        };

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            yield break;
        }

        Location = JsonUtility.FromJson<LocationData>(request.downloadHandler.text);
        LocationText.text = Location.city + ", " + Location.country;
        WeatherGetter.Begin(Location);
    }
}
