using UnityEngine;
using UnityEngine.SceneManagement;
public class Button : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
