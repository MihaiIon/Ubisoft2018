using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    // Scripts
    ConnectPlayersState connectPlayersScript;
    SplashScreenState splashScreenScript;
    MainMenuState mainMenuScript;
    StoryState storyScript;
    InGameState inGameScript;

    // All the possible states
    public enum State
    {
        INITIALIZING,
        SPLASH_SCREENS,
        MAIN_MENU,
        STORY,
        GAME_FADE_IN,
        IN_GAME,
        GAME_PAUSE,
        GAME_OVER,
        GAME_WON
    }

    [SerializeField]
    public State currState;

    [SerializeField]
    public bool allowPlayerInputs;
    
    //----------------------------------------------------------------
    
    /// <summary>
    /// 
    /// </summary>
	void Awake () {
        LoadScripts();
        SetState(State.INITIALIZING);
        allowPlayerInputs = false;
	}

    /// <summary>
    /// Loads every script that the SceneManager needs in order 
    /// to function properly.
    /// </summary>
    void LoadScripts ()
    {
        connectPlayersScript = GetComponent<ConnectPlayersState>();
        splashScreenScript = GetComponent<SplashScreenState>();
        mainMenuScript = GetComponent<MainMenuState>();
        storyScript = GetComponent<StoryState>();
        inGameScript = GetComponent<InGameState>();
    }

    /// <summary>
    /// Set the current state of the GameManager
    /// </summary>
    /// <param name="s"></param>
	private void SetState (State s)
    {
        switch (s)
        {
            case State.INITIALIZING:
                connectPlayersScript.Init();
                break;
            case State.SPLASH_SCREENS:
                connectPlayersScript.Dismiss();
                splashScreenScript.Init();
                break;
            case State.MAIN_MENU:
                splashScreenScript.Dismiss();   // If the players were at the SPLASH_SCREENS state.
                inGameScript.Dismiss();         // If the players were at the IN_GAME state.
                mainMenuScript.Init();
                SetPlayerInputPermission(true);
                break;
            case State.STORY:
                mainMenuScript.Dismiss();
                storyScript.Init();
                break;
            case State.GAME_FADE_IN:
                SetPlayerInputPermission(false);
                storyScript.Dismiss();          // If the players were at the STORY state.
                inGameScript.Init();
                break;
            case State.IN_GAME:
                SetPlayerInputPermission(true);
                // TODO.
                break;
            case State.GAME_PAUSE:
                // TODO.
                break;
            case State.GAME_OVER:
                SetPlayerInputPermission(false);
                // TODO.
                break;
            case State.GAME_WON:
                SetPlayerInputPermission(false);
                // TODO.
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void SetPlayerInputPermission (bool value)
    {
        allowPlayerInputs = value;
    }
}
