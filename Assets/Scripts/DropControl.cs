using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropControl : MonoBehaviour
{
    private float dropDuration = 0.8f; // 多久後恢復碰撞
    private LayerMask platformLayer;   // 指定平台圖層（Props 或 Platform）
    private Transform groundCheck;
    private float checkRadius = 0.2f;

    private void Start()
    {
        platformLayer = LayerMask.GetMask("Walls");
        groundCheck = transform.Find("GroundCheck");
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S) && transform.tag == "Player")
        {
            Debug.Log("platformLayer: " + platformLayer);
            Drop();
        }
    }

    public void Drop()
    {

        Collider2D platform = Physics2D.OverlapCircle(groundCheck.position, checkRadius, platformLayer);
        if (platform != null)
        {
            StartCoroutine(TemporarilyDisableCollision(platform));
        }
    }

    private System.Collections.IEnumerator TemporarilyDisableCollision(Collider2D platform)
    {
        Collider2D myCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(myCollider, platform, true);
        yield return new WaitForSeconds(dropDuration);
        Physics2D.IgnoreCollision(myCollider, platform, false);
    }
}
