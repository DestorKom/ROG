using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class texttimer : MonoBehaviour
{
    public GameObject triangle;
    public GameObject personage;
    public TextMeshProUGUI textPoint;
    public TextMeshProUGUI textTime;
    public TMP_InputField textname;
    public float time;
    public int counter;
    void Start()
    {
        
        counter = personage.GetComponent<personage>().counter;
        textPoint.text ="Очки "+counter;

        time = triangle.GetComponent<RoomPlacer>().timer;
        textTime.text = "Время " + Mathf.Round(time) +" с.";

       
    }
    private void OnDestroy()
    {
        StaticTabl.time = time;
        StaticTabl.point = counter;
        StaticTabl.nam = textname.text;
    }
}
