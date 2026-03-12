using UnityEngine;
using UnityEngine.UI;

public class TextEnding : MonoBehaviour
{
    public Text Label;

    private void OnEnable()
    {
        if (ScoreSystem.Win) Label.text = ("Level Finished, Congratulations!");
        else Label.text = ("You died, Game Over!");
    }
}