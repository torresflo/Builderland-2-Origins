using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public float levelStartDelay = 1.5f;
    public float fadeDelay = 0.3f;
    public GameBoard board;
    public AudioClip playerDeath;
    public AudioClip playerWin;

    protected GameObject levelImage;
    protected GameObject endImage;
    protected Text levelText;
    protected GameObject myLevels;
    protected Level[] levels;
    protected int currentLevel = 0;
    protected GameObject player;
    protected GameObject cursor;

    // Use this for initialization
    void Awake () {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("SINGLETON ERROR");
            Destroy(gameObject);
            return;
        }

        
        DontDestroyOnLoad(gameObject);

        Screen.SetResolution(512, 512, false);

        InitGameManager();
    }

    protected void InitGameManager()
    {
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        player = GameObject.FindGameObjectWithTag("Player");

        GameBoard.LoadSprites();
        myLevels = GameObject.Find("GameLevels");

        levels = myLevels.GetComponentsInChildren<Level>();

        levelImage = GameObject.Find("LevelImage");
        endImage = GameObject.Find("EndImage");
        endImage.SetActive(false);
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        InitLevel();
    }

    public void PlayerDeath()
    {
        SoundManager.instance.PlayAudio(playerDeath);
        cursor.GetComponent<CursorScript>().Reset();

        InitLevel();
    }

    public void PlayerWin()
    {
        SoundManager.instance.PlayAudio(playerWin);
        cursor.GetComponent<Transform>().position = new Vector3(0.5f, 0.5f, 0f);

        currentLevel++;

        if (currentLevel >= levels.Length)
        {
            // Game completed
            levelImage.SetActive(false);
            endImage.SetActive(true);
            return;
        }

        InitLevel();
    }

    protected void StartLevel()
    {
        if (currentLevel >= levels.Length)  // Game completed
        {
            Debug.Log("Cannot load level " + currentLevel);
        }

        board.InitBoard(levels[currentLevel]);

        HideLevelImage();

        player.GetComponent<PlayerScript>().Reset();
        player.SetActive(true);
    }

    protected void InitLevel()
    {
        ShowLevelImage();
        Invoke("StartLevel", levelStartDelay);
    }

    protected void HideLevelImage()
    {
        levelImage.GetComponent<CanvasRenderer>().SetAlpha(levelImage.GetComponent<CanvasRenderer>().GetAlpha() - 0.34f);

        if (levelImage.GetComponent<CanvasRenderer>().GetAlpha() < 0.1f)
        {
            levelImage.GetComponent<CanvasRenderer>().SetAlpha(1f);
            levelImage.SetActive(false);
        }
        else
            Invoke("HideLevelImage", fadeDelay);
    }

    protected void ShowLevelImage()
    {
        levelText.text = "STAGE " + (currentLevel + 1);
        levelImage.SetActive(true);
    }
}
