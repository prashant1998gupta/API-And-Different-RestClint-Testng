using Newtonsoft.Json;
// using Packages.Rider.Editor.Util;
using RestSharp;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class CountryDetailsApiManager : MonoBehaviour
{
    private const string m_baseUrl = "https://restcountries.eu/rest/v2";
    private const string m_endPointOfUrl = "/all";
   


    public static Action<AllCountry> OnGetCountryDetails;

    [SerializeField]
    private AllCountry allCountry; 

    void Start()
    {
        SetCountryUrl();
    }

    void SetCountryUrl()
    {
        string url = m_baseUrl + m_endPointOfUrl;
        // Debug.Log(url);
        //StartCoroutine(GetCountryDetails(url));
        //GetcounInfo(url);
        GetCountInfo(url);
    }

    IEnumerator GetCountryDetails(string url)
    {
        using(UnityWebRequest www = UnityWebRequest.Get(url))
        {


            /*www.SetRequestHeader("x-rapidapi-host", "restcountries-v1.p.rapidapi.com");
            www.SetRequestHeader("x-rapidapi-key", "89d62bcfc9msh09c9b0cd6551e3cp11a1dfjsnd72713b8ae5d");*/

            // Request and wait for the desired result.
            yield return www.SendWebRequest();


            if(www.isNetworkError)
            {
                Debug.Log(": Error:" + www.error);
            }
            else
            {
                //Debug.Log(":\nReceived: " + www.downloadHandler.text);
                string json = www.downloadHandler.text;
                string newJson = "{ \"country\": " + json + "}"; // this is because parseing can not done with json arrayObject only support with jsonObject
                Debug.Log(newJson);
                AllCountry countryDetails = JsonUtility.FromJson<AllCountry>(newJson);
                //AllCountry countryDetails = JsonConvert.DeserializeObject<AllCountry>(newJson);
                allCountry = countryDetails;
                OnGetCountryDetails?.Invoke(countryDetails);


            }

        }
    }

    void GetcounInfo(string url)
    {
        RestSharp.RestClient client = new RestSharp.RestClient(url);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Cookie", "__cfduid=de73afb10b16885a45ff1a8da23025d091602322209");
        IRestResponse response = client.Execute(request);
        string json = response.Content;
        string newJson = "{ \"country\": " + json + "}"; // this is because parseing can not done with json arrayObject only support with jsonObject
        Debug.Log(newJson);
        AllCountry countryDetails = JsonUtility.FromJson<AllCountry>(newJson);
        //AllCountry countryDetails = JsonConvert.DeserializeObject<AllCountry>(newJson);
        allCountry = countryDetails;
        OnGetCountryDetails?.Invoke(countryDetails);
    }

    

    void GetCountInfo(string url)
    {
        Proyecto26.RestClient.GetArray<CountryInfo>(url).Then(res =>
        {

            foreach (var item in res)
            {
                allCountry.country.Add(item);
            }

            OnGetCountryDetails?.Invoke(allCountry);
        });

        Debug.Log(url);
    }


}


#region subModel

[System.Serializable]
public class Currency
{
    public string code;
    public string name;
    public string symbol;
}

[System.Serializable]
public class Language
{
    public string iso639_1;
    public string iso639_2;
    public string name;
    public string nativeName;
}


[System.Serializable]
public class Translations
{
    public string de;
    public string es;
    public string fr ;
    public string ja ;
    public string it ;
    public string br ;
    public string pt ;
    public string nl ;
    public string hr ;
    public string fa;
}

[System.Serializable]
public class RegionalBloc
{
    public string acronym;
    public string name;
    public List<string> otherAcronyms;
    public List<string> otherNames;
}

[System.Serializable]
public class CountryInfo
{
    public string name;
    public List<string> topLevelDomain;
    public string alpha2Code;
    public string alpha3Code;
    public List<string> callingCodes;
    public string capital;
    public List<string> altSpellings;
    public string region;
    public string subregion;
    public int population;
    public List<double> latlng;
    public string demonym;
    public double? area;
    public double? gini;
    public List<string> timezones;
    public List<string> borders;
    public string nativeName;
    public string numericCode;
    public List<Currency> currencies;
    public List<Language> languages;
    public Translations translations;
    public string flag;
    public List<RegionalBloc> regionalBlocs;
    public string cioc;
}
#endregion

#region models
[System.Serializable]
public class AllCountry
{
     //public CountryInfo[] country;
    public List<CountryInfo> country;
}

#endregion
