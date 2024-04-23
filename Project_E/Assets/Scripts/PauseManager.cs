using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            { 
                Pause();
            }
        }
    }
 
    void Pause()
    {  
        pauseMenuUI.SetActive(true);
        //This can be used for slow motion effects or to speed up your application.
        //When timeScale is 1.0, time passes as fast as real time.
        //When timeScale is 0.5 time passes 2x slower than realtime.
        Time.timeScale = 0f;
        //通过此函数修改时间传递的快慢，设置为0使得时间暂停
        GameIsPaused = true;
    }
 
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
 
    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }
    
}