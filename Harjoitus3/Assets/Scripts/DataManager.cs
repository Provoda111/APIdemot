using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Player player;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            GetData();
        }
    }
    void GetData()
    {
        print("Haku");
        string uri = "https://localhost:7256/quests";

        Quest quest = new Quest();
        StartCoroutine(quest.LoadDataFromDatabase(uri, player));
    }
}
