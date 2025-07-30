using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerControl : MonoBehaviour
{
    public GameObject equationui;
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed = 10f;
    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right
    public float laneDistance = 2f;
    public float jumpForce;
    public float gravity = -9.81f;
    public Animator animator;
   public AudioSource a1;
    public AudioSource a2;
      

    public TextMeshProUGUI equationText; // Reference to UI Text to show equation

    private void Start()
    {
        Time.timeScale = 1f; 
        equationui.SetActive(false); // Hide the equation UI at start
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }

        direction.z = forwardSpeed;

        // Lane switching
        if (SwipeControll.Swiperight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (SwipeControll.Swipeleft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).x * 10;
        moveVector.y = direction.y;
        moveVector.z = direction.z;

        controller.Move(moveVector * Time.deltaTime);

        // Jump
        if (controller.isGrounded)
        {
            direction.y = -1;
            if (SwipeControll.Swipeup)
            {
                direction.y = jumpForce;
                animator.SetTrigger("Rotate");
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacles"))
        {
            FindFirstObjectByType<Game>().Overgame();
            a2.Play();
          
        }

        if (hit.gameObject.CompareTag("Collectable"))
        {
            Number numScript = hit.gameObject.GetComponent<Number>();
            if (numScript != null)
            {
                a1.Play(); // Play collect sound
                int val = numScript.GetValue();
                int factor = val / 3;

                ShowEquation(factor, 3, val);
                Game.Instance.OnNumberCollected(val);

                Destroy(hit.gameObject);
            }
        }
    }

     void ShowEquation(int a, int b, int result)
    {
        if (equationui && equationText )
        {
            equationui.SetActive(true); // Show the equation UI
            equationText.text = a + " * " + b + " = " + result;
            
            Invoke("ClearEquation", 1f); 
        }
    }
    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void ClearEquation()
    {
        if (equationui)
        {
            equationui.SetActive(false);
        }
    }
}
