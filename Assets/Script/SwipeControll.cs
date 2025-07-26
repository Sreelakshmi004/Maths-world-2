using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class SwipeControll : MonoBehaviour
{
    public static bool Tap, Swipeleft, Swiperight, Swipeup, Swipedown;
    private bool isdragging = false;
    private Vector2 Starttouch, Swipedelta;


    // Update is called once per frame
    void Update()
    {
        Tap = Swipeleft = Swiperight = Swipeup = Swipedown = false;
        if (Input.GetMouseButtonDown(0))
        {
            Tap = true;
            isdragging = true;
            Starttouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isdragging = false;
            Reset();

        }
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Tap = true;
                isdragging = true;
                Starttouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isdragging = false;
                Reset();
            }
        }
            Swipedelta = Vector2.zero;
            if (isdragging)
            {
                if (Input.touches.Length > 0)
                {

                    Swipedelta = Input.touches[0].position - Starttouch;
                }
                else if (Input.GetMouseButton(0))
                {
                    Swipedelta = (Vector2)Input.mousePosition - Starttouch;
                }
            }
            if (Swipedelta.magnitude > 100)
            {
                float x = Swipedelta.x;
                float y = Swipedelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        Swipeleft = true;
                    }
                    else
                    {
                        Swiperight = true;
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        Swipedown = true;
                    }
                    else
                    {
                        Swipeup = true;
                    }
                    
                }
                Reset();

            }


        }
    public void Reset()
    {
        Starttouch = Swipedelta = Vector2.zero;
        isdragging = false;
    }
}
