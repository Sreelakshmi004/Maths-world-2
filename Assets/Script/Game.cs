using UnityEngine;

public class Game : MonoBehaviour
{
    public static bool gameover;
    public GameObject gameoverui;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
        {
            Time.timeScale = 0;
            gameoverui.SetActive(true);
        }
    }
  
}
