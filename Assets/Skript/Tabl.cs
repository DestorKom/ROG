using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Tabl : MonoBehaviour
{
    GameObject[,] childM = new GameObject[4, 8];
    void Start()
    {



        for (int i = 0; i < 8; i++)
         {
             childM[0, i] = transform.GetChild(i).GetChild(0).gameObject;
             childM[1, i] = transform.GetChild(i).GetChild(1).gameObject;
             childM[2, i] = transform.GetChild(i).GetChild(2).gameObject;
             childM[3, i] = transform.GetChild(i).GetChild(3).gameObject;

         }
        if (PlayerPrefs.GetString("Name" + 0) != "")
        {
            Load();
        }
        if (StaticTabl.point != 0 && StaticTabl.time != 0 && StaticTabl.nam != "")
        {
            for (int i = 0; i < 8; i++)
            {
                if (StaticTabl.point > Convert.ToInt32(childM[2, i].GetComponent<Text>().text)) {
                    string name = childM[1, i].GetComponent<Text>().text;
                    string point = childM[2, i].GetComponent<Text>().text;
                    string time = childM[3, i].GetComponent<Text>().text;
                    childM[1, i].GetComponent<Text>().text = StaticTabl.nam;
                    childM[2, i].GetComponent<Text>().text = StaticTabl.point.ToString();
                    childM[3, i].GetComponent<Text>().text = StaticTabl.time.ToString();
                    StaticTabl.nam = name;
                    StaticTabl.point = Convert.ToInt32(point);
                    StaticTabl.time = (float)Convert.ToDouble(time);
                }
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                childM[0, i] = transform.GetChild(i).GetChild(0).gameObject;
                childM[1, i] = transform.GetChild(i).GetChild(1).gameObject;
                childM[2, i] = transform.GetChild(i).GetChild(2).gameObject;
                childM[3, i] = transform.GetChild(i).GetChild(3).gameObject;
            }
        }
        Save();
    }
    private void Save()
    {

        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetString("Name"+i, childM[1, i].GetComponent<Text>().text);
            PlayerPrefs.SetString("Point" + i, childM[2, i].GetComponent<Text>().text);
            PlayerPrefs.SetString("Timer" + i, childM[3, i].GetComponent<Text>().text);
        }
        PlayerPrefs.Save();
    }
    private void Load()
    {

        for (int i = 0; i < 8; i++)
        {
            //Debug.Log(PlayerPrefs.GetString("Point" + i));
            childM[1, i].GetComponent<Text>().text = PlayerPrefs.GetString("Name" + i);
            childM[2, i].GetComponent<Text>().text = PlayerPrefs.GetString("Point" + i);
            childM[3, i].GetComponent<Text>().text = PlayerPrefs.GetString("Timer" + i);
        }
    }
    private void OnDestroy()
    {
        Save();
    }

}
