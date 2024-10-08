using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveTimer = 5.0f;
    private Rigidbody rb;
    private bool Active;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        Active = false;
    }

    void Move()
    {
        rb.AddForce((player.position - transform.position).normalized * 700 * Time.deltaTime);
    }

    void Update()
    {   
        if (Active == true){
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0)
            {
                Move();
                if (moveTimer <= -1.0f){
                    moveTimer = 3.0f;
                }
            }
            if (transform.position.y < -10)
            {
                transform.position = new Vector3(0, 0.5f, 0);
            }
        }
        else {
            if (Vector3.Distance(player.position, transform.position) < 20)
                {
                    Active = true;
                }
        }
    }
}
