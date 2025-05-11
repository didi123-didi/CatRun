using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    public GameObject target;
    public float speed = 6;
    public float climbingSpeed = 3.5f;
    private float speedUp = 1.0f;
    Animator anim;

    private float jumpForce = 15.0f;
    private bool isGrounded;

    private Rigidbody2D rb;
    private bool isClimbing = false;
    private bool hasAWall = false;
    private Bounds wallBounds;
    private int Status = 0;
    private bool isGoingAfter = false;

    private DropControl dropControl;

    private Collider2D collidedWall;

    private CatMovement catMovement;
    private Transform confused;

    // Start is called before the first frame update
    void Start()
    {
        target = target == null ? GameObject.FindGameObjectWithTag("Player") : target;
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        dropControl = this.GetComponent<DropControl>();
        catMovement = target.GetComponent<CatMovement>();
        confused = transform.parent.Find("Confused");
        confused.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isGoingAfter)
        {
            Vector2 diff = target.transform.position - transform.position;
            float distance = diff.magnitude;
            if (distance < 30)
            {
                isGoingAfter = true;
            }
        }
        if (isGoingAfter)
        {
            if (catMovement.isHiding)
            {
                confused.gameObject.SetActive(true);

                rb.velocity = new Vector2(0, 0);
                anim.SetInteger("Status", 0);
            }
            else
            {
                confused.gameObject.SetActive(false);
                Bounds myBounds = this.GetComponent<Collider2D>().bounds;

                if (collidedWall)
                Debug.Log("isOnTheWall(target, collidedWall): " + isOnTheWall(target, collidedWall));

                if (collidedWall && isOnTheWall(target, collidedWall))
                {
                    Debug.Log("CLIMBIN!");
                    if (myBounds.min.y < collidedWall.bounds.max.y)
                    {
                        isClimbing = true;
                    }
                    else
                    {
                        isClimbing = false;
                    }
                }
                else
                {
                    isClimbing = false;
                }

                if (isClimbing)
                {
                    Status = 2;
                    rb.velocity = new Vector2(0, climbingSpeed);
                }
                else if (isGrounded)
                {
                    if (transform.position.y - target.transform.position.y > 2)
                    {
                        dropControl.Drop();
                    }
                    if (Mathf.Abs(target.transform.position.x - transform.position.x) > 1)
                    {
                        Status = 1;
                        var direction = Mathf.Sign(target.transform.position.x - transform.position.x);
                        rb.velocity = new Vector2(direction * speed * speedUp, rb.velocity.y);
                        transform.localScale = new Vector3(direction * transform.localScale.z, transform.localScale.y, transform.localScale.z);
                    }
                    else
                    {
                        Status = 0;
                    }
                }

                Vector2 pos = transform.position;
                Collider2D obstacle = Physics2D.OverlapPoint(pos, LayerMask.GetMask("blockWalls"));

                if (obstacle != null &&
                    target.transform.position.y > transform.position.y &&
                    Mathf.Abs(target.transform.position.x - transform.position.x) < 2 &&
                    isGrounded
                    )
                {
                    Status = 3;
                    Debug.Log("Player is inside the obstacle!");
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isGrounded = false;
                }

                anim.SetInteger("Status", Status);

                
            }
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isClimbing)
        {
            Bounds myBounds = this.GetComponent<Collider2D>().bounds;
            Bounds bounds = collision.collider.bounds;

            Vector3 centerPos = new Vector3(myBounds.center.x, myBounds.center.y, 0);
            if (bounds.Contains(centerPos) && collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
            {
                //Debug.Log("YYY " + collision.collider.name);
                collidedWall = collision.collider;
            }
            else
            {
                //collidedWall = null;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == collidedWall)
        {
            collidedWall = null;
        }
    }

    private bool isOnTheWall(GameObject target, Collider2D wall)
    {
        Transform tra = target.transform;
        Vector3 position = tra.position;
        Bounds wallBounds = wall.bounds;
        if (position.x >= wallBounds.min.x && position.x <= wallBounds.max.x && position.y >= wallBounds.max.y)
        {
            return true;
        }
        return false;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("SlowDownBadGuy"))
        {
            speedUp = 0.3f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(WaitToBackToNormalSpeed());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            
            StartCoroutine(WaitToGround());
        }
    }

    private IEnumerator WaitToGround()
    {
        yield return new WaitForSeconds(0.2f);
        isGrounded = true;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private IEnumerator WaitToBackToNormalSpeed()
    {
        yield return new WaitForSeconds(1f);
        speedUp = 1f;
    }
}
