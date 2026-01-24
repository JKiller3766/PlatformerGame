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
    public int coinValue = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
