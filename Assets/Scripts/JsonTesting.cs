using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class JsonTesting : MonoBehaviour
{
    private void Start()
    {
            /*MyJson myJson = new MyJson();
            myJson.pos = new Vector3();
            myJson.charName = "Prashant";
            myJson.health = 10;

            string json = JsonUtility.ToJson(myJson);
            Debug.Log(json);

            File.WriteAllText(Application.dataPath + "fileJson.json", json);*/

        string json = File.ReadAllText(Application.dataPath + "fileJson.json");

        MyJson JsonObj = JsonUtility.FromJson<MyJson>(json);
        Debug.Log(JsonObj.health + " " + JsonObj.pos + " " + JsonObj.charName);
    }




    private class MyJson
    {
        public Vector3 pos;
        public int health;
        public string charName;
    }

}
