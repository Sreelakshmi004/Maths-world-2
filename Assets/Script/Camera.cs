using UnityEngine;

public class Cameras : MonoBehaviour
{
    public Transform Player;
    public float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 campos = transform.position;
        campos.z = Player.position.z + offset;
        transform.position = campos;
    }
}
