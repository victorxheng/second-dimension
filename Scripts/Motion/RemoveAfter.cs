using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfter : MonoBehaviour {

    public float seconds;

	// Use this for initialization
	void Start () {
        StartCoroutine("DestroyParticles");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
