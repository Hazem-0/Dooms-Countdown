using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlRestartExit : MonoBehaviour
{
    
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
    public void exit()
    {
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1f;
    }
}
