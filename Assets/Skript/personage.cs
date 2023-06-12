using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class personage : MonoBehaviour
{

    public int movingSpeed = 2;

    public Animator Animation;
    public SpriteRenderer sprite;
    public Camera camera;
    public BulletPers Bullet;
    int hp = 100;
    public Room[,] spawnedRooms;
    public int counter = 0;
    public int BUF = 0;
    int x = 5, y = 5;

    private void Start()
    {
        Animation = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    float lastTime;
    private void FixedUpdate()
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


        if (Input.GetMouseButton(0))
        {
            if (Time.realtimeSinceStartup - lastTime > 0.5f)
            {
                Animation.Play("attack");
                for (float i = -BUF * 3; i <= BUF * 3; i += 3)
                {
                    if(i!=0f)
                        Instantiate(Bullet, transform.position, Quaternion.identity).nap = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + i, Input.mousePosition.y + i, 0));
                }
                if(BUF%2==0)
                    Instantiate(Bullet, transform.position, Quaternion.identity).nap = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lastTime = Time.realtimeSinceStartup;
            }

        }

        Animation.SetFloat("Move", Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")));
    }
    float damagetimer;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "HP(Clone)")
        {
            hp += 20;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "BUF(Clone)") {
            BUF++;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Enemy"))
        {
           
            if (Time.realtimeSinceStartup - damagetimer > 1f)
            {
                hp -= 5;
                damagetimer = Time.realtimeSinceStartup;
                
            }
        }
            if (col.gameObject.CompareTag("BulletEnemy"))
        {
            BulletEnemy Bullet = col.GetComponent<BulletEnemy>();
            hp -= Bullet.Damage;
            Destroy(col.gameObject);
            
        }
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }

        if (col.gameObject.CompareTag("DoorL"))
        {


            x--;
            
            transform.position = new Vector3(transform.position.x - 40, transform.position.y, -1);
            camera.transform.position = new Vector3(camera.transform.position.x - 50, camera.transform.position.y, -10);
            if (spawnedRooms[x, y].tag == "Monster" || spawnedRooms[x, y].tag == "Boss")
            {
                spawnedRooms[x, y].LockedDoor();
            }

        }
        if (col.gameObject.CompareTag("DoorR"))
        {
            x++;
           
            transform.position = new Vector3(transform.position.x + 40, transform.position.y, -1);
            camera.transform.position = new Vector3(camera.transform.position.x + 50, camera.transform.position.y, -10);
            if (spawnedRooms[x, y].tag == "Monster" || spawnedRooms[x, y].tag == "Boss")
            {
                spawnedRooms[x, y].LockedDoor();
            }
        }
        if (col.gameObject.CompareTag("DoorU"))
        {
            
            y++;
            
            transform.position = new Vector3(transform.position.x, transform.position.y + 40, -1);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 50, -10);
            if (spawnedRooms[x, y].tag == "Monster" || spawnedRooms[x, y].tag == "Boss")
            {
                spawnedRooms[x, y].LockedDoor();
            }
        }
        if (col.gameObject.CompareTag("DoorD"))
        {
            y--;
           
            transform.position = new Vector3(transform.position.x, transform.position.y - 40, -1);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 50, -10);
            if (spawnedRooms[x, y].tag == "Monster"|| spawnedRooms[x, y].tag =="Boss")
            {
                spawnedRooms[x, y].LockedDoor();
            }
                           

        }
    }

    }
