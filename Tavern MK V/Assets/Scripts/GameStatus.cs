using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameStatus// : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _isPaused;
    private int _currentScene;
   

    public void Restart()
    {

    }

    //public void Pause()
    //{
    //    _isPaused ? _pauseMenu.

    //    if (_isPaused)
    //    {
    //        _pauseMenu.
    //        Time.timeScale = 1;
    //        // Hide menu panel
    //        _isPaused = false;
    //    }
    //    else
    //    {

    //    }

    //    void 
    //}

    public void Exit()
    {
        //
        // Save Game
        //

        Application.Quit(0);
    }
}
