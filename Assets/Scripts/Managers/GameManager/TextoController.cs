using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoController : MonoBehaviour {

    // Sends textos to players
    public TextoManager textoManager;

    /// <summary>
    /// 
    /// </summary>
	public void Init ()
    {
        
    }

    /// <summary>
    /// Informs the players of the situation through a short story.
    /// This is done by sending textos to both players.
    /// </summary>
    public void LaunchStory ()
    {

    }

    /// <summary>
    /// Display a random texto to the players.
    /// </summary>
    /// <param name="msg">Message to be displayed</param>
    public void SendRandomTexto()
    {
        // textoManager.SendRandom(msg);
    }

    /// <summary>
    /// Display a simple texto to the players.
    /// The sender is Anonymous.
    /// </summary>
    /// <param name="msg">Message to be displayed</param>
    public void SendTexto (string msg)
    {
        // textoManager.SendTexto(msg);
    }

    /// <summary>
    /// Display a simple texto to the players with a custom sender name.
    /// </summary>
    /// <param name="sender">Message to be displayed</param>
    /// <param name="msg">Sender of the texto</param>
    public void SendTexto (string sender, string msg)
    {
        // textoManager.SendTexto(sender, msg);
    }

    /// <summary>
    /// Display a simple texto to the players with a custom sender name and a custom color.
    /// </summary>
    /// <param name="sender">Message to be displayed</param>
    /// <param name="msg">Sender of the texto</param>
    /// <param name="color">Color of the texto's body</param>
    public void SendTexo(string sender, string msg /*, TextoManager.Color color*/)
    {
        // textoManager.SendTexto(sender, msg, color);
    }

}
