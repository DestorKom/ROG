using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class personage : MonoBehaviour
{
  
    public int movingSpeed = 2;
    public Animator Animation;
    public SpriteRenderer sprite;
    public Camera camera;
    public bullet Bullet; 
    private void Start()
    {
        Animation = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    float lastTime;
    private void Update()
    {
      
       
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
        //if (Input.GetMouseButtonUp(0))

        if (Input.GetMouseButton(0))
        {
            if (Time.realtimeSinceStartup - lastTime > 0.5f)
            {
           

                Animation.Play("attack");
                if (sprite.flipX == true)
                {

                    Instantiate(Bullet, new Vector3(transform.position.x + 2, transform.position.y, -1), Quaternion.identity).vector = true;
                }
                else
                {
                    Instantiate(Bullet, new Vector3(transform.position.x - 2, transform.position.y, -1), Quaternion.identity).vector = false;
                }
                lastTime = Time.realtimeSinceStartup;
            }

        }

        Animation.SetFloat("Move", Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")));
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DoorL")
        {
            transform.position = new Vector3(transform.position.x - 40, transform.position.y, -1);
            camera.transform.position = new Vector3(camera.transform.position.x - 50, camera.transform.position.y, -10);
        }
        if (col.gameObject.tag == "DoorR")
        {
            transform.position = new Vector3(transform.position.x + 40, transform.position.y, -1);
            camera.transform.position = new Vector3(camera.transform.position.x + 50, camera.transform.position.y, -10);
        }
        if (col.gameObject.tag == "DoorU")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y+40, -1);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 50, -10);
        }
        if (col.gameObject.tag == "DoorD")
        {
            transform.position = new Vector3(transform.position.x , transform.position.y-40, -1);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 50, -10);
        }
    }

}
