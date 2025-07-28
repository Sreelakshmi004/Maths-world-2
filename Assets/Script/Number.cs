using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{
    public int number;

    public TextMeshPro text;
    public int GetValue()
    {
        return number;
    }


    public void Setup(int number)
    {
        this.number = number;

        if (text == null)
            text = GetComponentInChildren<TextMeshPro>();

        if (text != null)
            text.text = number.ToString();
        else
            Debug.LogError("TextMeshPro not found on: " + gameObject.name);


        if (number % 3 == 0)
            gameObject.tag = "Collectable";
    }
}
