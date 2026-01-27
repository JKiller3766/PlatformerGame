using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int Score = 0;

    //public delegate void OnScoredDelegate(int score);
    //public static event OnScoredDelegate OnScored;

    public static Action<int> OnScoreUpdated;

    private void OnEnable()
    {
        Coins.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        Coins.OnCoinCollected -= UpdateScore;
    }

    private void UpdateScore(Coins coin)
    {
        Score += coin.coinValue;

        OnScoreUpdated?.Invoke(Score);
    }
}
