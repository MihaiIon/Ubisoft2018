using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour {

    // Managers
    private GameManager gameManager;
    public StoryManager storyManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Use this for initialization
    public void Init () {
		
	}

    /// <summary>
    /// 
    /// </summary>
    public void Dismiss()
    {

    }
}
