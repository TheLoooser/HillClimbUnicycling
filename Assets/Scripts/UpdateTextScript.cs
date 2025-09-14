using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        speed = (unicycle.gameObject.transform.position - lastPosition).magnitude;
        lastPosition = unicycle.gameObject.transform.position;
        text.text = (Math.Floor(speed * 10000) / 100).ToString();
    }
}
