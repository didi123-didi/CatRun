using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInGrassManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer sr = collision.transform.GetComponent<SpriteRenderer>();
        CatMovement catMovement = collision.transform.GetComponent<CatMovement>();
        if (catMovement)
        {
            catMovement.setHidden(true);
        }
        Color color = sr.color;
        color.a = 0.5f;
        sr.color = color;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SpriteRenderer sr = collision.transform.GetComponent<SpriteRenderer>();
        CatMovement catMovement = collision.transform.GetComponent<CatMovement>();
        if (catMovement)
        {
            catMovement.setHidden(false);
        }
        Color color = sr.color;
        color.a = 1.0f;
        sr.color = color;
    }
}
