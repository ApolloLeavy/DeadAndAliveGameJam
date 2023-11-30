using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int currentLevel = 0;
    public GameObject pauseMenu;
    public GameObject startMenu;
    public GameObject player;
    public GameObject canvas;
    public GameObject look;
    public GameObject reticle;
    // Start is called before the first frame update
    void Start()
    {
        
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        startMenu = GameObject.Find("StartMenu");
        player = GameObject.Find("Player");
        player.SetActive(false);
        canvas = GameObject.Find("Canvas");
        look = GameObject.Find("Look");
        look.SetActive(false);
        reticle = GameObject.Find("Reticle");
        reticle.SetActive(false);
        
        
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(look);

    }
    public void StartGame()
    {
        startMenu.SetActive(false);
        Cursor.visible = false;
        player.SetActive(true);
        reticle.SetActive(true);
        look.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    
    public void loadLevel(int level)
    {

    }
}
