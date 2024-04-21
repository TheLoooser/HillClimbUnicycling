using UnityEngine;

public class DriveUnicycle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _tireRB;
    [SerializeField] private float _speed = 150f;
    private float _moveInput;

    private RaycastHit2D hit;
    public Transform raycastPoint;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

        Vector2 forward = transform.TransformDirection(Vector2.right) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

    }

    private void FixedUpdate()
    {
        _tireRB.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);

        hit = Physics2D.Raycast(raycastPoint.position, Vector2.down);
        Debug.Log(hit.transform);
        Debug.Log(hit.collider);
        Debug.Log(hit.distance);

        transform.position = new Vector2(transform.position.x, transform.position.y - hit.distance + 1.02f);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, 0), transform.TransformDirection(Vector2.down) * 10, Color.yellow);
        
    }
}
