using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreTextEnding : MonoBehaviour
{
    public Text label;

    

    private void OnEnable()
    {
        label.text = ("Score:" + ScoreSystem.Score);
    }
}
