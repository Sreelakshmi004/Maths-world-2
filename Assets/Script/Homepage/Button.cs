using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void onquit()
    {
        Application.Quit();
    }
   
}
