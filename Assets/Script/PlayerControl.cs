using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardspeed;
    private int desiredlane = 1; // 0 = left, 1 = middle, 2 = right
    public float lanedistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardspeed;
        if (Input.GetKey(KeyCode.RightArrow)    )
        {
            desiredlane++;
            if (desiredlane >2)
            {
                desiredlane = 2;
            }
            
        }
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            desiredlane--;
            if (desiredlane <0)
            {
                desiredlane = 0;
            }
           

        }
        Vector3 targetposition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredlane == 0)
        {
            targetposition += Vector3.left * lanedistance;
        }
        else if (desiredlane == 2)
        {
            targetposition += Vector3.right * lanedistance;
        }
        transform.position = targetposition;
    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
