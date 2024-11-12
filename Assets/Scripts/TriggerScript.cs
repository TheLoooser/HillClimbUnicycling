using System.Collections;
using UnityEngine;

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

        if (triggerIsActive)
        {
            triggerIsActive = false;
            StartCoroutine(Pause());  // Ensure that the ground is shifted only once per trigger

            int length = 2*(ground1.GetComponent<EnvironmentGenerator>()._levelLength - 1);
            float end = 2*(ground.GetComponent<EnvironmentGenerator>()._levelLength - 1) + ground.transform.position.x;
            ground.transform.position += new UnityEngine.Vector3(end + length, 0, 0);

            ground.GetComponent<EnvironmentGenerator>()._noiseStep = 0.4f;
            ground.GetComponent<EnvironmentGenerator>()._levelLength = 25;

            ground.GetComponent<EnvironmentGenerator>()._isEnd = true;
            ground1.GetComponent<EnvironmentGenerator>()._isEnd= false;
            // ssc.BakeCollider();
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        triggerIsActive = true;
    }
}
