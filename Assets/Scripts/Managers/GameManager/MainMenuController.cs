using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    // Managers
    private GameManager gameManager;
    public MainMenuManager mainMenuManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Use this for initialization
    public void Init()
    {

        // When all is in place
        gameManager.SetAllowPlayerInputs(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
