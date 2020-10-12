using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetCountryNameAndCode : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown countryListDropdown;
    public static List<string> countryList = new List<string>();

    void Start()
    {
        CountryDetailsApiManager.OnGetCountryDetails += PopulateList;
    }

    private void PopulateList(AllCountry allCountry)
    {
        foreach (var item in allCountry.country)
        {
            string nameAndCode =  item.alpha2Code + " " + "(" + item.name + ")";
            countryList.Add(nameAndCode);
            //Debug.Log(nameAndCode);
        }
        foreach (var item in countryList)
        {
            Debug.Log(item);
        }
        Debug.Log("this is PopulateList mathod");
        countryListDropdown.AddOptions(countryList);
    }

   
}
