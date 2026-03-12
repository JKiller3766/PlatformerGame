using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public enum CoinType 
    {
        Bronce = 1,
        Silver = 3,
        Gold = 5,
        Platinum = 10  
    }

    public CoinType CoinsType = CoinType.Bronce;
    public int CoinValue;
    public static event Action<Coins> OnCoinCollected;

    void Start()
    {
        AssignValuePerType();
    }

    void AssignValuePerType()
    {
        switch(CoinsType)
        {
            case CoinType.Bronce:
                CoinValue = 1;
                break;
            case CoinType.Silver:
                CoinValue = 3;
                break;
            case CoinType.Gold:
                CoinValue = 5;
                break;
            case CoinType.Platinum:
                CoinValue = 10;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCoinCollected?.Invoke(this);
        Destroy(gameObject);
    }
}
