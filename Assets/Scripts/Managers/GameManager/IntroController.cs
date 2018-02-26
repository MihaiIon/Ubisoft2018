using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour {

    // Managers
    public IntroManager introManager;
    private GameManager gameManager;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    /// <summary>
    /// 
    /// </summary>
	public void ConnectPlayers ()
    {


        // When all is done
        gameManager.SetState(GameManager.State.SPLASH_SCREENS);
    }

    /// <summary>
    /// 
    /// </summary>
    public void DisplaySplashScreens ()
    {

        // When all is done
        gameManager.SetState(GameManager.State.MAIN_MENU);
    }
}
