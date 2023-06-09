using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public bool vector;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (vector)
            transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, -1);
        else
            transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, -1);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
