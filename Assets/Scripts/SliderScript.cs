using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;

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
    }

    // Update is called once per frame
    void Update()
    {
        prevValue = _slider.value;
    }
}
