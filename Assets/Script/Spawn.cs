using UnityEngine;

public class NumberSpawner : MonoBehaviour
{
    private int[] multiplesOfThree = { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30 };
    private int currentIndex = 0;
    public static NumberSpawner Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public int? GetNextMultipleOfThree()
    {
        if (currentIndex < multiplesOfThree.Length)
        {
            int next = multiplesOfThree[currentIndex];
            currentIndex++;
            return next;
        }
        else
        {
            return null; // No more multiples
        }
    }

    public bool AllSpawned()
    {
        return currentIndex >= multiplesOfThree.Length;
    }
}
