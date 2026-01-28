using UnityEngine;
using UnityEngine.UI;

public class TextEnding : MonoBehaviour
{
    public Text label;

    private void OnEnable()
    {
        if (ScoreSystem.Win) label.text = ("Level Finished, Congratulations!");
        else label.text = ("You died, Game Over!");
    }
}