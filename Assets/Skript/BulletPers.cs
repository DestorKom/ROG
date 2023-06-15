using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BulletPers : MonoBehaviour
{
    public Animator Animation;
    float speed = 24f;
    public Rigidbody2D rb;
    public Vector3 nap;
    public int Damage;
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        Vector3 diference = nap - transform.position;
        float rotateZ = Mathf.Atan2(-diference.x,diference.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        rb.velocity = transform.up * speed;
    }
        void OnTriggerEnter2D(Collider2D col)
        {
        if (col.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.CompareTag("BulletEnemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
