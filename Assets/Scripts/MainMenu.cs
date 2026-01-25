using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private string gameSceneName = "Gameplay";
    private bool isPaused = false;
    
    public void Update()
    {

        //Pressionar Enter
        if (Input.GetKeyDown(KeyCode.Return) ) 
        {
            SceneManager.LoadSceneAsync(gameSceneName);
        }

        //Pressionar Escape
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Debug.Log("Saliendo del Juego...");
            Application.Quit();
        }

        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
             //Pressionar P 
            if (Input.GetKeyDown(KeyCode.P)) 
            {
            isPaused = !isPaused;
            
               if (isPaused)
               {
                Time.timeScale = 1;
                Debug.Log("Juego en marcha");
               }
               else 
               {
                Time.timeScale = 0;
                Debug.Log("Juego Pausado");
               }
            }  
        } 
    }
}