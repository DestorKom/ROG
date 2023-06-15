using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour
{
    
   
    public Animator Animation;
    public BulletEnemy Bullet;
    public Room room;
    int hp = 60;
    int movingSpeed = -10;
    float lastTime;



    int vid;
    delegate void moving();
    private void Start()
    {
        
        vid = Random.Range(0,2);
       
        if (vid == 0)
        {
            Animation.SetBool("vid",true);
        }
        if (vid == 1)
        {
            StartCoroutine(SomeCoroutine());
            Animation.Play("Walk");
            Animation.SetBool("vid", false);
        }
        
        
    }
    private IEnumerator SomeCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
    }
    private void FixedUpdate()
    {
        if (vid==1)
        {
            transform.position += transform.right * movingSpeed * Time.deltaTime;
        }
    }
    public void Attack()
    {
        Fire(1f);
    }
    public void Teleport()
    {
        transform.position = new Vector3(room.transform.position.x + Random.Range(-15, 15), room.transform.position.y + Random.Range(-5, 5), -1);
    }
    public void TeleportDebug()
    {
        transform.position = new Vector3(transform.position.x- 2,transform.position.y, -1);
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
    bool nap = true;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {

            if (!nap)
            {
                GetComponent<SpriteRenderer>().flipX = nap;
                nap = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = nap;
                nap = false;
            }
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
                    room.Die(20);
                }
            }
        }
    }
}
