using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    string mode = "main"; //How-to, credits.
    int bomChanging;

    public Texture soundOn;
    public Texture soundOff;

    public GUIStyle guiButton;
    public GUIStyle guiBox;
    public GUIStyle guiSmallButton;
    public GUIStyle guiChangeBomb;
    public GUIStyle guiSettings;

    public Font font;

    string newGame = "NEW GAME";
    string howTo = "HOW TO";
    string customize = "CUSTOMIZE";
    string credits = "CREDITS";
    string quit = "QUIT";
    string back = "BACK";

    #region GUI Positions

    // The Y-coordinates and heights based on the screen being divided by 5.
    // Each button is 1/5 of the screen, and is placed accordingly.
    // the '-50' is used to modify the placement so it looks more aligned.

    // New game button
    int newGamePosX = Screen.width / 4;
    int newGamePosY = Screen.height / 5 - 50;
    int newGameWidth = Screen.width / 4 * 2;
    int newGameHeight = Screen.height / 5;
    Rect rectNewGame;

    //How-to button ** Coordinates are also used for the customize-button **
    int howToPosX = Screen.width / 4;
    int howToPosY = Screen.height / 5 * 2 - 50;
    int howToWidth = Screen.width / 4 * 2;
    int howToHeight = Screen.height / 5;
    Rect rectHowTo;

    //Credits button ** Coordinates are also used for the Back-Button **
    int creditsPosX = Screen.width / 4;
    int creditsPosY = Screen.height / 5 * 3 - 50;
    int creditsWidth = Screen.width / 4 * 2;
    int creditsHeight = Screen.height / 5;
    Rect rectCredits;

    //Quit
    int quitPosX = Screen.width / 4;
    int quitPosY = Screen.height / 5 * 4 - 50;
    int quitWidth = Screen.width / 4 * 2;
    int quitHeight = Screen.height / 5;
    Rect rectQuit;

    // Settings
    float settingsPosX = 10;                       // 10 for a little spacing;
    float settingsPosY = Screen.height - 100 - 10; // minus 100 for the image. Minus 10 for a little spacing 
    float settingsWidth = Screen.width / 8;        // Results in 100px at a width-800.
    float settingsHeight = Screen.height / 4.8f;   // Results in 100px at a height-480.
    Rect rectSettings;

    //Text Box - used for HowTo, Credits and Customize
    int textBoxPosX = Screen.width / 4;
    int textBoxPosY = Screen.height / 5 - 50;
    int textBoxWidth = Screen.width / 4 * 2;
    int textBoxHeight = Screen.height / 5 * 3;
    Rect rectTextBox;

    #region Customize bombs
    //Customize bomb buttons
    float customBombPosX1 = Screen.width / 4;
    float customBombWidth1 = Screen.width / 4 * 2 / 3;
    
    float customBombWidth2 = Screen.width / 4 * 2 / 3;

    float customBombWidth3 = Screen.width / 4 * 2 / 3;

    float customBombHeight = Screen.height / 5;
    float customBombPosY = Screen.height / 5 * 2 - 50;

    Rect[] rectCustomBomb = new Rect[3];

    #endregion


    #endregion

    void Start()
    {
        rectNewGame = new Rect(newGamePosX, newGamePosY, newGameWidth, newGameHeight);
        rectHowTo = new Rect(howToPosX, howToPosY, howToWidth, howToHeight);
        rectCredits = new Rect(creditsPosX, creditsPosY, creditsWidth, creditsHeight);
        rectQuit = new Rect(quitPosX, quitPosY, quitWidth, quitHeight);
        rectTextBox = new Rect(textBoxPosX, textBoxPosY, textBoxWidth, textBoxHeight);
        rectSettings = new Rect(settingsPosX, settingsPosY, settingsWidth, settingsHeight);

        rectCustomBomb[0] = new Rect(customBombPosX1, customBombPosY, customBombWidth1, customBombHeight);
        rectCustomBomb[1] = new Rect(customBombPosX1 + customBombWidth1, customBombPosY, customBombWidth2, customBombHeight);
        rectCustomBomb[2] = new Rect((customBombPosX1 + customBombWidth1)+customBombWidth2, customBombPosY, customBombWidth3, customBombHeight);

        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetString("sound", "on");
        }

        //Bombs
        if ( !PlayerPrefs.HasKey("BombType0") && !PlayerPrefs.HasKey("BombType1") && !PlayerPrefs.HasKey("BombType2"))
        {
            PlayerPrefs.SetString("BombType0", "SimpleBomb");
            PlayerPrefs.SetString("BombType1", "SimpleBomb");
            PlayerPrefs.SetString("BombType2", "SimpleBomb");
        }

        //settings for GUIStyles
        guiBox.font = font;
        guiButton.font = font;
        guiChangeBomb.font = font;
        guiSettings.font = font;
        guiSmallButton.font = font;

    }

    void OnGUI()
    {
        switch (mode)
        {
            case "main":
                MainMenu();
                break;
            case "howto":
                HowTo();
                break;
            case "credits":
                Credits();
                break;
            case "customize":
                Customize();
                break;
            case "changebomb":
                ChangeBomb(bomChanging);
                break;
            case "settings":
                Settings();
                break;
        }
    }

    void MainMenu()
    {
        if (GUI.Button(rectNewGame, newGame, guiButton))
        {
            Application.LoadLevel(1);
        }


        if (GUI.Button(rectHowTo, howTo,guiButton))
        {
            mode = "howto";
        }

        if (GUI.Button(rectCredits, credits, guiButton))
        {
            mode = "credits";
        }


        if (GUI.Button(rectQuit, quit, guiButton))
        {
            Application.Quit();
        }

        if(GUI.Button(rectSettings, "", guiSettings))
        {
            mode = "settings";
        }
    }

    void HowTo()
    {
        //GUI.Box(rectTextBox, "HOW TO PLAY!\nEach level you get 3 bombs.\nPlace them where you want.\nHit the Explode-button to begin when all bombs are placed.");
        GUITextBox("\nHOW TO PLAY!\nEach level you get 3 bombs.\nPlace them where you want.\nHit the Detonate-button to begin,\n when all bombs are placed.\n\nPress the customize-button beow to change,\n what bombs you are placing.");
        if(GUI.Button(rectCredits, customize, guiButton))
        {
            mode = "customize";
        }
        if (GUI.Button(rectQuit, back, guiButton))
        {
            mode = "main";
        }
    }

    void Customize()
    {
        string[] bombtype = new string[3];
        bombtype[0] = PlayerPrefs.GetString("BombType0");
        bombtype[1] = PlayerPrefs.GetString("BombType1");
        bombtype[2] = PlayerPrefs.GetString("BombType2");

        GUITextBox("\nCustomize your gameplay.\nSelect one of the bombs to change the loadout.");
        for (int i = 0; i < rectCustomBomb.Length; i++)
        {
            if (GUI.Button(rectCustomBomb[i], "Bomb " + i + "\n"+bombtype[i], guiSmallButton))
            {
                bomChanging = i;
                mode = "changebomb";
            }
        }
        if (GUI.Button(rectCredits, back, guiButton))
        {
            mode = "main";
        }
    }

    void Credits()
    {
        //GUI.Box(rectTextBox, "Credits!\nDeveloped by Mikkel Thygesen.\nGraphics made by Mikkel Thygesen\nThanks to the Unity team!");
        GUITextBox("\nCREDITS\nDeveloped by Mikkel Thygesen.\nGraphics made by Mikkel Thygesen\nThanks to the Unity team!\n- Copyright Mikkel Thygesen 2011 -");
        if (GUI.Button(rectCredits,  back, guiButton))
        {
            mode = "main";
        }
    }

    void ChangeBomb(int i)
    {
        //GUI.Box(rectTextBox, "Change the bomb for bomb "+i);
        GUITextBox("\nChange the bomb-loadout for bomb " + (i+1));

        if (GUI.Button(rectHowTo, "SIMPLE BOMB\nThe simple bomb explode without any fancy tricks.\nShort name: SimpleBomb.", guiChangeBomb))
        {
            string[] value = { "BombType", i.ToString() };
            PlayerPrefs.SetString(string.Join("", value, 0, 2), "SimpleBomb");
            mode = "customize";
        }
        if (GUI.Button(rectCredits, "TIME BOMB\nThis lazy waits a few moments before it explode.\nShort name: TimeBomb.", guiChangeBomb))
        {
            string[] value = { "BombType", i.ToString() };
            PlayerPrefs.SetString(string.Join("", value, 0, 2), "TimeBomb");
            mode = "customize";
        }
        if (GUI.Button(rectQuit, "IMPLOSION BOMB\nThis highly advanced bomb pulls objects instead of throwing them away.\nShort name: ImpBomb.", guiChangeBomb))
        {
            string[] value = { "BombType", i.ToString() };
            PlayerPrefs.SetString(string.Join("", value, 0, 2), "ImpBomb");
            mode = "customize";
        }
        if (GUI.Button(new Rect(rectQuit.xMin,rectQuit.yMax,rectQuit.width,rectQuit.height), "COLLISION BOM\nThis bomb explodes when an object hits it.\nShort name: CollBomb", guiChangeBomb))
        {
            string[] value = { "BombType", i.ToString() };
            PlayerPrefs.SetString(string.Join("", value, 0, 2), "CollBomb");
            mode = "customize";

        }
    }

    void GUITextBox(string text)
    {
        GUI.Box(rectTextBox, text, guiBox);
    }

    void Settings()
    {
        GUITextBox("\nSETTINGS\nClick the below icons to toggle sound, --");
        if (PlayerPrefs.GetString("sound") == "on")
        {
            if (GUI.Button(rectCustomBomb[0], soundOn, GUIStyle.none))
            {
                PlayerPrefs.SetString("sound", "off");
            }
        }
        else if (GUI.Button(rectCustomBomb[0], soundOff, GUIStyle.none))
        {
            PlayerPrefs.SetString("sound", "on");
        }

        if (GUI.Button(rectCredits, back, guiButton))
        {
            mode = "main";
        }
    }
}
