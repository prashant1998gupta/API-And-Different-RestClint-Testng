using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace APITest {
    public class WeatherFetchApi : MonoBehaviour
    {
        private const string logkey = "WeatherFetchApi";

        private const string m_baseUrl = "https://community-open-weather-map.p.rapidapi.com";
        private const string m_weatherEndPoint = "/weather?";
        private const string m_weatherForcastEndPoint = "/forecast/daily?";

        [SerializeField] private WeatherDetailsResponse weatherDetailsResponse;
        [SerializeField] private WeatherForcastDetiailsResponse weatherForcastDetiailsResponse;

        [SerializeField] private Button m_btn_SendRequest1;
        [SerializeField] private Button m_btn_SendRequest2;
        [SerializeField] private InputField m_inputCityName;
        [SerializeField] private string m_cityName;

        void Start()
        {
            m_btn_SendRequest1.onClick.AddListener(()=> { GetWeatherDetails(m_cityName);});
            m_btn_SendRequest2.onClick.AddListener(()=> { GetWeatherForcastDetiails(m_cityName);});
            m_inputCityName.onEndEdit.AddListener((text)=> { m_cityName = text;});
        }

        void GetWeatherDetails(string city)
        {
            string url = m_baseUrl + m_weatherEndPoint + "q=" + city;
            StartCoroutine(GetRequestGetWeatherDetails(url));
            Debug.Log($"{logkey} =>GetWeatherDetails request is sent.");
        }
        void GetWeatherForcastDetiails(string city)
        {
            string url = m_baseUrl + m_weatherForcastEndPoint + "q=" + city;
            StartCoroutine(GetRequestGetWeatherForcastDetiails(url));
            Debug.Log($"{logkey} =>GetWeatherDetails request is sent.");
        }

        IEnumerator GetRequestGetWeatherDetails(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {

                //webRequest.SetRequestHeader("x-rapidapi-host", "community-open-weather-map.p.rapidapi.com");
                webRequest.SetRequestHeader("x-rapidapi-key", "89d62bcfc9msh09c9b0cd6551e3cp11a1dfjsnd72713b8ae5d");


                // Request and wait for the desired result.
                yield return webRequest.SendWebRequest();


                if (webRequest.isNetworkError)
                {
                    Debug.Log(": Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                    string json = webRequest.downloadHandler.text;
                    WeatherDetailsResponse JsonObj = JsonUtility.FromJson<WeatherDetailsResponse>(json);
                    weatherDetailsResponse = JsonObj;
                }
            }
        }
        IEnumerator GetRequestGetWeatherForcastDetiails(string url)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {

                //webRequest.SetRequestHeader("x - rapidapi - host", "community - open - weather - map.p.rapidapi.com");
                webRequest.SetRequestHeader("x-rapidapi-key", "89d62bcfc9msh09c9b0cd6551e3cp11a1dfjsnd72713b8ae5d");


                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();


                if (webRequest.isNetworkError)
                {
                    Debug.Log(": Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                    string json = webRequest.downloadHandler.text;
                    WeatherForcastDetiailsResponse JsonObj = JsonUtility.FromJson<WeatherForcastDetiailsResponse>(json);
                    weatherForcastDetiailsResponse = JsonObj;
                }
            }
        }
    }

    #region Model class
    [System.Serializable]
    public class WeatherDetailsResponse : Response
    {
        public Coord coord;
        public List<Weather> weather;
        public string @base;
        public Main main;
        public int visibility;
        public Wind wind;
        public Clouds clouds;
        public int dt;
        public Sys sys;
        public int timezone;
        public int id;
        public string name;
        public int cod;
    }

    [System.Serializable]
    public class WeatherForcastDetiailsResponse : Response
    {
        public City city;
        public string cod;
        public double message;
        public int cnt;
        public List<ListInfo> list;
    }

    #region Sub Model class

    [System.Serializable]
    public class Coord
    {
        public double lon;
        public double lat;
    }

    [System.Serializable]
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }

    [System.Serializable]
    public class Main
    {
        public double temp;
        public double feels_like;
        public double temp_min;
        public double temp_max;
        public int pressure;
        public int humidity;
    }

    [System.Serializable]
    public class Wind
    {
        public double speed;
        public int deg;
    }

    [System.Serializable]
    public class Clouds
    {
        public int all;
    }

    [System.Serializable]
    public class Sys
    {
        public int type;
        public int id;
        public string country;
        public int sunrise;
        public int sunset;
    }

    [System.Serializable]
    public class City
    {
        public int id;
        public string name;
        public Coord coord;
        public string country;
        public int population;
        public int timezone;

    }

    [System.Serializable]
    public class ListInfo
    {
        public int dt;
        public int sunrise;
        public int sunset;
        public Temp temp;
        public FeelsLike feels_like;
        public int pressure;
        public int humidity;
        public List<Weather> weather;
        public double speed;
        public int deg;
        public int clouds;
        public int pop;
    }

    [System.Serializable]
    public class Temp
    {
        public double day;
        public double min;
        public double max;
        public double night;
        public double eve;
        public double morn;
    }

    [System.Serializable]
    public class FeelsLike
    {
        public double day;
        public double night;
        public double eve;
        public double morn;
    }

    interface Response
    {

    }

    #endregion
    #endregion
}