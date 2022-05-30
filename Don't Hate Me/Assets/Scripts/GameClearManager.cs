using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameClearManager : MonoBehaviour
{
    public TextMeshProUGUI deathCountText, timeText;

    private void Start()
    {
        deathCountText.text = string.Format("<#A20000>Death</color> - {0}", GameManager.Instance.deathCount);
        timeText.text = string.Format("<#B6FCFF>Time</color> - {0:D2}:{1:D2}", GameManager.Instance.min, (int)GameManager.Instance.sec);
    }
}
