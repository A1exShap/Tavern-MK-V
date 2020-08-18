using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObserver : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            Debug.Log("Lose");
        }

        else if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Win");
        }
    }
}
