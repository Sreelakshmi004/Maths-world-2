using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardspeed;
    private int desiredlane = 1; // 0 = left, 1 = middle, 2 = right
    public float lanedistance = 2.5f;
    public float jumpforce;
    public float gravity = -9.18f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        direction.z = forwardspeed;
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredlane--;
            if (desiredlane < 0) desiredlane = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredlane++;
            if (desiredlane > 2) desiredlane = 2;
        }
        if(controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
       

        Vector3 targetPosition = transform.position.z * Vector3.forward + transform.position.y * Vector3.up;
        float laneOffset = (desiredlane - 1) * lanedistance;
        targetPosition += Vector3.right * laneOffset;

        transform.position = targetPosition;
    }
    private void jump()
    {
        direction.y = jumpforce;
    }
    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
