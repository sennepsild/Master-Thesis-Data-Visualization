using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShaderHandler : MonoBehaviour
{
    public Renderer rend;

    float[] floatArray = new float[] { 0f, 1f, 0.5f, 1f };
    // Start is called before the first frame update
    void Start()
    {
        var materialProperty = new MaterialPropertyBlock();
        float[] floatArray = new float[] { 6f, 1f };

        

        List<string> BlobSize = new List<string>();
        List<string> BlobNeg = new List<string>();
        List<string> BlobPos = new List<string>();
        List<string> BlobNeu = new List<string>();

        using (var reader = new StreamReader(@"Assets\AtroneerblobData.csv"))
        {
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                BlobSize.Add(values[0]);
                BlobNeg.Add(values[1]);
                BlobPos.Add(values[2]);
                BlobNeu.Add(values[3]);
            }
        }
        print(BlobSize.Count);
        print(BlobSize[1]);

        materialProperty.SetFloat("blobCount", (float) BlobSize.Count-1);


        float[] sizes = convertStringListToFloatArray(BlobSize,false);
        float[] negatives = convertStringListToFloatArray(BlobNeg, true);
        float[] positives = convertStringListToFloatArray(BlobPos, true);
        float[] neutrals = convertStringListToFloatArray(BlobNeu, true);

        float[] bokiValues = new float[sizes.Length];
        for (int i = 0; i < sizes.Length; i++)
        {
            if(negatives[i] > positives[i])
            {
                bokiValues[i] = neutrals[i]+ negatives[i];
            }
            else
            {
                bokiValues[i] = 1-  (neutrals[i]+ positives[i]);
            }

        }

        materialProperty.SetFloatArray("blobSize", sizes);
        materialProperty.SetFloatArray("blobNeg", negatives);
        materialProperty.SetFloatArray("blobPos", positives);
        materialProperty.SetFloatArray("blobNeu", neutrals);
        materialProperty.SetFloatArray("bokivalues", bokiValues);
        
        rend.SetPropertyBlock(materialProperty);

    }
    float[] convertStringListToFloatArray(List<string> blob, bool normalized)
    {
        float[] blobSizeConverted = new float[blob.Count - 1];

        for (int i = 1; i < blob.Count; i++)
        {
            string shortendString2 = blob[i];

            while (shortendString2.Length > 7)
            {
                shortendString2 = shortendString2.Remove(shortendString2.Length - 1);
                print(shortendString2);
            }

            float convertedString = float.Parse(shortendString2);
            if(convertedString>1)
            convertedString *= 0.00001f;
            if (convertedString > 1)
                convertedString = 0.00001f;

            if (normalized)
            {
                convertedString = Mathf.Lerp(0.1f, 1f, convertedString );
            }
            else
            {
                convertedString = Mathf.Lerp(0.1f, 2f, convertedString * 100);
            }
            

            blobSizeConverted[i - 1] = convertedString;
        }
        return blobSizeConverted;
    }
    
}
