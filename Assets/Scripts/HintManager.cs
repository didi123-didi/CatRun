using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    Animator anim;
    RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        anim.SetBool("IsOpen", !anim.GetBool("IsOpen"));
        Transform toggletext = transform.Find("ToggleButton/ToggleButtonText");
        toggletext.GetComponent<Text>().text = anim.GetBool("IsOpen") ? "<" : ">";
    }
}
