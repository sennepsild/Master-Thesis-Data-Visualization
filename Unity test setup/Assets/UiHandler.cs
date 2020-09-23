using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    public int currentGroup = 0;

    public static GameObject currentCluster;

    public GameObject Mover;

    public TMPro.TMP_Text overText;

    public ClusterMaker clusters;

    public GameObject datasend;
    public GameObject otherCanvas;


    bool isLooking = false;

    public Text buttonText;

    public void Done()
    {
        if (clusters.createdClusterTexts.Count <= 0)
            buttonText.text = "Make at least 1 group first";
        else if(!isLooking) {

            if (currentGroup < clusters.createdClusterTexts.Count)
            {
                
                overText.gameObject.SetActive(true);
                Mover.SetActive(false);

                buttonText.text = "Click when done looking";


                Vector3 newcampos;
                newcampos = clusters.createdClusterTexts[currentGroup].transform.position;
                newcampos.y = Camera.main.transform.position.y;

                ClusterText ct = clusters.createdClusterTexts[currentGroup].GetComponent<ClusterText>();

                currentCluster = ct.gameObject;

                overText.text = "Look at the " + ct.unders + " in " + ct.overs;




                ct.gameObject.SetActive(false);

                Camera.main.transform.position = newcampos;

                Camera.main.orthographicSize = 2;

                hideOtherImages();
                isLooking = true;

            }
            else
            {
                overText.text = "Thank you for participating";
                buttonText.transform.parent.gameObject.SetActive(false);
            }

        }
        else
        {
            Camera.main.transform.position = new Vector3(11111, Camera.main.transform.position.y, 1111);
            isLooking = false;
            otherCanvas.SetActive(false);
            

            datasend.SetActive(true);
        }


    }

    public void unHideImages()
    {
        for (int i = 0; i < clusters.allImages.Length; i++)
        {
            
                clusters.allImages[i].transform.parent.gameObject.SetActive(true);
        }

        
    }

    void hideOtherImages()
    {
        float shortestLength = float.MaxValue;
        int index = 0;


        for (int i = 0; i < clusters.allImages.Length; i++)
        {
            float length = Vector3.Distance(clusters.allImages[i].transform.position, currentCluster.transform.position);
            if (length < shortestLength)
            {
                shortestLength = length;
                index = i;
            }

        }
        

        int clusterNr = clusters.allImages[index].cluster;

        for (int i = 0; i < clusters.allImages.Length; i++)
        {
            if (clusters.allImages[i].cluster != clusterNr)
                clusters.allImages[i].transform.parent.gameObject.SetActive(false);
        }
        for (int i = 0; i < clusters.createdClusterTexts.Count; i++)
        {
            clusters.createdClusterTexts[i].SetActive(false);
        }

    }
}
