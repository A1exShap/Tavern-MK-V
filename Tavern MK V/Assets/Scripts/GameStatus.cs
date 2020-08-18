using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private bool _isPaused;

    public enum Status
    {
        None,
        Run,
        Pause,
        Restart,
        Exit
    };

    private void Start()
    {
        SetStatus(Status.Run);
    }

    public void SetStatus (Status status)
    {
        switch (status)
        {
            //case Status.Run: 
        }
    }

    private void StartGame()
    {

    }


}
