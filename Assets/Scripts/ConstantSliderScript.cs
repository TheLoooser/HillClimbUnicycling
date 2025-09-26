using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstantSliderScript : MonoBehaviour
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
            _sliderText.text = v.ToString("0.00");
        });
    }

    // Update is called once per frame
    void Update()
    {
        prevValue = _slider.value;
    }
}
