using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    int bombsLeft = 3;                      // Number of bombs left.

    public GUIStyle guiDetonate;            // GUIStyle for the Detonate-button.
    public GUIStyle guiScore;               // guistyle for the score
    public GUIStyle guiButton;
    public GUIStyle guiBox;

    public Texture texSimpleBomb;           // Texture used for displaying # of bombs left on GUI.
    public Texture texTimeBomb;             //
    public Texture texImpBomb; 

    public GameObject pfSimpleBomb;         // Finding the simple bomb-object in the game.
    public GameObject pfTimeBomb;           // Finding the time bomb-object in the game.
    public GameObject pfImpBomb;            // Prefab of the Implosion Bomb;

    public GameObject bombPos;              // finding the position of the bomb.

    float CAMERA_DISTANCE;                  // Distance from camera to scene.

    public static int score = 0;            // your score in the game.
    int finalScore = 0;                     // Hinders the score of increasing after the game ends.

    float winTime = 0;                      // The actual time you'll end. This depends on the var PLAY_TIME.
    const int PLAY_TIME = 5;                // The time the game tracks the score. Begins when you placed all the bombs and hit explode.
    public static bool won = false;         // If there time left, it's false, else it's true.

    bool pauseGame = false;

    public string[] bombType;

    #region GUI positions
    // Score board position
    int scoreBoardPosX = Screen.width / 4;
    int scoreBoardPosY = Screen.height / 5;
    int scoreBoardWidth = Screen.width / 4 * 2;
    int scoreBoardHeight = Screen.height / 5 * 3;
    Rect rectScoreBoard;

    // Score Board restart button;
    int scoreBoardRestartPosX = Screen.width / 4;
    int scoreBoardRestartPosY = Screen.height / 5 * 3;
    int scoreBoardRestartWidth = Screen.width / 4 * 2;
    int scoreBoardRestartHeight = Screen.height / 5;
    Rect rectScoreBoardRestartButton;

    // Score Board Next Level Button
    int scoreBoardNextLevelPosX = Screen.width / 4;
    int scoreBoardNextLevelPosY = Screen.height / 5 * 2;
    int scoreBoardNextLevelWidth = Screen.width / 4 * 2;
    int scoreBoardNextLevelHeight = Screen.height / 5;
    Rect rectscoreBoardNextLevel;

    // bombs left box ** No Rect-var here, as the values are needed individually.
    int bombsLeftPosX = 0;
    int bombsLeftPosY = 0;
    int bombsLeftWidth = 32;
    int bombsLeftHeight = 32;

    // Score box
    int scoreBoxPosX = 0;
    int scoreBoxPosY = 32;
    int scoreBoxWidth = 128;
    int scoreBoxHeight = 32;
    Rect rectScoreBox;

    // Explode Button
    int explodeButtonPosX = 0;
    int explodeButtonPosY = 64;
    int explodeButtonWidth = 100;
    int explodeButtonHeight = 100;
    Rect rectExplodeButton;

    // in-game pausebutton
    int pauseButtonPosX = Screen.width - 64;
    int pauseButtonPosY = 16;
    int pauseButtonWidth = 48;
    int pauseButtonHeight = 48;
    Rect rectPauseButton;

    // Pause button box
    int pauseButtonBoxPosX = Screen.width / 4;
    int pauseButtonBoxPosY = Screen.height / 5;
    int pauseButtonBoxWidth = Screen.width / 4 * 2;
    int pauseButtonBoxHeight = Screen.height / 5 * 3;
    Rect rectPauseButtonBox;

    // Pause button - Resume game button
    int resumeGamePosX = Screen.width / 4;
    int resumeGamePosY = Screen.height / 5;
    int resumeGameWidth = Screen.width / 4 * 2;
    int resumeGameHeight = Screen.height / 5;
    Rect rectResumeGame;

    #endregion


    void Start()
    {
        // Rectangle-positions for the GUI interface
        rectScoreBox = new Rect(scoreBoxPosX, scoreBoxPosY, scoreBoxWidth, scoreBoxHeight);
        rectExplodeButton = new Rect(explodeButtonPosX,explodeButtonPosY,explodeButtonWidth,explodeButtonHeight);
        rectScoreBoard = new Rect(scoreBoardPosX, scoreBoardPosY, scoreBoardWidth, scoreBoardHeight);
        rectScoreBoardRestartButton = new Rect(scoreBoardRestartPosX, scoreBoardRestartPosY, scoreBoardRestartWidth, scoreBoardRestartHeight);
        rectscoreBoardNextLevel = new Rect(scoreBoardNextLevelPosX, scoreBoardNextLevelPosY, scoreBoardNextLevelWidth, scoreBoardNextLevelHeight);
        rectPauseButton = new Rect(pauseButtonPosX, pauseButtonPosY, pauseButtonWidth, pauseButtonHeight);
        rectPauseButtonBox = new Rect(pauseButtonBoxPosX, pauseButtonBoxPosY, pauseButtonBoxWidth, pauseButtonBoxHeight);
        rectResumeGame = new Rect(resumeGamePosX, resumeGamePosY, resumeGameWidth, resumeGameHeight);

        // Sets the z-position of the camera when clicking/touching
        CAMERA_DISTANCE = bombPos.transform.position.z;

        won = false;
        
        bombType = new string[bombsLeft];
        bombType[0] = PlayerPrefs.GetString("BombType0");
        bombType[1] = PlayerPrefs.GetString("BombType1");
        bombType[2] = PlayerPrefs.GetString("BombType2");
    }

    void Update()
    {

        WorldClick();
        Win();
    }

    void OnGUI()
    {
        for (int i = 0; i < bombsLeft; i++)
        {
            switch (bombType[i])
            {
                case "SimpleBomb":
                    GUI.Box(new Rect(
                        bombsLeftPosX + (32 * i),
                        bombsLeftPosY,
                        bombsLeftWidth,
                        bombsLeftHeight),
                        texSimpleBomb, GUIStyle.none);
                    break;
                case "TimeBomb":
                    GUI.Box(new Rect(
                        bombsLeftPosX + (32 * i),
                        bombsLeftPosY,
                        bombsLeftWidth,
                        bombsLeftHeight),
                        texTimeBomb, GUIStyle.none);
                    break;
                case "ImpBomb":
                    GUI.Box(new Rect(
                        bombsLeftPosX + (32 * i),
                        bombsLeftPosY,
                        bombsLeftWidth,
                        bombsLeftHeight),
                        texImpBomb, GUIStyle.none);
                    break;
            }
    }

        GUI.Box(rectScoreBox, "Score: " + finalScore,guiScore);
        if (GUI.Button(rectExplodeButton, "", guiDetonate))
        {
            if (bombsLeft == 0 && won == false)
            {   
                GameObject[] objs = GameObject.FindGameObjectsWithTag("Bomb");
                for(int i = 0; i<objs.Length; i++)
                {
                    switch (objs[i].name)
                    {
                        case "SimpleBomb(Clone)":
                            objs[i].GetComponent<Bomb_Simple>().exploded = true;
                            break;
                        case "TimeBomb(Clone)":
                            objs[i].GetComponent<Bomb_Time>().exploded = true;
                            break;
                        case "ImpBomb(Clone)":
                            objs[i].GetComponent<Bomb_Implosion>().exploded = true;
                            break;
                    }
                    objs[i].animation.Play();
                }
                winTime = Time.time+PLAY_TIME;
                won = true;
            }
        }

        if (Time.time >= winTime && winTime > 0)
        {
            GUI.Box(rectScoreBoard,
                "\nThe session is over!\nYour final score was\n"+finalScore, guiBox);

            if (GUI.Button(rectscoreBoardNextLevel, "Next Level",guiButton))
            {
                Application.LoadLevel(Application.loadedLevel + 1 );
            }

            if (GUI.Button(rectScoreBoardRestartButton, "Restart the game", guiButton))
            {
                Restart();
                Application.LoadLevel(0);
            }
        }
        if (GUI.Button(rectPauseButton, "P"))
            {
                pauseGame = true;
            }

        if (pauseGame)
        {
            Pause();
        }
    }

    #region Methods
    void Win()
    {
        if (Time.time >= winTime && winTime > 0)
        {
            score = finalScore;
        }else{
            finalScore = score;
        }
    }

    void WorldClick()
    {
        // touch : Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended
        // mouse : Input.GetMouseButtonDown(0)

        if (Input.GetMouseButtonDown(0))
        {
            // finds the cursor's position and store it in worldPos.
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = CAMERA_DISTANCE;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            worldPos = new Vector3(worldPos.x, worldPos.y, bombPos.transform.position.z);

            //Stores the position of where the lightning should strike in InstancePos.
            Vector3 instancePos = new Vector3(worldPos.x, worldPos.y, bombPos.transform.position.z);

            if (bombsLeft > 0)
            {
                GameObject bomb;
                bomb = GameObject.FindGameObjectWithTag("Bomb");
                switch (bombType[bombsLeft - 1])
                {
                    case "SimpleBomb":
                        bomb = (GameObject)GameObject.Instantiate(pfSimpleBomb, instancePos, Quaternion.identity);
                        bombsLeft--;
                        break;
                    case "TimeBomb":
                        bomb = (GameObject)GameObject.Instantiate(pfTimeBomb, instancePos, Quaternion.identity);
                        bombsLeft--;
                        break;
                    case "ImpBomb":
                        bomb = (GameObject)GameObject.Instantiate(pfImpBomb, instancePos, Quaternion.identity);
                        bombsLeft--;
                        break;
                }
                int scale = 100;
                bomb.transform.localScale = new Vector3(scale, scale, scale);
                bomb.transform.eulerAngles = new Vector3(0, 180, 0);
                bomb.animation.wrapMode = WrapMode.Once;
            }
        }
    }

    void Restart()
    {
        won = false;
        finalScore = 0;
        score = 0;
    }

    void Pause()
    {
        GUI.Box(rectPauseButtonBox,"");

        if (GUI.Button(rectResumeGame, "Resume"))
        {
            pauseGame = false;
        }
    }
    #endregion
}