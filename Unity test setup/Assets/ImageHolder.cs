using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHolder : MonoBehaviour
{
    public Texture[] images;
    int number  = 0;

    public Texture getTex()
    {
        number++;
        return images[number-1];
    }

}
