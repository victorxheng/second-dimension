using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleEffectsControl : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        ParticleSystem particle = gameObject.GetComponent<ParticleSystem>();
        if (PlayerPrefs.GetInt("effects", 1) == 1)
        {
            particle.enableEmission = true;
        }
        else
        {
            particle.enableEmission = false;
        }

    }

}
