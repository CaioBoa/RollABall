using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpSquare : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce = 1f;
    public float jumpCd = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jumpCd <= 0)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCd = 1f;
            }
        else
            {
                jumpCd -= Time.deltaTime;
            }
    }
}
