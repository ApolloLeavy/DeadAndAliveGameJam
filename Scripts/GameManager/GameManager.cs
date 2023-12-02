using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public int currentLevel = 0;
    public GameObject pauseMenu;
    public GameObject startMenu;
    public GameObject gameOver;
    public GameObject gameWin;
    public GameObject player;
    public GameObject canvas;
    public GameObject look;
    public GameObject reticle;
    public GameObject HUD;
    public GameObject healthAmount;
    public GameObject decoherenceAmount;
    public GameObject dualitycd;
    public GameObject tunnelcd;
    public GameObject superPositioncd;
    public GameObject alignmentcd;
    public GameObject entanglecd;
    public GameObject eventSystem;
    [SerializeField] Slider volume;
    public float dualityCD;
    public float tunnelCD;
    public float superPositionCD;
    public float alignmentCD;
    public float entangleCD;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        startMenu = GameObject.Find("StartMenu");
        gameOver = GameObject.Find("GameOver");

        gameWin = GameObject.Find("Victory");
        gameWin.SetActive(false);
        gameOver.SetActive(false);
        player = GameObject.Find("Player");
        canvas = GameObject.Find("Canvas");
        look = GameObject.Find("Look");
        reticle = GameObject.Find("Reticle");
        reticle.SetActive(false);
        HUD = GameObject.Find("HUD");
        eventSystem = GameObject.Find("EventSystem");

        HUD.SetActive(false);


        dualityCD = player.GetComponent<Player>().dcd;
        tunnelCD = player.GetComponent<Player>().qtcd;
        superPositionCD = player.GetComponent<Player>().spcd;
        alignmentCD = player.GetComponent<Player>().aacd;
        entangleCD = player.GetComponent<Player>().qecd;
        

        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(look);
        DontDestroyOnLoad(eventSystem);
        DontDestroyOnLoad(this.gameObject);

        StartCoroutine(Cooldowns());
    }
    public void StartGame()
    {
        Time.timeScale = 1;

        startMenu.SetActive(false);
        Cursor.visible = false;
        reticle.SetActive(true);
        HUD.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Volume()
    {
        AudioListener.volume = volume.value;
    }
    public void NextLevel()
    {
        //SceneManager.UnloadSceneAsync(sceneName: "Level" + (currentLevel));
        currentLevel++;
        if (currentLevel == 3)
        {
            gameWin.SetActive(true);
            Time.timeScale = 0;
            
        }
        else
        {
            SceneManager.LoadScene(currentLevel);
            player.transform.position = new Vector3(0, 0, 0);
        }
        
        
    }
    public void Lose()
    {
        gameOver.SetActive(true);
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        reticle.SetActive(false);

        Time.timeScale = 0;
        return;
    }
    public void Quit()
    {
        Application.Quit();
    }
    

    public IEnumerator Cooldowns()
    {
        healthAmount.GetComponent<Text>().text = player.GetComponent<Player>().hp.ToString();
        decoherenceAmount.GetComponent<Text>().text = player.GetComponent<Player>().decoherence.ToString();
        if (player.GetComponent<Player>().canDuality)
        {
            dualitycd.GetComponent<Text>().text = "RMB";
            dualityCD = player.GetComponent<Player>().dcd;
        }
        else
        {
            dualitycd.GetComponent<Text>().text = dualityCD.ToString();
            dualityCD--;
        }
        if (player.GetComponent<Player>().canTunnel)
        {
            tunnelcd.GetComponent<Text>().text = "LShft";
            tunnelCD = player.GetComponent<Player>().qtcd;
        }
        else
        {
            tunnelcd.GetComponent<Text>().text = tunnelCD.ToString();
            tunnelCD--;
        }
        if (player.GetComponent<Player>().canSuper)
        {
            superPositioncd.GetComponent<Text>().text = "LCtrl";
            superPositionCD = player.GetComponent<Player>().spcd;
        }
        else
        {
            superPositioncd.GetComponent<Text>().text = superPositionCD.ToString();
            superPositionCD--;
        }
        if (player.GetComponent<Player>().canAlignment)
        {
            alignmentcd.GetComponent<Text>().text = "E";
            alignmentCD = player.GetComponent<Player>().aacd;
        }
        else
        {
            alignmentcd.GetComponent<Text>().text = alignmentCD.ToString();
            alignmentCD--;
        }
        if (player.GetComponent<Player>().canEntangle)
        {
            entanglecd.GetComponent<Text>().text = "R";
            entangleCD = player.GetComponent<Player>().qecd;
        }
        else
        {
            entanglecd.GetComponent<Text>().text = entangleCD.ToString();
            entangleCD--;
        }
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(Cooldowns());
    }
    
}
