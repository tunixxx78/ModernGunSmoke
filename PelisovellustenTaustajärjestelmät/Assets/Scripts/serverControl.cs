using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using SimpleJSON;

public class serverControl : MonoBehaviour
{
    public string address, getPlayerAddress;
    public GameObject plr;

    private void Start()
    {
        address = "http://localhost:3000/unitytest";
        getPlayerAddress = "http://localhost:3000/api/player/1";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetWebData(address));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(GetPlayerData(getPlayerAddress));
        }
    }

    IEnumerator GetWebData(string address)
    {
        UnityWebRequest www = UnityWebRequest.Get(address);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("ERROR" + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            //tulee response

            ProcessServerResponse(www.downloadHandler.text);
        }
    }

    IEnumerator GetPlayerData(string address)
    {
        UnityWebRequest www = UnityWebRequest.Get(address);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("ERROR: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            //tulee response

            //ProcessServerResponse(www.downloadHandler.text);
        }
    }

    void ProcessServerResponse(string rawResponse)
    {
        Debug.Log("Response on saatu" + rawResponse);

        JSONNode node = JSON.Parse(rawResponse);

        if(node["action"] == "instancePlayer")
        {
            Debug.Log("Instansioidaan pelaaja, syystä että....");
            InstantiatePlayer(node);
        }
    }

    public void InstantiatePlayer(JSONNode nodeInfo)
    {
        Vector3 plrPos = new Vector3(nodeInfo["position"][0]["value"], nodeInfo["position"][1]["value"], nodeInfo["position"][2]["value"]);

        Instantiate(plr, plrPos, Quaternion.identity);
    }
}
