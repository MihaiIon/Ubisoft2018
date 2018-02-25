using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager : MonoBehaviour {

    // Load Scripts
    ConnectPlayers connectPlayers;

    // All the possible states
    public enum State
    {
        INITIALIZING,
        SPLASH_SCREENS,
        MAIN_MENU,
        STORY,
        FADE_IN_GAME,
        IN_GAME,
        GAME_PAUSE,
        GAME_OVER,
        GAME_WON
    }

    [SerializeField]
    public State currState; 
    
    //----------------------------------------------------------------
    
    /// <summary>
    /// 
    /// </summary>
	void Awake () {
        loadScripts();
        setState(State.INITIALIZING);
	}

    void loadScripts ()
    {
        connectPlayers = GetComponent<ConnectPlayers>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
	private void setState (State s)
    {
        switch (s)
        {
            case State.INITIALIZING:
                connectPlayers.init();
                break;
            case State.SPLASH_SCREENS:
                break;
            case State.MAIN_MENU:
                break;
            case State.STORY:
                break;
            case State.FADE_IN_GAME:
                break;
            case State.IN_GAME:
                break;
            case State.GAME_PAUSE:
                break;
            case State.GAME_OVER:
                break;
            case State.GAME_WON:
                break;
        }
    }
}
