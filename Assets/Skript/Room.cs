using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Room : MonoBehaviour
{
    public GameObject DoorU;
    public GameObject DoorR;
    public GameObject DoorD;
    public GameObject DoorL;    

    public GameObject Monster;
    public GameObject Boss;
    public GameObject Buf;
    public GameObject Hp;
    public GameObject Point;

    public personage personage;
    public int kolEnemy;

    private void SpawnBoss()
    {
        kolEnemy = 1;
        Boss.SetActive(true);
        Boss.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        Boss.GetComponent<BossEnemy>().room = this;
    }
    private void SpawnEnemy()
    {
        kolEnemy = Random.Range(2, 6);
        for (int i = 0; i < kolEnemy; i++)
        {
            Instantiate(Monster, new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5), -1), Quaternion.identity).GetComponent<Enemy>().room = this;
        }
    }

    private void LockedDoor()
    {
        if (DoorD != null)DoorD.GetComponent<Collider2D>().enabled = false;
        if (DoorU != null) DoorU.GetComponent<Collider2D>().enabled = false;
        if (DoorL != null) DoorL.GetComponent<Collider2D>().enabled = false;
        if (DoorR != null) DoorR.GetComponent<Collider2D>().enabled = false;
    }
    private void SpawnPrize()
    {
        if(Random.Range(0, 2)==0)
            Instantiate(Buf, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
        else
            Instantiate(Hp, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            Instantiate(Point, new Vector3(transform.position.x+Random.Range(-20,20), transform.position.y + Random.Range(-10, 10), -1), Quaternion.identity);
        }
    }
    public void Die(int point)
    {
        personage.counter += point;
        personage.DyeEnemy();
        kolEnemy--;
        if (kolEnemy == 0)
        {
            tag = "Clear";
            if (DoorD != null) DoorD.GetComponent<Collider2D>().enabled =true;
            if (DoorU != null) DoorU.GetComponent<Collider2D>().enabled = true;
            if (DoorL != null) DoorL.GetComponent<Collider2D>().enabled = true;
            if (DoorR != null) DoorR.GetComponent<Collider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Slime"))
        {
            if (CompareTag("Boss"))
            {
                SpawnBoss();
                LockedDoor();
            }
            if (CompareTag("Monster"))
            {
                SpawnEnemy();
                LockedDoor();
            }
            if(CompareTag("Prize"))
                SpawnPrize();
            GetComponent<Collider2D>().enabled = false;
        }

    }
}
