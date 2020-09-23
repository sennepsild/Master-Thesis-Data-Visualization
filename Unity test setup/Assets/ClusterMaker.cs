using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterMaker : MonoBehaviour
{
    public GameObject clusterPrefab;

    List<List<Vector3>> positions;

    public Image[] allImages;

    public List<GameObject> createdClusterTexts = new List<GameObject>();

    public void CreateClusters()
    {
        positions = new List<List<Vector3>>();

        for (int i = 0; i < Image.numberOfclusters+1; i++)
        {
            positions.Add(new List<Vector3>());
        }

        for (int i = 0; i < allImages.Length; i++)
        {
            
            if(allImages[i].cluster != -1)
            {
                
                positions[allImages[i].cluster].Add(allImages[i].transform.position);
            }
        }
        int clusterTextCounter = 0;
        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 sum = new Vector3();
        
            for (int k = 0; k < positions[i].Count; k++)
            {
                sum += positions[i][k];
            }
            if(positions[i].Count > 0)
            {
                if (createdClusterTexts.Count - 1 < clusterTextCounter) 
                    createdClusterTexts.Add(Instantiate(clusterPrefab));
                sum.y = 0;
                sum.x = sum.x / positions[i].Count;
                sum.z = sum.z / positions[i].Count;
                createdClusterTexts[clusterTextCounter].transform.position = sum ;

                ClusterText ct = createdClusterTexts[clusterTextCounter].GetComponent<ClusterText>();

                ct.overs = "Group " + (clusterTextCounter + 1);
                ct.unders = positions[i].Count+ " Image";
                if (positions[i].Count > 1)
                    ct.unders = ct.unders + "s";


                clusterTextCounter++;
            }

        }

        while(clusterTextCounter <= createdClusterTexts.Count-1)
        {

            GameObject obj = createdClusterTexts[createdClusterTexts.Count - 1];
            createdClusterTexts.Remove(obj);
            Destroy(obj);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
