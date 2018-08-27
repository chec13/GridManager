using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class createImage : MonoBehaviour {
    public int width = 25, height = 25;
    [Range(1, 20)]
    public int Octaves = 1;
    public string name;
    Texture2D t;
    public float scale;
    public string seed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void createTexture()
    {
        t = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                t.SetPixel(x, y, color);
            }
        }
        t.Apply();

        
    }
    Color CalculateColor(int x, int y)
    {
        float sample = 0;
        for (int i = 1; i < Octaves+1; i++)
        {
            float xValue = (float)x / width * scale/i;
            float yValue = (float)y / width * scale/i;

            sample += Mathf.PerlinNoise(xValue + StringValue(seed), yValue + StringValue(seed));
        }
        

        
        return new Color(sample/Octaves, sample/Octaves, sample/Octaves);
    }
    public void saveImage()
    {
        byte[] bytes = t.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/created/" + name + ".png", bytes);
        
    }
    public Texture2D getTexture()
    {
        return t;
    }
    public int StringValue(string s)
    {
        int val = 0;
        for (int x = 0; x < s.Length; x++)
        {
            val = (val + (s[x] * (int)(Mathf.Pow(10,x)))) % int.MaxValue;
            
        }
        return val;
    }
}
