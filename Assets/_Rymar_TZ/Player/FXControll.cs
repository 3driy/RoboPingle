using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXControll : MonoBehaviour
{
    [SerializeField] float waterHeight;

    PlayerMoveControll controllScript;
    [SerializeField]
    GameObject[] trails;
    ParticleSystem[] trailsPart;
    [SerializeField]
    GameObject waterRipple;
    ParticleSystem ripple;
    

    // Start is called before the first frame update
    void Start()
    {
        controllScript = GetComponent<PlayerMoveControll>();
        ripple = waterRipple.GetComponent<ParticleSystem>();
        trailsPart = new ParticleSystem[trails.Length];
        for (int i = 0; i < trails.Length; i++)
        {
            trailsPart[i] = trails[i].GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        WaterRipple();

        TrailsEmission();

    }



    void WaterRipple() {

            var emission = ripple.emission;
        if (transform.position.y <= waterHeight)
        { //underwater
            Vector3 currentPos = waterRipple.transform.position;
            waterRipple.transform.position = new Vector3(currentPos.x, waterHeight + 0.01f, currentPos.z);
            emission.rateOverTime =  2f;
            emission.rateOverDistance = 8f;
        }
        else {
            emission.rateOverDistance = emission.rateOverTime = 0f;
        }
    
    }


    void TrailsEmission() {

        if (trails[0].transform.position.y <= waterHeight)
        { //underwater
            foreach (ParticleSystem trail in trailsPart)
            {
                var emission = trail.emission;
                emission.rateOverTimeMultiplier = 0;
            }



        }
        else {

            foreach (ParticleSystem trail in trailsPart)
            {
                var emission = trail.emission;
               emission.rateOverTime = controllScript.inputMagnitude * 15f;
            }
        
        
        }
    
    
    }
}
