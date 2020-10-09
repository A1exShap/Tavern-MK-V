using UnityEngine;


public sealed class CollisionObserver : MonoBehaviour
{
    #region UnityMethods

    public delegate void OnCollision(StatusType statusType);
    public event OnCollision onCollision;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            onCollision?.Invoke(StatusType.Lose);
        }

        else if (other.gameObject.CompareTag("Finish"))
        {
            onCollision?.Invoke(StatusType.Win);
        }
    }

    #endregion
}
