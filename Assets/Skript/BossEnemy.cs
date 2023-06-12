using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public Animator Animation;
    public BulletEnemy Bullet;
    public Rigidbody2D rb;
    public Room room;
    int hp = 200;
    int movingSpeed = 10;
    float Timer;
    float lastTime;
    bool start = false;
    Vector3 lastVelocity;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SomeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            lastVelocity = rb.velocity;
            if (Time.realtimeSinceStartup - lastTime > 3f)
            {
                if (Random.Range(0, 2) == 0)
                    StartCoroutine(Attack1());
                else
                    StartCoroutine(Attack2());
                lastTime = Time.realtimeSinceStartup;
            }
        }

    }
    private IEnumerator SomeCoroutine()
    {
        yield return new WaitForSeconds(2);
        start = true;
    }
    public IEnumerator Attack1() { 
        Vector3 pers = new Vector3(0, 0, 0);
        
        for (int j = 0; j < 6; j++)
        {
            for (int i = -15; i < 15; i += 5)
            {
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Slime"))
                {
                    pers = go.transform.position;
                }
                Instantiate(Bullet, transform.position, Quaternion.identity).nap = new Vector3(pers.x + i, pers.y + i, pers.z);
               
            }
            yield return new WaitForSeconds(0.5f);
        }

        lastTime = Time.realtimeSinceStartup;

    }
   
    public IEnumerator Attack2()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Slime"))
        {
            for (int i = 0; i < 2; i++)
            {
                rb.AddForce(new Vector2((float)Random.Range(-30, 30), (float)Random.Range(-30, 30)), ForceMode2D.Impulse);
                yield return new WaitForSeconds(1f);
            }
            rb.velocity = Vector3.zero;
            lastVelocity = Vector3.zero;

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 2f) * 0.6f;
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("BulletPers"))
        {

            BulletPers Bullet = col.GetComponent<BulletPers>();
            hp -= Bullet.Damage;
            Destroy(col.gameObject);

            if (hp <= 0)
            {
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Slime"))
                {
                    go.GetComponent<personage>().counter += 10;
                    Debug.Log(go.GetComponent<personage>().counter);
                }
                Destroy(this.gameObject);
                room.k--;
            }

        }

    }

}
