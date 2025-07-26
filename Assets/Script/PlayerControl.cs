using System.Runtime.CompilerServices;
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
    public Animator animator;

    void Start()
    {
        Time.timeScale = 1f;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        direction.z = forwardspeed;
        
        if (SwipeControll.Swipeleft)
        {
            desiredlane--;
            if (desiredlane < 0) desiredlane = 0;
        }

        if (SwipeControll.Swiperight)
        {
            desiredlane++;
            if (desiredlane > 2) desiredlane = 2;
        }
        if(controller.isGrounded)
        {
            if (SwipeControll.Swipeup)
            {
                jump();
                animator.SetTrigger("Rotate");
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
        controller.center = controller.center;
    }
    private void jump()
    {
        direction.y = jumpforce;
    }
    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacles"))
        {
            Game.gameover = true;
        }
    }
}
