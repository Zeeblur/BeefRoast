using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Loads a level
    /// </summary>
    /// <param name="level">Name of level to load</param>
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// Quits Game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
