using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SpriteRendererFrameAnimation : MonoBehaviour
{
    public List<Sprite> frames = new List<Sprite>();

    public float frameDuration = 0.1f;

    public bool loop = true;

    public float delayAfterPlay = 0f;
    public int goToSceneIndex;
    public UnityEvent onFinish;

    public bool isAutoPlay = true;

    private SpriteRenderer spriteRenderer;
    private Coroutine playCoroutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (isAutoPlay)
        {
            Play();
        }
    }


    public void Play()
    {
        gameObject.SetActive(true);
        if (playCoroutine != null)
            StopCoroutine(playCoroutine);

        playCoroutine = StartCoroutine(PlayFrames());
    }


    public void Stop()
    {
        if (playCoroutine != null)
            StopCoroutine(playCoroutine);

        playCoroutine = null;
    }

    private IEnumerator PlayFrames()
    {
        do
        {
            for (int i = 0; i < frames.Count; i++)
            {
                spriteRenderer.sprite = frames[i];
                yield return new WaitForSeconds(frameDuration);
            }

            if (!loop)
            {
                if (delayAfterPlay > 0f)
                    yield return new WaitForSeconds(delayAfterPlay);
                SceneManager.LoadScene(goToSceneIndex);
                onFinish?.Invoke();
            }

        } while (loop);
    }
}
