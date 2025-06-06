
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Transform Player;
    public float remainderTime;
    public Transform MainCanvas;
    public Transform[] Particles;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
   
}
/*
 
 1- Slash
 2- Lvl UP
 
 
 
 
 
 */