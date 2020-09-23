using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveimage : MonoBehaviour
{
    public ClusterMaker maker;

    Transform imageToMove;

    Animator anim;

    bool select = false;

    Vector3 pos;

    Vector3 moveVector;
    Vector3 startPos;

    public LayerMask groundMask,imageMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 10000000, imageMask);

            
            if(hit.collider != null)
            {
                anim = hit.collider.GetComponent<Animator>();
                imageToMove = hit.collider.transform.parent;

                
                pos = imageToMove.transform.position;
            }

            if (imageToMove != null)
            {
                MouseDown();

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            if(imageToMove != null){
                MouseUp();

            }
            imageToMove = null;
        }
        if (imageToMove != null)
        {
            Mouse();
        }
        //print(imageToMove);
    }

    void MouseUp()
    {
        select = false;
        anim.SetBool("Up", select);

        pos = imageToMove.transform.position;

        imageToMove.GetChild(0).GetComponent<Image>().UpdateCluster();
        maker.CreateClusters();
    }
    void Mouse()
    {
        if (select)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 10000000, groundMask);

            moveVector = hit.point;
            //moveVector =Camera.main.ScreenToWorldPoint(moveVector);

            moveVector = new Vector3(moveVector.x, 0, moveVector.z);


            imageToMove.position = pos + (moveVector - startPos);



        }
    }
    void MouseDown()
    {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 1000000, groundMask);

        
        startPos = hit.point;

        startPos = new Vector3(startPos.x, 0, startPos.z);
        select = true;
        anim.SetBool("Up", select);

        
    }
}
