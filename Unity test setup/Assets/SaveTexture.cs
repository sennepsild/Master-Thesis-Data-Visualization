using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveTexture : MonoBehaviour
{


    public RenderTexture rendTex;
    public Material mat;
    public Texture2D text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {

        

        if (Input.GetKeyDown(KeyCode.S))
        {

            Texture2D frame = new Texture2D(rendTex.width, rendTex.height);
            Graphics.ConvertTexture(rendTex, frame);

            

            

            

            
            frame.ReadPixels(new Rect(0, 0, rendTex.width, rendTex.height), 0, 0, false);
            frame.Apply();
            byte[] bytes = frame.EncodeToPNG();
            FileStream file = File.Open(@"Works.png", FileMode.Create);
            BinaryWriter binary = new BinaryWriter(file);
            binary.Write(bytes);
            file.Close();



        }
    }
}
