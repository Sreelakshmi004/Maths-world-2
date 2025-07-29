using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{
    public int number;
    public TextMeshPro text;

    void Start()
    {
        Game.Instance.RegisterNumber(this);
    }

    public int GetValue()
    {
        return number;
    }

    public void Setup(int number)
    {
       this. number = number;

        if (text == null)
            text = GetComponentInChildren<TextMeshPro>();

        if (text != null)
            text.text = number.ToString();
        else
            Debug.LogError("TextMeshPro not found");
        if (number % 3 == 0) { gameObject.tag = "Collectable"; }
        // Always collectable (current target only)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Game.Instance.OnNumberCollected(number);
            Destroy(gameObject);
        }
    }
}

