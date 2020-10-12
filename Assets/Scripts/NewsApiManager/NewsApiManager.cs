using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class NewsApiManager : MonoBehaviour
{
    //https://newsapi.org/docs/get-started
    private const string m_baseUrl = "http://newsapi.org/v2";
    private const string m_endpoint = "/top-headlines?";
    private const string access_key = "f3126fecbec44006b2225feae788a036";

    [SerializeField]
    private GetNewsinfo m_getNewinfo;
    public static Action<GetNewsinfo> OnNewsFeedDataGet;
    /*[SerializeField]
    NewsFeedView newsFeedView;*/

    /* void Awake()
     {
         newsFeedView = GameObject.FindGameObjectWithTag("NewsFeedView").GetComponent<NewsFeedView>();
     }*/

    private void Start()
    {
        GetNewsDetails("in");
    }
    private void GetNewsDetails(string country)
    {
        string url = m_baseUrl + m_endpoint + "country=" + country + "&" + "apiKey=" + access_key;
        Debug.Log(url);
        StartCoroutine(GetNews(url));
    }

    IEnumerator GetNews(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(": Error: " + www.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + www.downloadHandler.text);
                string json = www.downloadHandler.text;
                GetNewsinfo newInfo = JsonUtility.FromJson<GetNewsinfo>(json);
                m_getNewinfo = newInfo;

                /*if(OnNewsFeedDataGet != null)
                {
                    OnNewsFeedDataGet(newInfo);
                }*/

                OnNewsFeedDataGet?.Invoke(newInfo);
            }
        }
    }
}

[System.Serializable]
public class Source
{
    public string id;
    public string name;
}

[System.Serializable]
public class Article
{
    public Source source;
    public string author;
    public string title;
    public string description;
    public string url;
    public string publishedAt;
    public string content;
}

[System.Serializable]
public class GetNewsinfo
{
    public string status;
    public int totalResults;
    public List<Article> articles;
}

