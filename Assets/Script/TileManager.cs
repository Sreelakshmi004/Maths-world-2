using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tileprefabs;
    public float spawnz;
    public float tilelength = 30f;
    public int initialTileCount = 5;
    public Transform playertransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < initialTileCount; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, tileprefabs.Length));
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playertransform.position.z>spawnz-(tilelength * initialTileCount))
        {
            SpawnTile(Random.Range(0, tileprefabs.Length));
        }
    }
    public void SpawnTile(int tileindex)
    {
        Vector3 spawnPos = transform.position + transform.forward * spawnz;
        Instantiate(tileprefabs[tileindex], spawnPos, transform.rotation);
        spawnz += tilelength;

    }


}
