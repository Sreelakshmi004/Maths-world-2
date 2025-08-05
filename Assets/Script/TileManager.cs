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

        SpawnNumberInTile(tile);
    }

    private void SpawnNumberInTile(GameObject tile)
    {
        if (multipleIndex >= multiplesOfThree.Count) return;

        Transform spawnPoint = null;

        // Find first available spawn point
        foreach (Transform child in tile.transform)
        {
            if (child.name.ToLower().Contains("spawnpoint"))
            {
                spawnPoint = child;
                break;
            }
        }

        if (spawnPoint != null)
        {
            int numberValue = multiplesOfThree[multipleIndex];

            GameObject number = Instantiate(numberPrefab, spawnPoint.position, Quaternion.identity, tile.transform);
            number.name = "Number_" + numberValue;

            // Set number text (TextMesh or TMP)
            var text = number.GetComponentInChildren<TextMesh>();
            if (text != null)
                text.text = numberValue.ToString();
            else
            {
                var tmp = number.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = numberValue.ToString();
            }

            // Assign value to Number script
            Number nv = number.GetComponent<Number>();
            if (nv == null)
                nv = number.AddComponent<Number>();

            nv.number = numberValue;
        }
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    // Call this from Number.cs when a correct number is collected
    public void OnMultipleCollected()
    {
        multipleIndex++;
    }
}
