﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    // Scripts
    private StoryController storyController;
    private InGameController inGameController;
    private TextoController textoController;

    // All the possible states
    public enum State
    {
        INITIALIZING,
        STORY,
        GAME_FADE_IN,
        IN_GAME,
        GAME_PAUSE,
        GAME_OVER,
        GAME_WON
    }

    [SerializeField]
    public State currentState;

    [SerializeField]
    public bool allowPlayerInputs;
    
    //----------------------------------------------------------------
    
    /// <summary>
    /// 
    /// </summary>
	void Awake () {
        LoadControllers();
        SetAllowPlayerInputs(false);
        SetState(State.INITIALIZING);
    }

    /// <summary>
    /// Load all the scripts.
    /// </summary>
    void LoadControllers()
    {
        storyController = GetComponent<StoryController>();
        inGameController = GetComponent<InGameController>();
        textoController = GetComponent<TextoController>();
    }

    /// <summary>
    /// Set the current state of the GameManager
    /// </summary>
    /// <param name="s"></param>
	public void SetState (State state)
    {
        // Set current State
        currentState = state;

        // Execute related scripts.
        switch (state)
        {
            case State.INITIALIZING:
                storyController.Init();
                break;
            case State.STORY:
                storyController.LaunchStory();
                break;
            case State.GAME_FADE_IN:
                // SetAllowPlayerInputs(false);
                inGameController.Init();
                break;
            case State.IN_GAME:
                // SetAllowPlayerInputs(true);
                // TODO.
                break;
            case State.GAME_PAUSE:
                // TODO.
                break;
            case State.GAME_OVER:
                // SetAllowPlayerInputs(false);
                // TODO.
                break;
            case State.GAME_WON:
                // SetAllowPlayerInputs(false);
                // TODO.
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public GameManager.State GetState()
    {
        return currentState;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void SetAllowPlayerInputs (bool value)
    {
        allowPlayerInputs = value;
    }
}
