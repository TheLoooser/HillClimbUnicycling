using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject unicycle;

    private float speed = 0.0f;
    Vector3 lastPosition = Vector3.zero;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 movingDirection = unicycle.gameObject.transform.position - lastPosition;
        speed = movingDirection.magnitude * 100;
        lastPosition = unicycle.gameObject.transform.position;

        float dotProduct = Vector3.Dot(movingDirection, transform.right);

        if (dotProduct < 0.01f) // Use a small tolerance for floating point comparisons
        {
            speed *= -1;
        }
        
        text.text = (Math.Floor(speed * 100) / 100).ToString();
    }
}
