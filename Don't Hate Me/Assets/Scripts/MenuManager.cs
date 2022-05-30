using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI startText;

    private bool isReadyForStart;

    private void Start()
    {
        StartCoroutine(WaitForStartRoutine());
    }

    private void Update()
    {
        if (Input.anyKeyDown && isReadyForStart)
        {
            SceneMove("Stage_1");
        }
    }

    private IEnumerator WaitForStartRoutine()
    {
        yield return new WaitForSeconds(2f);
        isReadyForStart = true;
        startText.DOFade(1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void SceneMove(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
