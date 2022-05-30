using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jumper : MonoBehaviour
{
    private Transform tf;
    private SpriteRenderer sr;
    private CircleCollider2D cCol;
    private ParticleSystem ps_Get;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cCol = GetComponent<CircleCollider2D>();
        tf = GetComponent<Transform>();

        ps_Get = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        tf.DOLocalMoveY(0.2f, 0.5f).SetId("Move").SetRelative().SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        DOTween.Rewind("Move");
    }

    public void GetJumper()
    {
        ps_Get.Play();
        sr.enabled = false;
        cCol.enabled = false;
        StartCoroutine(AutoRespawnRoutine());
    }

    private IEnumerator AutoRespawnRoutine()
    {
        yield return new WaitForSeconds(2f);
        sr.enabled = true;
        cCol.enabled = true;
    }
}
