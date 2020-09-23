using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ClusterText : MonoBehaviour
{

    public TMPro.TMP_Text over, under;
    public string overs, unders;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (over.text != overs)
            over.text = overs;

        if (under.text != unders)
            under.text = unders;
        
    }
}
