using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendData : MonoBehaviour
{
    public ClusterMaker maker;

    public GameObject sendDataObj;

    public UiHandler handler;

    const string formURL = "https://docs.google.com/forms/d/e/1FAIpQLSd7WzdNm3BuLNywbDSlbGXcjn4BM7omM-CjjrnQQ0S0kDLfHA/";


    const string clusterEntry = "entry.2114644727";
    const string thoughtEntry = "entry.277373159";

    public Text thoughtData;
    public InputField field;

    public void Send()
    {
        handler.unHideImages();
        GameObject clustertext = UiHandler.currentCluster;
        float shortestLength = float.MaxValue;
        int index = 0;


        for (int i = 0; i < maker.allImages.Length; i++)
        {
            float length = Vector3.Distance(maker.allImages[i].transform.position, clustertext.transform.position);
            if (length < shortestLength)
            {
                shortestLength = length;
                index = i;
            }

        }
        string clusterData = "";

        int clusterNr = maker.allImages[index].cluster;

        for (int i = 0; i < maker.allImages.Length; i++)
        {
         if(maker.allImages[i].cluster == clusterNr)
            {
                if(clusterData == "")
                {
                    clusterData = "" + maker.allImages[i].GameName;
                }
                else
                {
                    clusterData = clusterData + "," + maker.allImages[i].GameName;
                }
                
            }   
        }

        

        StartCoroutine(SendFormData(clusterData, thoughtData.text));

        
        field.text = "";
        thoughtData.text = "";

        handler.otherCanvas.SetActive(true);
        handler.currentGroup++;
        handler.Done();
        sendDataObj.SetActive(false);
    }

    

    private static IEnumerator SendFormData(string clusterData, string thoughtData)
    {
        
        
        WWWForm form = new WWWForm();
        form.AddField(clusterEntry, clusterData);
        form.AddField(thoughtEntry, thoughtData);
        string urlGFormResponse = formURL + "formResponse";
        using (UnityWebRequest www = UnityWebRequest.Post(urlGFormResponse, form))
        {
            yield return www.SendWebRequest();
        }
    }

}
