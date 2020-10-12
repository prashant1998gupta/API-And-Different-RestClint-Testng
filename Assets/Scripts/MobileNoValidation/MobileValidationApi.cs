using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MobileValidationApi : MonoBehaviour
{
    private const string m_baseUrlWithEndPoint = "http://apilayer.net/api/validate?";
    private const string access_key = "c950470bd3662faefccd4e81d07ccdf2";
    //http://apilayer.net/api/validate?access_key=c950470bd3662faefccd4e81d07ccdf2&number=8052504940&country_code=IN&format=0

    [SerializeField] 
    private InputField m_inputNumber;

    [SerializeField] 
    private TMP_Dropdown countryListDropDownValue;

    [SerializeField]
    private Button m_btn_SendRequest;

    [SerializeField]
    private ValidationInfo MobileNumberValidaton;

    private string number;
    private string country_code;
    private string format = "0"; // 0 or 1 
   

    private void Start()
    {
        m_inputNumber.onEndEdit.AddListener((text) => { number = text; });
        countryListDropDownValue.onValueChanged.AddListener((index)=>
        { 
            country_code = GetCountryNameAndCode.countryList[index-1];
            string[] cCode = country_code.Split(' ');
            country_code = cCode[0];
            Debug.Log(country_code);
        }
        );
        m_btn_SendRequest.onClick.AddListener(() => GetValidation(number, country_code, format));


    }
    private void GetValidation(string number, string country_code , string format)
    {
        string url = m_baseUrlWithEndPoint + "access_key=" + access_key + "&" + "number=" + number + "&" + "country_code=" +  country_code + "&" + "format=" + format ;
        Debug.Log(url);
        StartCoroutine(GetvalidationDetails(url));

    }

    IEnumerator GetvalidationDetails(string url)
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
                ValidationInfo validationInfo = JsonUtility.FromJson<ValidationInfo>(json);
                MobileNumberValidaton = validationInfo;
            }
        }
    }
}

[System.Serializable]
public class ValidationInfo
{
    public bool valid;
    public string number;
    public string local_format;
    public string international_format;
    public string country_prefix;
    public string country_code;
    public string country_name;
    public string location;
    public string carrier;
    public string line_type;
}
