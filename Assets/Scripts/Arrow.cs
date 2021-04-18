using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public bool isRight;

    public int minDamage;
    public int maxDamage;

    private Rigidbody2D rig;
    private int x = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRight)
        {
            rig.velocity = Vector2.right * speed;

        }
        else
        {
            rig.velocity = Vector2.left * speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            System.Random rnd = new System.Random();
            int randomDamage = rnd.Next(minDamage, maxDamage + 1);
            collision.GetComponent<EnemyGuy>().Damage(randomDamage);
            Destroy(gameObject);
        }
    }
}
