using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour {

    // GameManager reference
    private GameManager gameManager;
    public InGameManager inGameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    /// <summary>
    /// 
    /// </summary>
	public void Init ()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void Dismiss()
    {

    }
}
