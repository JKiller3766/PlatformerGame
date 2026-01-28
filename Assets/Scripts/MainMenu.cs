using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Gameplay";
    
    public void Update()
    {
        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}