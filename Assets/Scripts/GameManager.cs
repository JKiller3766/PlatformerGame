using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int totalCoins = 0;
    public TMP_Text coinText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Coins.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        Coins.OnCoinCollected -= HandleCoinCollected;
    }

    private void HandleCoinCollected(Coins coin)
    {
        AddCoins(coin.coinValue);
    }

    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoins(int coinValue)
    {
        totalCoins += coinValue;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Monedas: {totalCoins}";
        }
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }
}
