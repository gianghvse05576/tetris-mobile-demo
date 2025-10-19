using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<int> OnScoreAdded;
    public static event Action<int> OnLineCleared;
    public static event Action<int> OnLevelUp;
    public static event Action OnGameOver;

    public static void RaiseScoreAdded(int amount) => OnScoreAdded?.Invoke(amount);
    public static void RaiseLineCleared(int lines) => OnLineCleared?.Invoke(lines);
    public static void RaiseLevelUp(int level) => OnLevelUp?.Invoke(level);
    public static void RaiseGameOver() => OnGameOver?.Invoke();
}
