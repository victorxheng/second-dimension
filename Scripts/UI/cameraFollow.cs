using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
    public bool start = false;
    // Use this for initialization
    void Start()
    {
        offset = new Vector3(0, 0, -10);
        StartCoroutine(startOffset());
    }
    IEnumerator startOffset()
    {
        yield return new WaitForSeconds(1.5f);
        start = true;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(start)
       transform.position = player.transform.position + offset;
    }
}
