using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personage : MonoBehaviour
{
  
    public int movingSpeed = 2;
    public Animator Animation;
    public SpriteRenderer sprite;

    private void Start()
    {
        Animation = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Animation.Play("attack");
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * movingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.up * movingSpeed * Time.deltaTime;
           
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * movingSpeed * Time.deltaTime;
            sprite.flipX = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * movingSpeed * Time.deltaTime;
            sprite.flipX = true;
        }
        Animation.SetFloat("Move", Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")));
    }
}
