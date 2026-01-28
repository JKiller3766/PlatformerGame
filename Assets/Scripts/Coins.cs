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

    public CoinType coinType = CoinType.Bronce;
    public int coinValue;
    public static event Action<Coins> OnCoinCollected;

    void Start()
    {
        AssignValuePerType();
    }

    void AssignValuePerType()
    {
        switch(coinType)
        {
            case CoinType.Bronce:
                coinValue = 1;
                break;
            case CoinType.Silver:
                coinValue = 3;
                break;
            case CoinType.Gold:
                coinValue = 5;
                break;
            case CoinType.Platinum:
                coinValue = 10;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCoinCollected?.Invoke(this);
        Destroy(gameObject);
    }
}
