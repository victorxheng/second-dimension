using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    private GameObject player;
    public float moveSpeed;
    public Vector3 moveLocation;
    private Rigidbody erb;
    // Use this for initialization
    void Start ()
    {
        erb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(moveEnemy());
        Vector3 MoveLocation = player.transform.position;
        Vector3 SpawnLocation = gameObject.transform.position;
        float y = MoveLocation.y - SpawnLocation.y;
        float x = MoveLocation.x - SpawnLocation.x;
        float rotation;
        if(x>0&&y>0) rotation = Mathf.Atan2(Mathf.Abs(y), Mathf.Abs(x));
        else if (x < 0 && y > 0) rotation = 180-Mathf.Atan2(Mathf.Abs(y), Mathf.Abs(x));
        else if (x < 0 && y < 0) rotation = -(180 - Mathf.Atan2(Mathf.Abs(y), Mathf.Abs(x)));
        else rotation = -Mathf.Atan2(y, x);

        transform.Rotate(rotation * 3.1416f / 180f, 0,0);

        erb.velocity = new Vector2(Mathf.Cos(rotation) * moveSpeed, Mathf.Sin(rotation) *moveSpeed);
    }
    /*
    IEnumerator moveEnemy()
    {
        while (gameObject.activeSelf)
        {
            //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, moveSpeed * Time.deltaTime);


            if (!gameObject.activeSelf)
            {
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
	*/
	// Update is called once per frame
	void Update () {
		
	}
}
