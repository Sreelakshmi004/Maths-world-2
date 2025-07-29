using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;
public class Game : MonoBehaviour
{
    public static Game Instance;
    public GameObject gameoverui;
    public GameObject nextLevelUI;
    public GameObject equationui;
    public TextMeshProUGUI timetext;
    private int[] targetMultiples = { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30 };
    private int currentIndex = 0;
    private bool isgamerunning = false;
    private float timer = 60f;
    private List<Number> activeNumberObjects = new List<Number>();

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        
    }

    void Start()
    {
        isgamerunning = true;
       
    }

    void Update()
    {
        if (isgamerunning)
        {
            timetext.enabled = true;
            timer -= Time.deltaTime;
            Time.timeScale = 1f;
            
            UpdateTime();
            if(timer<=0 && currentIndex <targetMultiples.Length)
            {
                timer = 0f;
                Overgame();
            }
        }
    }
    public void UpdateTime()
    {
        int sec = Mathf.CeilToInt(timer);
        if (timetext != null)
        {
            timetext.text = "Time:" + sec.ToString()+"s";

        }
       
    }
    public int GetCurrentTarget()
    {
        return targetMultiples[currentIndex];
    }

    public void RegisterNumber(Number numberObj)
    {
        activeNumberObjects.Add(numberObj);
        numberObj.Setup(GetCurrentTarget()); // Ensure it uses current target immediately
    }

    public void OnNumberCollected(int numbercollected)
    {
        if (numbercollected == targetMultiples[currentIndex])
        {
            currentIndex++;

            if (currentIndex >= targetMultiples.Length)
            {
                Time.timeScale = 0f;
                nextLevelUI.SetActive(true);
                equationui.SetActive(false);
                timetext.enabled = false;
            }
            else
            {
                int newTarget = targetMultiples[currentIndex];
                foreach (var number in activeNumberObjects)
                {
                    if (number != null)
                    {
                        number.Setup(newTarget);
                    }
                }
            }
        }
        else
        {
            Time.timeScale = 0f;
            gameoverui.SetActive(true);
        }
    }
    public void Overgame()
    {
        isgamerunning = false;
        Time.timeScale = 0f;
        gameoverui.SetActive(true);
        equationui.SetActive(false);
        timetext.enabled = false;
    }

}



