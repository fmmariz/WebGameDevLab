using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private int _width = 25;
    [SerializeField] private int _depth = 25;
    [SerializeField] private float _scale = 25.0f;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    
    [SerializeField] private List<Material> _Materials;

    private GameObject[,] _map;

    private float _oldOffsetX;
    private float _oldOffsetY;
    private float _oldScale;

    // Start is called before the first frame update
    void Start()
    {
        _offsetX = Random.Range(-100, 100);
        _offsetX = Random.Range(-100, 100);
        _map = new GameObject[_width, _depth];
        GenerateMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeTexturesBasedOnRandom();
        }else if (Input.GetKeyDown(KeyCode.P)) {
            ChangeTextureBasedOnPerlin();
        }else if(Input.GetKeyDown(KeyCode.H))
        {
            ChangeHeightBasedOnPerlin();
        }

        if(_oldOffsetX != _offsetX)
        {
            _oldOffsetX = _offsetX;
            ChangeTextureBasedOnPerlin();
            ChangeHeightBasedOnPerlin();
        }
        if (_oldOffsetY != _offsetY)
        {
            _oldOffsetY = _offsetY;
            ChangeTextureBasedOnPerlin();
            ChangeHeightBasedOnPerlin();
        }

        if (_oldScale != _scale)
        {
            _oldScale = _scale;
            ChangeTextureBasedOnPerlin();
            ChangeHeightBasedOnPerlin();
        }

    }

    private void GenerateMap()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {
                _map[x,z] = Instantiate(_tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }

    private void ChangeTexturesBasedOnRandom()
    {
        for(int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {
                Material material = _Materials[Random.Range(0,_Materials.Count)];
                ChangeMaterial(_map[x,z], material);
            }
        }
    }

    private void ChangeTextureBasedOnPerlin()
    {
        float coordX, coordY;
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {
                coordX = (float)x / _width * _scale ;
                coordY = (float)z / _depth * _scale;
                float randomMaterial = Mathf.PerlinNoise(coordX +_offsetX, coordY+_offsetY);
                ChangePerlinMaterial(_map[x,z], randomMaterial);
            }
        }
    }

    private void ChangeHeightBasedOnPerlin()
    {
        float coordX, coordY;
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {
                coordX = (float)x / _width * _scale;
                coordY = (float)z / _depth * _scale;
                float perlinPosition = Mathf.PerlinNoise(coordX + _offsetX, coordY + _offsetY);
                Vector3 position = _map[x, z].transform.position;
                position.y = ChangePerlinHeight(perlinPosition);
                _map[x, z].transform.position = position;
            }
        }
    }

    private void ChangeMaterial(GameObject go, Material material)
    {
        go.GetComponent<Renderer>().material = material;
    }

    private void ChangePerlinMaterial(GameObject go, float random)
    {
        Material material;
        switch (random)
        {
            case <= 0.20f:
                material = _Materials[0];
                break;
            case <= 0.40f:
                material = _Materials[1];
                    break;
            case <= 0.60f:
                material = _Materials[2];
                break;
            case <= 0.80f:
                material = _Materials[3];
                break;
            default:
                material = _Materials[4];
                break;
        }
        go.GetComponent<Renderer>().material = material;
    }

    private float ChangePerlinHeight( float random)
    {

        switch (random)
        {
            case <= 0.20f:
                return 0;
            case <= 0.40f:
                return 0.10f;
            case <= 0.60f:
                return 0.3f;
            case <= 0.80f:
                return 0.8f;
            default:
                return 0.9f;
        }
    }
}
