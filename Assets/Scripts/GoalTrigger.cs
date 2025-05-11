using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GoalTrigger : MonoBehaviour
{
    public GameObject virtualCamera;        // 虛擬攝影機（讓它停住）
    public Text winText;                    // 顯示 "你贏了！"
    public Text instructionText;            // 顯示 "點擊任意鍵回主選單"
    private string interfaceSceneName = "Interface";  // 主選單 Scene 名稱

    public bool isWon { get; private set; } = false;
    public bool hasWon { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Win();
        }
    }

    private void Update()
    {
        if (hasWon && Input.anyKeyDown)
        {
            SceneManager.LoadScene(interfaceSceneName);
        }
    }

    public void Win ()
    {
        isWon = true;

        GameObject cat = GameObject.FindGameObjectWithTag("Player");
        GameOverTrigger gameOverTrigger = cat.GetComponent<GameOverTrigger>();
        gameOverTrigger.isActive = false;

        virtualCamera.SetActive(false);

        winText.text = "🎉 You win！";


        GameObject[] policeList = GameObject.FindGameObjectsWithTag("BadGuy");

        foreach (GameObject police in policeList)
        {
            MonoBehaviour[] scripts = police.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }

            Rigidbody2D rb = police.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            Animator animator = police.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }
        }

        StartCoroutine(WaitToEnableExit());

        
    }

    private IEnumerator WaitToEnableExit()
    {
        yield return new WaitForSeconds(2f);
        hasWon = true;
        instructionText.text = "Press any key to return.";
    }
}