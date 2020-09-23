using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image : MonoBehaviour
{
    public static int numberOfclusters = 0;

    ImageHolder holder;

    Renderer rend;


    public string GameName;
    

    public int cluster = -1;

    public LayerMask mask;

    public void UpdateCluster()
    {
        numberOfclusters++;
        cluster = numberOfclusters;


        Collider[] nearbyImages = Physics.OverlapSphere(transform.position, 1f, mask);
        for (int i = 0; i < nearbyImages.Length; i++)
        {
           Image img =  nearbyImages[i].GetComponent<Image>();
           

                
                
            
            if(img.cluster != cluster)
            {
                
                img.cluster = cluster;


                img.UpdateClusternoInc();
            }


        }
        


    }

    public void UpdateClusternoInc()
    {
        
        


        Collider[] nearbyImages = Physics.OverlapSphere(transform.position, 1f, mask);
        for (int i = 0; i < nearbyImages.Length; i++)
        {
            Image img = nearbyImages[i].GetComponent<Image>();





            if (img.cluster != cluster)
            {

                img.cluster = cluster;


                img.UpdateClusternoInc();
            }


        }
        


    }




    // Start is called before the first frame update
    void Start()
    {
       holder = FindObjectOfType<ImageHolder>();

        rend = GetComponent<Renderer>();
        rend.material.mainTexture = holder.getTex();
        GameName =rend.material.mainTexture.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
