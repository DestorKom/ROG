using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Unity.Mathematics;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class personage : MonoBehaviour
{

    public int movingSpeed = 2;

    //public Animator Animation;
    //public SpriteRenderer sprite;
    public Camera camera;
    public BulletPers Bullet;
    public UnityEngine.UI.Image image;
    public Room[,] spawnedRooms;
    public int counter = 0;
    public GameObject canvas;
    public Sprite spriteD;
    public Sprite spriteLR;
    public Sprite spriteU;
    public Text reloadT;
    public Text pointT;

    int hp = 100;
    int Maxhp = 100;
    
    float reload = 1.3f;
   
    float lastTime;
   
    private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * movingSpeed * Time.deltaTime;
            GetComponent<SpriteRenderer>().sprite = spriteU;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.up * movingSpeed * Time.deltaTime;
            GetComponent<SpriteRenderer>().sprite = spriteD;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * movingSpeed * Time.deltaTime;
            GetComponent<SpriteRenderer>().sprite = spriteLR;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * movingSpeed * Time.deltaTime;
            GetComponent<SpriteRenderer>().sprite = spriteLR;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Time.realtimeSinceStartup - lastTime > reload)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity).nap = new Vector3(transform.position.x + 1, transform.position.y, 0);
                lastTime = Time.realtimeSinceStartup;
            }

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Time.realtimeSinceStartup - lastTime > reload)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity).nap = new Vector3(transform.position.x - 1, transform.position.y, 0);
                lastTime = Time.realtimeSinceStartup;
            }

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Time.realtimeSinceStartup - lastTime > reload)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity).nap = new Vector3(transform.position.x, transform.position.y+1, 0);
                lastTime = Time.realtimeSinceStartup;
            }

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Time.realtimeSinceStartup - lastTime > reload)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity).nap = new Vector3(transform.position.x, transform.position.y - 1, 0);
                lastTime = Time.realtimeSinceStartup;
            }

        }
        if (Input.GetMouseButton(0))
        {
            if (Time.realtimeSinceStartup - lastTime > reload)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity).nap = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                lastTime = Time.realtimeSinceStartup;
            }

        }

    }
    public void DyeEnemy()
    {
        pointT.text = "Point: " + counter;
    }
    float damagetimer;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("HP"))
        {
            if (hp + 20 >= Maxhp)
            {
                hp = Maxhp;               
            }
            else
                hp += 20;
            image.fillAmount = (float)hp / (float)Maxhp;
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("BUF"))
        {
            if (reload - 0.4f > 0)
                reload -= 0.4f;
            else
                reload = 0.1f;
            reloadT.text = "Reload: " + reload;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("POINT"))
        {
            counter += 10;
            pointT.text = "Point: " + counter;
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Enemy"))
        {

            if (Time.realtimeSinceStartup - damagetimer > 1f)
            {
                hp -= 5;
                image.fillAmount = (float)hp / (float)Maxhp;
                damagetimer = Time.realtimeSinceStartup;
                image.fillAmount = (float)hp / (float)Maxhp;
            }
        }

        if (col.gameObject.CompareTag("BulletEnemy"))
        {
            if (Time.realtimeSinceStartup - damagetimer > 1f)
            {
                BulletEnemy Bullet = col.GetComponent<BulletEnemy>();
                hp -= Bullet.Damage;
                image.fillAmount = (float)hp / (float)Maxhp;
                Destroy(col.gameObject);
                damagetimer = Time.realtimeSinceStartup;
            }

        }


        if (hp <= 0)
        {        
            Destroy(this.gameObject);
            Time.timeScale = 0f;
            canvas.SetActive(true);
        }

        if (col.gameObject.CompareTag("DoorL"))
        {
            transform.position = new Vector3(transform.position.x - tpX, transform.position.y, -1);
            camera.transform.position = new Vector3(camera.transform.position.x - 50, camera.transform.position.y,-10);

        }
        if (col.gameObject.CompareTag("DoorR"))
        {
            transform.position = new Vector3(transform.position.x + tpX, transform.position.y, -1);
            camera.transform.position = new Vector3(camera.transform.position.x + 50, camera.transform.position.y ,- 10);

        }
        if (col.gameObject.CompareTag("DoorU"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + tpY, -1);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 50 ,- 10);
        }
        if (col.gameObject.CompareTag("DoorD"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - tpY, - 1);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 50 ,- 10);
        }
    }
    int tpX = 12;
    int tpY = 32;

}
