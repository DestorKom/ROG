using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class buttonexit : MonoBehaviour
{
    public GameObject canvas;
    public void Load()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Play()
    {
        Time.timeScale = 1.0f;
        canvas.SetActive(false);

    }
    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void Table()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }

}
