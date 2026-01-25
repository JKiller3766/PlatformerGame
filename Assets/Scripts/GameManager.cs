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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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



    // Update is called once per frame
    void Update()
    {
        
    }
}
