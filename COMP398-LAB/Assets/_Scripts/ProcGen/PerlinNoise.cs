using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] private int _width = 256;
    [SerializeField] private int _height = 256;
    [SerializeField] private float _scale = 20.0f;


    private void FixedUpdate()
    {
        GetComponent<Renderer>().material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture()
    {
        var texture = new Texture2D(_width, _height);
        for(int i = 0; i < _width; i++)
        {
            for(int o = 0; o < _height; o++)
            {
                Color color = CalculateColor(i, o);
                texture.SetPixel(i, o, color);
            }
        }
        texture.Apply();
        return texture;
    }

    private Color CalculateColor(int i, int o)
    {
        float coordX = (float) i / _width * _scale;
        float coordY = (float) o / _height * _scale;
        float sample = Mathf.PerlinNoise(coordX, coordY);
        return new Color(sample,sample,sample);
    }
}
