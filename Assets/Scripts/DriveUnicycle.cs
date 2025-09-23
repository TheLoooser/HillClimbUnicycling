using UnityEngine;
using UnityEngine.UI;

public class DriveUnicycle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _tireRB;
    [SerializeField] private float _speed = 150f;
    [SerializeField] private Slider _slider;
    //PID constants
    [SerializeField] private float Kp;
    [SerializeField] private float Ki;
    [SerializeField] private float Kd;
     //Line Renderer
    [SerializeField] private LineController line;


    //private float _moveInput;
    private RaycastHit2D hit;
    public Transform raycastPoint;
    private float _currentSpeed = 0.0f;
    Vector3 lastPosition = Vector3.zero;
    //PID variables
    private float targetSpeed;
    private float previousError = 0.0f;
    private float integral = 0.0f;
    private float dt = 0.1f;
    private Vector2[] points;
    private int index = 0;


    /*
    TODO
    ---
    Fix issue when target speed = 0
    Tune PID constants
    Add graph
        two lines (actual speed, target speed)
        grid lines (0km/h, 50km/h, 100km/h, ...)
    */


    // Start is called before the first frame update
    void Start()
    {
        targetSpeed = _slider.value;

        //Initialise points
        points = new Vector2[1920];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector2(-910, 0);
        }
        line.SetUpLine(points, 0);
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

        _speed = ComputePID();
        Debug.Log("Speed (Acc): " + _speed.ToString());

        //Update line
        if (hit.distance < 5f)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Vector2(points[i].x - 1, points[i].y);
            }
            points[index] = new Vector2(1010, _currentSpeed * 1.5f);
            index++;
            index %= points.Length;
            line.SetUpLine(points, index);
        }
    }

    private float ComputePID()
    {
        // Calculate error
        float error = targetSpeed - _currentSpeed;
        Debug.Log("Error: " + error.ToString());

        // Proportional term
        float P_out = Kp * error;

        // Integral term
        integral += error * dt;
        float I_out = Ki * integral;

        // Derivative term
        float derivative = (error - previousError) / dt;
        float D_out = Kd * derivative;

        // Compute total output
        float output = P_out + I_out + D_out;

        // Update previous error
        previousError = error;

        return output;
    }
}
