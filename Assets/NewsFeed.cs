using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsFeed : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Text;

    string headLineText;
    string descritonText;
    public string HeadLineText
    {
        set
        {
            headLineText = $"<b>{"Tital -:"}</b> <b>{value}</b>";
        }
    }

    public string DescriptionText
    {
        set
        {
            descritonText = value;
            Text.text =headLineText + "\n \n" + descritonText;
        }
    }


}
