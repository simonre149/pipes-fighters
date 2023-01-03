using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> tiles = new();
    [SerializeField] float tilesScale = .1f;
    [SerializeField] int size = 100;
    [SerializeField] int seed = 0;
    [SerializeField] float scale = .1f;
    [SerializeField] float waterNoiseThreshold = .2f;

    GameObject map = null;

    float[,] heightMap;

    void Start()
    {
        loadMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            loadMap();
        }
    }

    private void loadMap(int seed = 0)
    {
        Destroy(map);
        map = new GameObject();
        map.name = "map";
        Instantiate(map);
        map.transform.parent = transform;

        if (seed == 0)
        {
            seed = Random.Range(-9999999, 9999999);
        }

        heightMap = new float[size, size];
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                heightMap[x, y] = Mathf.PerlinNoise(x * scale, y * scale);

                GameObject tile;
                if (heightMap[x, y] <= waterNoiseThreshold)
                {
                    tile = tiles[0];
                }
                else
                {
                    tile = tiles[1];

                }
                tile = Instantiate(tile);
                tile.transform.parent = map.transform;
                tile.transform.position = new Vector3(tilesScale * x, tilesScale * y, 0);
            }
        }
    }
}