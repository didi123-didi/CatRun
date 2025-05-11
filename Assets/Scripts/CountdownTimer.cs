using UnityEngine;
using UnityEngine.UI;

public class SimpleCountdown : MonoBehaviour
{
    public Text countdownText;       // 設定要顯示的 UI Text（普通 UI Text）
    public Canvas guideline;
    public GameObject player;        // 玩家物件，先暫時禁用
    public Text Timer;
    public GameOverTrigger gameOverTrigger;
    public GoalTrigger goalTrigger;

    private bool isGuidelineFinished = false;
    private float countdown = 3f;
    public float TimeLimit = 180.0f;
    private float starTime;
    private bool countdownFinished = false;

    void Start()
    {
        player.SetActive(false); // 禁用角色
    }

    void Update()
    {
        if (Input.anyKey && !isGuidelineFinished)
        {
            isGuidelineFinished = true;
            guideline.enabled = false;
        }

        if (!countdownFinished && isGuidelineFinished)
        {
            countdown -= Time.deltaTime;

            if (countdown > 0)
            {
                countdownText.text = Mathf.Ceil(countdown).ToString();
            }
            else
            {
                countdownFinished = true;
                countdownText.text = "GO!";
                starTime = Time.time;
                player.SetActive(true); // 啟用角色
                Invoke("ClearText", 0.5f); // 0.5 秒後清除文字
            }
        }

        if (countdownFinished && !gameOverTrigger.isLost && !goalTrigger.isWon)
        {
            float usedTime = Time.time - starTime;
            float remainingTime = TimeLimit - usedTime;

            if (remainingTime <= 0)
            {
                gameOverTrigger.Lose();
            }
            else
            {
                int m = Mathf.FloorToInt(remainingTime / 60);
                int s = Mathf.FloorToInt(remainingTime) - m * 60;

                Timer.text = m.ToString("D2") + ":" + s.ToString("D2");
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && isGuidelineFinished)
        {
            Time.timeScale = Time.timeScale == 0 ? 1.0f : 0f;
        }
    }

    //void StartTimer ()
    //{
    //    Timer.text = "";

    //}

    void ClearText()
    {
        countdownText.gameObject.SetActive(false);
    }
}