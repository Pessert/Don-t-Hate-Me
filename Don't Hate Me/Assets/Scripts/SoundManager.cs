using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sfx_Jump, sfx_Landing, sfx_Coin, sfx_Jumper, sfx_NextStage, sfx_Die;

    #region Singleton
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get { return instance; }
    }
    #endregion

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
