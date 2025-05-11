using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public EnergyBar energy;
    public float energyCost = 0.1f;
    public float energyGain = 20f;
    public float translationSpeed = 4.0f;
    public float jumpForce = 5.0f;
    public bool isHiding { get; private set; } = false;
    private bool isGrounded;
    public bool isAllowDoubleJump = false;
    private bool canDoubleJump = false;
    private Rigidbody2D rb;
    private int status = 0;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        status = 0;
        var horizonValue = Input.GetAxis("Horizontal");
        var speedUp = 1.0f;
        var isRunning = false;
        if (horizonValue != 0 && Input.GetKey(KeyCode.LeftShift) && energy.currentEnergy >= energyCost)
        {
            isRunning = true;
            speedUp  = 2.0f;
            energy.UseEnergy(energyCost);
            
        }
        

        float translation = horizonValue * translationSpeed * speedUp;
        //Debug.Log("translation:" + translation);
        if (translation != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(translation), 1, 1);
            status = 1;
        }
        rb.velocity = new Vector2(translation, rb.velocity.y);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGrounded = false;
                if (isAllowDoubleJump)
                    canDoubleJump = true;
            }
            else if (isAllowDoubleJump && canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
        if (!isGrounded)
        {

            status = 2;
            isRunning = false;
        }

        if (status == 0)
        {
            energy.GainEnergy(0.005f);
        }

        anim.SetInteger("Status", status);
        anim.SetBool("IsRunning", isRunning);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("AttackTrigger");
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision.contacts:" + collision.contacts[0].normal);
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            //return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            energy.GainEnergy(energyGain);
        }
    }

    public bool GetIsGrounded ()
    {
        return isGrounded;
    }

    public void setHidden (bool value)
    {
        isHiding = value;
    }

}
