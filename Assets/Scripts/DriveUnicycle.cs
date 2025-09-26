using UnityEngine;
using UnityEngine.UI;

public class DriveUnicycle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _tireRB;
    [SerializeField] private float _speed = 150f;
    [SerializeField] private Slider _slider;

    //PID constants
    [SerializeField] private Slider Kp; //3.33
    [SerializeField] private Slider Ki; //0.02
    [SerializeField] private Slider Kd; //0.05

     //Line Renderer
    [SerializeField] private LineController line;

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

    }

    private void FixedUpdate()
    {
        // Accelerate
        _tireRB.AddTorque(-_speed * Time.fixedDeltaTime);

        // Glue unicycle to the ground
        hit = Physics2D.Raycast(raycastPoint.position, Vector2.down);
        transform.position = new Vector2(transform.position.x, transform.position.y - hit.distance + 1.02f);

        // Update acceleration force
        targetSpeed = _slider.value;
        Vector3 movingDirection = transform.position - lastPosition;
        _currentSpeed = movingDirection.magnitude * 100;

        float dotProduct = Vector3.Dot(movingDirection, transform.right);

        if (dotProduct < 0.01f) // Use a small tolerance for floating point comparisons
        {
            _currentSpeed *= -1;
        }

        lastPosition = transform.position;
        _speed = ComputePID();

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

        // Proportional term
        float P_out = Kp.value * error;

        // Integral term
        integral += error * dt;
        float I_out = Ki.value * integral;

        // Derivative term
        float derivative = (error - previousError) / dt;
        float D_out = Kd.value * derivative;

        // Compute total output
        float output = P_out + I_out + D_out;

        // Update previous error
        previousError = error;

        return output;
    }
}
