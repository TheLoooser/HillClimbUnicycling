using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private LineController line;
    private Vector2[] points;
    private int index = 0;

    float prevValue;

    // Start is called before the first frame update
    void Start()
    {
        prevValue = _slider.value;
        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = v.ToString("0");
            Vector3 sliderLabelposition = _sliderText.GetComponent<RectTransform>().localPosition;
            _sliderText.GetComponent<RectTransform>().localPosition = new Vector3(
                sliderLabelposition.x + (v - prevValue) * 2,
                sliderLabelposition.y,
                sliderLabelposition.z
                );
        });

        //Initialise points
        points = new Vector2[1920];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector2(1010, _slider.value * 1.5f);
        }
        line.SetUpLine(points, 0);
    }

    // Update is called once per frame
    void Update()
    {
        prevValue = _slider.value;
    }

    private void FixedUpdate()
    {
        //Update line
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector2(points[i].x - 1, points[i].y);
        }
        points[index] = new Vector2(1010, _slider.value * 1.5f);
        index++;
        index %= points.Length;
        line.SetUpLine(points, index);
    }
}
