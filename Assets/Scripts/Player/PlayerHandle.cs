using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandle : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    public Animator atkAnim;
    public Animator moveAnim;
    private float moveH, moveV;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal") * moveSpeed;
        moveV = Input.GetAxis("Vertical") * moveSpeed;
        if (moveH < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
        Attack();
    }

    private void FixedUpdate()
    {
        Movement(); 
    }

    private void Movement()
    {
        rb.velocity = new Vector2(moveH, moveV);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            atkAnim.SetTrigger("isAttack");
        }
    }
}
