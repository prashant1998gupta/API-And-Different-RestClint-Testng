using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class CalenderInfo : MonoBehaviour
{
    private const string m_baseUrl = "https://calendarific.com/api/v2";
    private const string m_endpointHolidays = "/holidays?";
    private const string m_endpointCountrirs = "/countries?";

    private const string m_accessKay = "43ebc18f461eda69187a5d6df529d9e647dccb5e";

    [SerializeField]
    private GetInfoOfHolidays m_getinfo;
    //GetinfoOfHolidaysByCountry m_getinfoByCountry;

    private void Start()
    {
        GetCalenderinfo("IN", 2020);
    }

    private void GetCalenderinfo(string country , int year)
    {
       
        string url1 = m_baseUrl + m_endpointHolidays + "&api_key=" + m_accessKay + "&country=" + country + "&year=" + year;
        //string url2 = m_baseUrl + m_endpointCountrirs + "&api_key=" + m_accessKay + "&country=" + country + "&year=" + year;
        Debug.Log(url1);
        //Debug.Log(url2);
        StartCoroutine(GetCalenderDetails(url1));
        //StartCoroutine(GetCalenderDetails(url2));
    }


    IEnumerator GetCalenderDetails(string url)
    {
        using(UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError)
            {
                Debug.Log(": Error: " + www.error);
            }
            else
            {
                Debug.Log(":\nReceived:" + www.downloadHandler.text);
                string json = www.downloadHandler.text;
                GetInfoOfHolidays getInfoOfHolidays = JsonUtility.FromJson<GetInfoOfHolidays>(json);
                m_getinfo = getInfoOfHolidays;
            }


        }
    }
}

[System.Serializable]
public class Meta
{
    public int code;
}

[System.Serializable]
public class Country
{
    public string id ;
    public string name;
}

[System.Serializable]
public class Datetime
{
    public int year;
    public int month;
    public int day;
    public int? hour;
    public int? minute;
    public int? second;
}

[System.Serializable]
public class Timezone
{
    public string offset;
    public string zoneabb;
    public int zoneoffset;
    public int zonedst;
    public int zonetotaloffset;
}

[System.Serializable]
public class Date
{
    public object iso;
    public Datetime datetime;
    public Timezone timezone;
}

[System.Serializable]
public class Holiday
{
    public string name;
    public string description;
    public Country country;
    public Date date;
    public List<string> type;
    public string locations;
    public string states;
}

[System.Serializable]
public class Response
{
    public List<Holiday> holidays;
}

[System.Serializable]
public class GetInfoOfHolidays
{
    public Meta meta;
    public Response response;
}

//------------------------------------ getInfoOfHolidaysByCountry
public class Meta1
{
    public int code;
}

public class Country1
{
    public string country_name;
    public string iso ;
    public int total_holidays;
    public int supported_languages;
    public string uuid;
    }

    public class Response1
{
    public string url;
    public List<Country1> countries;
}

public class GetinfoOfHolidaysByCountry
{
    public Meta1 meta;
    public Response1 response;
}