using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public int deathCount;
    public Transform mainCameraTf;
    
    [SerializeField] private TextMeshProUGUI deathCountText, timeCountText, stageNameText, descriptionText, timeText;

    [HideInInspector] public int min;
    [HideInInspector] public float sec;

    private bool timerStop, isTimerStart;

    #region Stage Names
    private const string stage_1Name = "1 - DEATH IS ONLY THE BEGINNING!";
    private const string stage_2Name = "2 - Learn by dying";
    private const string stage_3Name = "3 - Something different";
    private const string stage_4Name = "4 - Spikes";
    private const string stage_5Name = "5 - Sinkhole";
    private const string stage_6Name = "6 - Other Way";
    private const string stage_7Name = "7 - Invisible Thing";
    private const string stage_8Name = "8 - First thing you have to do";
    private const string stage_9Name = "9 - I'm getting annoyed";
    private const string stage_10Name = "10 - Jump Jump";
    private const string stage_11Name = "11 - More Jump";
    private const string stage_12Name = "12 - Go Back";
    private const string stage_13Name = "13 - Not Hurt";
    private const string stage_14Name = "14 - More Spikes";
    private const string stage_15Name = "15 - No Space To Step";
    private const string stage_16Name = "16 - Moonwalk";
    private const string stage_17Name = "17 - Break time";
    private const string stage_18Name = "18 - I'm getting dizzy";
    private const string stage_19Name = "19 - Painful stage";
    private const string stage_20Name = "20 - Just give up and hate this game";
    private const string stage_21Name = "21 - Goob job. you're winner";
    #endregion

    #region Descriptions
    private const string stage_1Description = "A,D or ¡ç¡æ - Move \nW or Space - Jump";
    private const string stage_2Description = "R - Restart";
    private const string stage_8Description = "You can't go to the next stage \nif you don't get coins";
    private const string stage_10Description = "+ Jump count";
    #endregion

    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
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

        Screen.SetResolution(1280, 720, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            deathCount++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //// Test
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    StageClear();
        //}
    }

    #region Scene Control

    private IEnumerator TimerRoutine()
    {
        if (isTimerStart) yield break;

        isTimerStart = true;
        while (true)
        {
            if (timerStop) yield break;

            sec += Time.deltaTime;
            timeText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);

            if ((int)sec > 59)
            {
                sec = 0;
                min++;
            }
            yield return null;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        deathCountText.text = "Death - " + deathCount.ToString("N0");

        if (!scene.name.Equals("GameClear")) { StartCoroutine(PlayerSpeedResetRoutine()); }

        if (scene.name.Equals("Stage_1")) 
        {
            StartCoroutine(TimerRoutine());
            stageNameText.text = stage_1Name; 
            descriptionText.text = stage_1Description;

            deathCountText.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
        }
        else if (scene.name.Equals("Stage_2")) { stageNameText.text = stage_2Name; descriptionText.text = stage_2Description; }
        else if (scene.name.Equals("Stage_3")) { stageNameText.text = stage_3Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_4")) { stageNameText.text = stage_4Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_5")) { stageNameText.text = stage_5Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_6")) { stageNameText.text = stage_6Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_7")) { stageNameText.text = stage_7Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_8")) { stageNameText.text = stage_8Name; descriptionText.text = stage_8Description; }
        else if (scene.name.Equals("Stage_9")) { stageNameText.text = stage_9Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_10")) { stageNameText.text = stage_10Name; descriptionText.text = stage_10Description; }
        else if (scene.name.Equals("Stage_11")) { stageNameText.text = stage_11Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_12")) { stageNameText.text = stage_12Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_13")) { stageNameText.text = stage_13Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_14")) { stageNameText.text = stage_14Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_15")) { stageNameText.text = stage_15Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_16")) { stageNameText.text = stage_16Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_17")) { stageNameText.text = stage_17Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_18")) { stageNameText.text = stage_18Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_19")) { stageNameText.text = stage_19Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_20")) { stageNameText.text = stage_20Name; descriptionText.text = null; }
        else if (scene.name.Equals("Stage_21")) { stageNameText.text = stage_21Name; descriptionText.text = null; }
        else if (scene.name.Equals("GameClear")) 
        {
            timerStop = true;
            stageNameText.gameObject.SetActive(false); 
            timeCountText.gameObject.SetActive(false); 
            deathCountText.gameObject.SetActive(false);
            descriptionText.text = null; 
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion

    private IEnumerator PlayerSpeedResetRoutine()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float originPlayerMoveSpeed = player.moveSpeed;

        player.rb.velocity = Vector2.zero;
        player.moveSpeed = 0;
        yield return new WaitForSeconds(0.2f);
        player.moveSpeed = originPlayerMoveSpeed;
    }

    public IEnumerator GameOverRoutine()
    {
        mainCameraTf.DOShakePosition(0.5f, 0.2f).SetId("CameraShaking").OnComplete(() => 
        {
            DOTween.Rewind("CameraShaking");
        });
        yield return new WaitForSeconds(0.7f);
        deathCount++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StageClear()
    {
        SoundManager.Instance.sfx_NextStage.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
