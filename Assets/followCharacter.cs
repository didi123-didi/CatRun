using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCharacter : MonoBehaviour
{
    private Transform police;
    // Start is called before the first frame update
    void Start()
    {
        police = transform.parent.Find("Police");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(police.position.x, police.position.y + 3.5f, police.position.z);



        transform.position = newPosition;
    }
}
