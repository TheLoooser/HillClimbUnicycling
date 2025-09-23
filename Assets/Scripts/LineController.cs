using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Vector2[] points;
    private int index;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Vector2[] points, int index)
    {
        lr.positionCount = points.Length;
        this.points = points;
        this.index = index;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int idx = index;
        for (int i = 0; i < points.Length; i++)
        {
            if (idx != points.Length - 1)
            {
                lr.SetPosition(i, new Vector2(points[idx].x, points[idx].y));
            }
            idx++;
            idx %= points.Length;
        }
    }
}
