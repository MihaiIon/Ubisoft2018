using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectPlayersState : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
	public void Init ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
    } 
}
