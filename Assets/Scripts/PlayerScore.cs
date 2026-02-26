using UnityEngine;

public class PlayerScore
{
    static int score = 0;

    public static void UpdateScore(int amount)
    {
        score += amount;
        Debug.Log($"Добавлено {amount} очков.");
        Debug.Log($"Текущее количество очков: {score}");
    }
}
