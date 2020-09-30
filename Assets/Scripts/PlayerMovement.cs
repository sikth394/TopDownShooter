using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    Animator animator;
    public Animator childAnim;


    private void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (movement.x != 0 || movement.y != 0)
        {
            childAnim.SetBool("walk", true);
        }
        else
        {
            childAnim.SetBool("walk", false);  
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg ;
        rb.rotation = angle;
    }
}
