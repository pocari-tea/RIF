using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChanageScene : MonoBehaviour
{
    public void MoveIngame()
    {
        SceneManager.LoadScene("Map");
    }

    public void MoveLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
