using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class BossEnemy : MonoBehaviour
{
    public Animator Animation;
    public BulletEnemy Bullet;
    public Rigidbody2D rb;
    public Room room;
    public GameObject canvas;
    int hp = 400;
    void Start()
    {
        NewAttack();
    }


    private IEnumerator SomeCoroutine()
    {
        yield return new WaitForSeconds(2);
        Animation.Play("idle");
    }
    public void Attack1()
    {
        Vector3 pers = new Vector3(0, 0, 0);

        for (int i = -24; i < 24; i += 8)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Slime"))
            {
                pers = go.transform.position;
            }
            Instantiate(Bullet, transform.position, Quaternion.identity).nap = new Vector3(pers.x + i, pers.y + i, pers.z);

        }
    }
    public void Attack2()
    {
        Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(dir * 30f, ForceMode2D.Impulse);

    }
    public void NewAttack()
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(SomeCoroutine());
        if (Random.Range(0, 2) == 0)
            Animation.Play("Rush");
        else
            Animation.Play("Fire");

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.AddForce(dir * 30f, ForceMode2D.Impulse);
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
                room.Die(200);
                Destroy(this.gameObject);
                Time.timeScale = 0f;
                canvas.SetActive(true);


            }

        }

    }

}
