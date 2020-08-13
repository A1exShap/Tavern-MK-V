using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST2 : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private float _horizontalSpeed = 0.2f
    [SerializeField] private float _verticalSpeed = 0.3f
    [SerializeField] private Transform _hand;

    private float _z;
    private Vector3 _startClick;
    private bool _canResetZ = true;
    private float _maxMagnitude = 60;
    private FixedJoint _lastObject;
    void Start()
    {
        _lastObject = GameObject.Find("LAST").GetComponent<FixedJoint>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _canResetZ = false;
            CancelInvoke(nameof(ResetZ));
            _startClick = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            var swipe = GetSwipeDirection(Input.mousePosition - _startClick);
            var horizontal = Vector3.left * swipe * Time.deltaTime;

            transform.position -= horizontal;
            _hand.position -= horizontal;

            _hand.Rotate(0, 0, -swipe * _horizontalSpeed );
            _z += -swipe * _horizontalSpeed;

        }
        if (Input.GetMouseButtonUp(0))
        {
            Invoke(nameof(ResetZ), 0.2f);
        }

        if (!Input.GetMouseButton(0))
        {
            _hand.rotation = Quaternion.Lerp(_hand.rotation, Quaternion.Euler(0, 0, 0), 5 * Time.deltaTime);

            if (_canResetZ)
            {
                _z = Mathf.Abs(_z);
                if (_z > 0) _z -= _horizontalSpeed;
                else _z = 0;
            }
        }

        if (_lastObject != null)
        {
            if (Mathf.Abs(_z) > 10 || _lastObject.currentForce.magnitude > _maxMagnitude)
            {
                foreach (var item in FindObjectsOfType<FixedJoint>())
                {
                    Destroy(item);
                }
            }
        }

        var vertical = Vector3.forward * _verticalSpeed * Time.deltaTime;

        transform.position -= vertical;
        _hand.position -= vertical;
    }

    private void ResetZ()
    {
        _canResetZ = true;
    }

    private float GetSwipeDirection(Vector3 swipe)
    {
        float x = Mathf.Abs(swipe.x);
        float y = Mathf.Abs(swipe.y);

        if (x > y)
        {
            return (swipe.x > 0) ? 1 : -1;
        }
        else return 0;
    }
}
