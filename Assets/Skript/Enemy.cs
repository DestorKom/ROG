using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
   
    public Animator Animation;
    public BulletEnemy Bullet;
    public Room room;
    int hp = 40;
    int movingSpeed = 10;
    float Timer;
    float lastTime;
 



    delegate void moving();
    moving move;
    private void Start()
    {
        int vid = Random.Range(0, 2);
       
        if (vid == 0)
        {
            move = Teleporter;
        }
        if (vid == 1)
        {
            move = Runer;
        }
        StartCoroutine(SomeCoroutine());
    }
    bool start = false;
    private IEnumerator SomeCoroutine()
    {
        yield return new WaitForSeconds(1);
        start = true;
    }
    private void FixedUpdate()
    {
        if (start)
        {                
            move();
        }
    }
 
    void Teleporter()
    {
        if (Time.realtimeSinceStartup - Timer > 2f)
        {
            
            Animation.Play("Cast");
            transform.position = new Vector3(room.transform.position.x + Random.Range(-15, 15), room.transform.position.y + Random.Range(-5, 5), -1);
            Timer = Time.realtimeSinceStartup;
            Fire(1f);
        }
    }
    void Runer()
    {

        Fire(1f);
        Animation.Play("Walk");
        transform.position += transform.right * movingSpeed * Time.deltaTime;
    }

    public void Fire(float reload)
    {
        Vector3 pers = new Vector3(0, 0, 0);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Slime"))
        {
            pers = go.transform.position;
        }
        if (Time.realtimeSinceStartup - lastTime > reload)
        {

            Instantiate(Bullet, transform.position, Quaternion.identity).nap = pers;
            lastTime = Time.realtimeSinceStartup;
        }
    }
    float damagetimer;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            movingSpeed *= -1;
        }

        if (col.gameObject.CompareTag("BulletPers"))
        {
            BulletPers Bullet = col.GetComponent<BulletPers>();
            Destroy(col.gameObject);
            if (Time.realtimeSinceStartup - damagetimer > 0.1f)
            {                
                hp -= Bullet.Damage;                
                if (hp <= 0)
                {
                    GetComponent<Collider2D>().enabled = true;
                    Destroy(this.gameObject);
                    room.Die(10);
                }
            }
        }
    }
}
