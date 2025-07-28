
using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject numberPrefab;

    public float spawnZ = 0;
    public float tileLength = 30f;
    public int initialTileCount = 5;
    public Transform playerTransform;

    private List<GameObject> activeTiles = new List<GameObject>();
    private List<int> multiplesOfThree = new List<int>() { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30 };
    private int multipleIndex = 0;

    void Start()
    {
        for (int i = 0; i < initialTileCount; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 35 > spawnZ - (tileLength * initialTileCount))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        Vector3 spawnPos = transform.position + transform.forward * spawnZ;
        GameObject tile = Instantiate(tilePrefabs[tileIndex], spawnPos, transform.rotation);
        activeTiles.Add(tile);
        spawnZ += tileLength;

        SpawnNumbersAtPoints(tile);
    }

    private void SpawnNumbersAtPoints(GameObject tile)
    {
        foreach (Transform child in tile.transform)
        {
            if (child.name.ToLower().Contains("spawnpoint"))
            {
                if (multipleIndex >= multiplesOfThree.Count)
                    return;

                GameObject number = Instantiate(numberPrefab, child.position, Quaternion.identity, tile.transform);

                // Assign number text (TextMesh or TextMeshPro)
                int numberValue = multiplesOfThree[multipleIndex++];
                var text = number.GetComponentInChildren<TextMesh>();
                if (text != null)
                {
                    text.text = numberValue.ToString();
                }
                else
                {
                    var tmp = number.GetComponentInChildren<TMPro.TextMeshPro>();
                    if (tmp != null)
                        tmp.text = numberValue.ToString();
                }

                Number nv = number.GetComponent<Number>();
                if (nv == null)
                {
                    nv = number.AddComponent<Number>(); // Add component if not already present
                }
                nv.number = numberValue;

                number.name = "Number_" + numberValue;
            }
        }
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
