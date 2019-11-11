using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using CCC.DesignPattern;

public delegate void SimpleEvent();

public class Game : PublicSingleton<Game>
{
    public const string mapSceneName = "Map";
    public const string UISceneName = "UI";


    private bool mapLoaded = false;
    private bool uiLoaded = false;

    public Canvas canvas = null;

    static private event SimpleEvent onGameReady;
    static private event SimpleEvent onGameStart;

    // GAME STATE

    public bool gameStarted = false;
    public bool gameReady = false;
    public bool gameOver = false;
    private bool readyToRestart = false;
    bool playerSpawned = false;

    public Map map;
    public Health healthBar;

    public Locker gameRunning = new Locker();


    private void Start()
    {
        Time.timeScale = 0;
        Random.InitState((int)System.DateTime.Now.Ticks);

        if (!Scenes.IsActiveOrBeingLoaded(mapSceneName))
            Scenes.LoadAsync(mapSceneName, LoadSceneMode.Additive, OnMapLoaded);
        if (!Scenes.IsActiveOrBeingLoaded(UISceneName))
            Scenes.LoadAsync(UISceneName, LoadSceneMode.Additive, OnUILoaded);
    }

    private void Update()
    {
        if(readyToRestart && Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    void OnMapLoaded(Scene scene)
    {
        Game.Instance.map = Map.instance;//scene.FindRootObject<Map>();

        mapLoaded = true;
        CheckInitGame();
    }


    void OnUILoaded(Scene scene)
    {
        Game.Instance.healthBar = scene.FindRootObject<Health>();
        Game.Instance.canvas = scene.FindRootObject<Canvas>();

        uiLoaded = true;
        CheckInitGame();
    }

    void CheckInitGame()
    {
        if (uiLoaded && mapLoaded)
        {
            Game.Instance.InitGame();
        }
    }


    static public event SimpleEvent OnGameReady
    {
        add
        {
            if (instance != null && instance.gameReady)
                value();
            else
                onGameReady += value;
        }
        remove
        {
            onGameReady -= value;
        }
    }


    static public event SimpleEvent OnGameStart
    {
        add
        {
            if (instance != null && instance.gameStarted)
                value();
            else
                onGameStart += value;
        }
        remove
        {
            onGameStart -= value;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onGameReady = null;
        onGameStart = null;
    }

    public void InitGame()
    {
        Time.timeScale = 1;
        gameRunning.onLockStateChange += GameRunning_onLockStateChange;

        map.SetEventManager(FindObjectOfType<EventManager>().GetComponent<EventManager>());
        NotificationManager.Instance.QueueNotificationMessage("Practice Mode\nCannot Lose Yet");
        Instance.healthBar.SetMap(map);
       // map.OnStateUpdate.AddListener(verifyGameover);

        
        //Spawn player
        // submarine = playerSpawn.SpawnPlayer();
        // submarine.movementEnable = false;

        //Ready up !
        ReadyGame();
    }

    private void GameRunning_onLockStateChange(bool state)
    {
        if (state)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    void ReadyGame()
    {
        if (gameReady)
            return;

        gameReady = true;

        LoadingScreen.OnNewSetupComplete();

        if (onGameReady != null)
        {
            onGameReady();
            onGameReady = null;
        }
    }

    void StartGame()
    {
        if (gameStarted || !playerSpawned)
            return;

        gameStarted = true;

        // Init Game Start Events
        if (onGameStart != null)
        {
            onGameStart();
            onGameStart = null;
        }
    }

    public void EndGame()
    {
        // End Game Screen
        if (gameOver)
            return;
        gameOver = true;

        FindObjectOfType<PlayerController>().GetComponent<PlayerController>().ChangeState(PlayerDeadState.GetInstance());
        GameObject endGameUI = GameObject.FindGameObjectWithTag("UI EndGame");
        TextMeshProUGUI textPro = endGameUI.GetComponentInChildren<TextMeshProUGUI>();
        textPro.SetText("Score: " + (int)Map.instance.depth + " meters\nPress Start to Try Again");
        Image black = endGameUI.GetComponentInChildren<Image>();

        black.DOFade(1, 2);
        textPro.DOFade(1, 2).OnComplete(() => { readyToRestart = true; });

        //Application.Quit();
        /*
        ui.feedbacks.ShowTimeUp(delegate ()
        {
            LoadingScreen.TransitionTo(FishingSummary.SCENENAME, new ToFishingSummaryMessage(fishingReport));
        });*/
    }

    public void verifyGameover()
    {
        if (map.GetHealth() < 0)
            EndGame();
    }

}
