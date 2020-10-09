using UnityEngine;


public sealed class PlayerMove : MonoBehaviour, IUpdateble
{
    [SerializeField] private Transform _hand;
    [SerializeField] private float _horizontalSpeed = 0.2f;
    [SerializeField] private float _verticalSpeed = 0.3f;

    private Transform _transform;
    private float _z;
    private Vector3 _startClick;
    private bool _canResetZ = true;
    //private float _maxMagnitude = 60;
    private GameObject _lastObject;

    private void OnEnable()
    {
        BaseBehaviour.Instance.AddToUpdateble(this);
    }

    private void OnDisable()
    {
        if (BaseBehaviour.Instance == null) return;
        BaseBehaviour.Instance.RemoveFromUpdateble(this);
    }

    void Start()
    {
        _transform = transform;
        //_lastObject = GameObject.Find("LAST").GetComponent<FixedJoint>();
        //_lastObject = _hand.GetComponent<FixedJoint>().connectedBody.gameObject;
    }

    void IUpdateble.DoUpdate()
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
            var invertedSwipe = -swipe * _horizontalSpeed;

            _transform.position -= horizontal;// * 1.5f;
            //_hand.position -= horizontal;

            

            //_hand.Rotate(0, 0, invertedSwipe);
            if ((invertedSwipe > 0 && _hand.rotation.z < 0.1f) || (invertedSwipe < 0 && _hand.rotation.z > -0.1f))
            {
                _hand.Rotate(0, 0, invertedSwipe);
            }

            _z += invertedSwipe;

        }

        else if (Input.GetMouseButtonUp(0))
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
                //else _z = 0;
            }
        }

        //if (_lastObject != null)
        //{


        //    if (Mathf.Abs(_z) > 10) //|| _lastObject.currentForce.magnitude > _maxMagnitude)
        //    {
        //        Destroy(_lastObject);
        //        //foreach (var item in _hand.FindObjectsOfType<FixedJoint>())
        //        //{
        //        //    Destroy(item);
        //        //}
        //    }
        //}

        var vertical = Vector3.forward * _verticalSpeed * Time.deltaTime;

        _transform.position -= vertical;
        //_hand.position -= vertical;
    }

    private void ResetZ()
    {
        _canResetZ = true;
    }

    private float GetSwipeDirection(Vector3 swipe)
    {
        var x = Mathf.Abs(swipe.x);
        var y = Mathf.Abs(swipe.y);

        if (y >= x) return 0;
        return swipe.x > 0 ? 1 : -1;
    }
}
