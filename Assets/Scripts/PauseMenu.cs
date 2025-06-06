using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject PauseMenu;
    public void pause()
    {
        if(PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(false);
           Time.timeScale = 1f;
        }
        else
        {
            PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        }

    }
    public void resume()
    {
        PauseMenu.SetActive(false);
       Time.timeScale = 1f;
    }
    public void exit()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
}
