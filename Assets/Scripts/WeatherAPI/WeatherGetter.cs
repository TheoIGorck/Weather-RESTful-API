using Newtonsoft.Json;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherGetter : MonoBehaviour
{
    public float MinutesBetweenUpdates;
    public string APIKey;
    public WeatherResponse Weather;
    public TextMeshProUGUI WeatherText;

    private float _latitude;
    private float _longitude;
    private float _currentTime;
    private bool _isLocationInitialize;

    public void Begin(LocationData location)
    {
        _latitude = location.lat;
        _longitude = location.lon;
        _isLocationInitialize = true;
    }

    private void Update()
    {
        if(_isLocationInitialize)
        {
            if(_currentTime <= 0)
            {
                Debug.Log("Getting weather data");
                StartCoroutine(GetWeatherCoroutine());
                _currentTime = MinutesBetweenUpdates * 60;
            }
            else
            {
                _currentTime -= Time.deltaTime;
            }
        }
    }

    private IEnumerator GetWeatherCoroutine()
    {
        UnityWebRequest request = new UnityWebRequest($"https://api.openweathermap.org/data/2.5/weather?lat={_latitude}&lon={_longitude}&appid={APIKey}")
        {
            downloadHandler = new DownloadHandlerBuffer()
        };

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            yield break;
        }

        Weather = JsonConvert.DeserializeObject<WeatherResponse>(request.downloadHandler.text);
        WeatherText.text = Weather.weather[0].main + ", " + ((int)Weather.main.temp - 273).ToString() + " °C"; //Kelvin to Celsius
    }
}
