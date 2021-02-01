 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public float speed;
    public float jumpForce;
    private float movement;

    private Rigidbody2D rig;
    private Animator anim;
    public GameObject arrow;
    public Transform firePoint;

    private bool isJumping;
    private bool doubleJump;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameController.instance.UpdateLives(health);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        BowFire();
    }
    private void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if(movement > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0,0,0);
        }
        
        if(movement < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(movement == 0 && !isJumping && !isShooting)
        {
            anim.SetInteger("transition", 0);
        }
        
    }
    
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                isJumping = true;
            }
            else
            {
                if (doubleJump)
                {
                    anim.SetInteger("transition", 2);
                    rig.AddForce(new Vector2(0, jumpForce * 1), ForceMode2D.Impulse);
                    doubleJump = false;

                }
            }

        }

        
    }

    private void BowFire()
    {
        StartCoroutine("Fire");
        
    }
    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isShooting = true;
            anim.SetInteger("transition", 3);
            GameObject Arrow = Instantiate(arrow, firePoint.position, firePoint.rotation);
            if(transform.rotation.y == 0)
            {
                Arrow.GetComponent<Arrow>().isRight = true;
            }

            else if (transform.rotation.y == 180)
            {
                Arrow.GetComponent<Arrow>().isRight = false;
            }
            yield return new WaitForSeconds(0.3f);
            isShooting = false;
            anim.SetInteger("transition", 0);
        }
    }

    public void IncreaseHealth(int value)
    {
        health += value;
        GameController.instance.UpdateLives(health);
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        GameController.instance.UpdateLives(health);
        anim.SetTrigger("hit");

        if(transform.rotation.y == 0)
        {
            transform.position += new Vector3(-0.5f, 0, 0);
        }
        if(transform.rotation.y == 180)
        {
            transform.position += new Vector3(0.5f, 0, 0);
        }

        if (health <= 0)
        {
            GameController.instance.GameOver();
       }
    }

    public void SpikeDamage(int dmg)
    {
        health -= dmg;
        GameController.instance.UpdateLives(health);
        anim.SetTrigger("hit");

        if(transform.rotation.y == 0)
        {
            transform.position += new Vector3(-0.5f, 0.4f, 0);
        }
        if(transform.rotation.y == 180)
        {
            transform.position += new Vector3(0.5f, -0.4f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            //doubleJump = false;

        }
    }
}
