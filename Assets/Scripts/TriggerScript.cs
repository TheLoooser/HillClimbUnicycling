using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.U2D;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject ground1;

    private bool triggerIsActive = true;

    // Start is called before the first frame update
    void Start()
    {
        ground1.transform.position = new UnityEngine.Vector3(38,0,0);  // length of ground = 38
        ground1.GetComponent<EnvironmentGenerator>()._noiseStep = 0.6f;
        ground1.GetComponent<EnvironmentGenerator>()._levelLength = 15;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        /*TODO: 
        Use trigger to create second ground object
        attach it to first
        move trigger to new location (delete previous ground object first, or move it with changes (no repetition of ground))
        make trigger invis
        */
        transform.position = new UnityEngine.Vector3(0,0,0);

        SpriteShapeController ssc = ground1.GetComponent<SpriteShapeController>();
        Debug.Log(ssc.spline.GetPosition(ssc.spline.GetPointCount() - 3));

        if (triggerIsActive)
        {
            triggerIsActive = false;
            StartCoroutine(Pause());  // Ensure that the ground is shifted only once per trigger
            ground.transform.position += new UnityEngine.Vector3(38*2,0,0);
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        triggerIsActive = true;
    }
}
