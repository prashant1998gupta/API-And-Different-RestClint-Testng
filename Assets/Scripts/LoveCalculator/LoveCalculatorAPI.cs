using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoveCalculatorAPI : MonoBehaviour
{
    private const string m_baseUrl = "https://rapidapi.p.rapidapi.com";
    private const string endPoint = "/getPercentage?";


    private string fName = "prashant";
    private string sName = "Mylove";

    private void Start()
    {
        GetPercentage( "prashant" , "mylove");
    }
    private void GetPercentage(string fName, string sName)
    {
        string url = m_baseUrl + endPoint + "fname=" + fName +  "sname=" + sName; //"&" +
        Debug.Log(url);
        StartCoroutine(GetPercentageDetails(url));

    }

    IEnumerator GetPercentageDetails(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            //www.SetRequestHeader("x - rapidapi - host", "community - open - weather - map.p.rapidapi.com");
            www.SetRequestHeader("x-rapidapi-key", "89d62bcfc9msh09c9b0cd6551e3cp11a1dfjsnd72713b8ae5d");
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(": Error: " + www.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + www.downloadHandler.text);
                string json = www.downloadHandler.text;
                File.WriteAllText(Application.dataPath + "LoveCalc.json", json);


            }
        }
    }
}
