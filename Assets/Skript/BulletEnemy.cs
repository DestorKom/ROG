using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    float speed = 15f;
    public Rigidbody2D rb;
    public Vector3 nap;
    public int Damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 diference = nap - transform.position;
        float rotateZ = Mathf.Atan2(-diference.x, diference.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        rb.velocity = transform.up * speed;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.CompareTag("BulletPers"))
        {
            Destroy(this.gameObject);
        }
    }
}
