using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

[System.Serializable]
public class Quest
{
    public int Id { get; set; }
    public string? Name { set; get; }
    public string? Description { set; get; }
    public int Reward { set; get; }

    public Quest() {}

    public Quest(int id, string name, string description, int reward)
    {
        Id = id;
        Name = name;
        Description = description;
        Reward = reward;
    }
    public IEnumerator LoadDataFromDatabase(string uri, Player player)
    {
        string json;
        // Send a WebApplication request by unity
        using UnityWebRequest request = UnityWebRequest.Get(uri);

        // After sending WebApplication request start communication with WebApplication via Unity code
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log($"WEB ERROR. Code: {request.error}");
        }
        else
        {
            json = request.downloadHandler.text;
            Quest[] quests = JsonConvert.DeserializeObject<Quest[]>(json);

            for (int i = 0; i < quests.Length; i++)
            {
                Debug.Log($"ID: {quests[i].Id}, Name: {quests[i].Name}");
            }
            Debug.Log($"ID: {quests[1].Id}, Name: {quests[1].Name}");
        }
    }
}