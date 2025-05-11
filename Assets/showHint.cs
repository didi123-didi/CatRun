using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHint : MonoBehaviour
{
    public Canvas hint;

    private void Start()
    {
        hint.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hint.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hint.enabled = false;
    }
}
