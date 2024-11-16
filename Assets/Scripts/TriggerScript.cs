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
        // Set right ground object
        float x = ground.transform.position.x + (ground.GetComponent<EnvironmentGenerator>()._levelLength - 1) * ground.GetComponent<EnvironmentGenerator>()._xMultiplier;
        ground1.transform.position = new Vector3(x, 0, 0);
        ground1.GetComponent<EnvironmentGenerator>()._noiseStep = 0.6f;
        ground1.GetComponent<EnvironmentGenerator>()._levelLength = 15;

        // Set initial trigger position
        float offset = ground1.transform.position.x + ground1.GetComponent<EnvironmentGenerator>()._xMultiplier * (ground1.GetComponent<EnvironmentGenerator>()._levelLength - 1) / 2;
        transform.position = new Vector3(offset, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void MoveGround(GameObject left, GameObject right)
    {
        EnvironmentGenerator env = left.GetComponent<EnvironmentGenerator>();
        EnvironmentGenerator env1 = right.GetComponent<EnvironmentGenerator>();

        // Move left ground to the right of the right ground
        float length = env1._xMultiplier * (env1._levelLength - 1);  // length of right ground
        float end = env1.transform.position.x;
        left.transform.position = new Vector3(end + length, 0, 0);

        // env._noiseStep = 0.4f;
        // env._levelLength = 25;

        env._isEnd = !env._isEnd;
        env1._isEnd = !env1._isEnd;

        // Update trigger position
        float offset = left.transform.position.x + left.GetComponent<EnvironmentGenerator>()._xMultiplier * (left.GetComponent<EnvironmentGenerator>()._levelLength - 1) / 2;
        transform.position = new Vector3(offset, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        /*TODO:
        fix unsmooth ground transitions
        make trigger invis
        randomise ground objects upon moving
        */

        if (triggerIsActive)
        {
            triggerIsActive = false;
            StartCoroutine(Pause());  // Ensure that the ground is shifted only once per trigger

            if(ground.GetComponent<EnvironmentGenerator>()._isEnd){
                MoveGround(ground1, ground);
            } else {
                MoveGround(ground, ground1);
            }
            // ssc.BakeCollider();
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        triggerIsActive = true;
    }
}
