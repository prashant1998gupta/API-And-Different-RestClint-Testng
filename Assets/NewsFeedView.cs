using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsFeedView : MonoBehaviour
{
    public NewsFeed newsFeed;
    public RectTransform newFeedContainer;

   
    void Awake()
    {
        NewsApiManager.OnNewsFeedDataGet += GetNewFeed;
    }
    
    public void GetNewFeed(GetNewsinfo data)
    {
        foreach (var item in data.articles)
        {
            //Debug.Log(item.title);
            //Debug.Log(item.description);
            //create feed for each artical
            CreateNewsFeed(item.title, item.description);
        }
    }

    private void CreateNewsFeed(string title, string description)
    {
        //insteacat a new game object as news feed
        NewsFeed newsFeedTemp =  Instantiate(newsFeed, newFeedContainer);
        newsFeedTemp.HeadLineText = title;
        newsFeedTemp.DescriptionText = description;
    }

    void OnDisable()
    {
        NewsApiManager.OnNewsFeedDataGet -= GetNewFeed;
        Debug.Log("OnDisable");
    }
}
