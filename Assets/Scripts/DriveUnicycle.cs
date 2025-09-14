using UnityEngine;
using UnityEngine.UI;

public class DriveUnicycle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _tireRB;
    [SerializeField] private float _speed = 150f;
    [SerializeField] private Slider _slider;

    //private float _moveInput;
    private RaycastHit2D hit;
    public Transform raycastPoint;
    private float targetSpeed;
    private float _currentSpeed = 0.0f;
    Vector3 lastPosition = Vector3.zero;

    /*
    TODO
    ---
    Fix issue when target speed = 0
    Add PID to control speed (acceleration)
    Add graph
        list of points (draw each frame)
        new point in the middle of the screen
        old points move to the left
        two lines (actual speed, target speed)
    */


    // Start is called before the first frame update
    void Start()
    {
        targetSpeed = _slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        //_moveInput = Input.GetAxisRaw("Horizontal");

        // Vector2 forward = transform.TransformDirection(Vector2.right) * 10;
        // Debug.DrawRay(transform.position, forward, Color.green);

    }

    private void FixedUpdate()
    {
        //_tireRB.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
        _tireRB.AddTorque(-_speed * Time.fixedDeltaTime);

        hit = Physics2D.Raycast(raycastPoint.position, Vector2.down);
        // Debug.Log(hit.transform);
        // Debug.Log(hit.collider);
        // Debug.Log(hit.distance);

        transform.position = new Vector2(transform.position.x, transform.position.y - hit.distance + 1.02f);
        // Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, 0), transform.TransformDirection(Vector2.down) * 10, Color.yellow);

        //-----------------------------------------------------------------
        targetSpeed = _slider.value;
        _currentSpeed = (transform.position - lastPosition).magnitude * 100;
        lastPosition = transform.position;

        //Debug.Log("Current: " + _currentSpeed);
        //Debug.Log("Target: " + targetSpeed);
        //Debug.Log("Acc: " + _speed);
        //Debug.Log(_currentSpeed < targetSpeed);

        if (_currentSpeed < targetSpeed)
        {
            _speed = 200f;
        }
        else
        {
            _speed = -200f;
        }
    }
}
