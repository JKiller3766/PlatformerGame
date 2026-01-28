using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class Ending : MonoBehaviour
{
    public enum EndingType { Death = 0, Win = 1 }

    private static EndingType s_type = EndingType.Death;

    [Header("UI")]
    [SerializeField] private TMP_Text textEnding;

    [Header("Scene Names")]
    [SerializeField] private string restartSceneName = "Gameplay";

    [Header("Messages")]
    [SerializeField] private string deathTitle = "Game Over";
    [SerializeField] private string winTitle = "You Win!";

    private void Awake()
    {
        if (textEnding == null)
            textEnding = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        Time.timeScale = 1;

        int score = (GameManager.instance != null) ? GameManager.instance.GetTotalCoins() : 0;
        string title = (s_type == EndingType.Win) ? winTitle : deathTitle;

        textEnding.text =
            $"{title}\n\n" +
            $"Monedas: {score}\n\n" +
            "Press ENTER to restart\n" +
            "Press ESC to exit";
    }

    private void Update()
    {
        if (IsEnterPressed())
        {
            if (GameManager.instance != null)
                GameManager.instance.totalCoins = 0;

            Time.timeScale = 1;
            SceneManager.LoadSceneAsync(restartSceneName);
        }

        if (IsEscapePressed())
        {
            Debug.Log("Saliendo del Juego...");
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    public static void GoToEnding(EndingType type, string endingSceneName = "Ending")
    {
        s_type = type;
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(endingSceneName);
    }

    private bool IsEnterPressed()
    {
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
            return Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.numpadEnterKey.wasPressedThisFrame;
#endif
        return Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
    }

    private bool IsEscapePressed()
    {
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
            return Keyboard.current.escapeKey.wasPressedThisFrame;
#endif
        return Input.GetKeyDown(KeyCode.Escape);
    }
}



