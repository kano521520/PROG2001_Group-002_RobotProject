using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Game2Mgr : MonoBehaviour
{
    public Text scoreTxt;
    public int rubbishCount;
    public UnityEvent onFinish;

    int score;
    private void Start()
    {
        scoreTxt.text = "Collected rubbish: " + score + "/" + rubbishCount;
    }
    public void CollectRubbish()
    {
        score++;
        scoreTxt.text = "Collected rubbish: " + score + "/" + rubbishCount;
        if (score >= rubbishCount)
        {
            onFinish.Invoke();
        }
    }
}
