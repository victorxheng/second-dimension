using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFire : MonoBehaviour {
    private GameObject player;
    public GameObject weapon1;

    public ParticleSystem explosion;


    private int bulletSpeed = 20;
    private float currentFireSpeed = 1;
    private EnemyTag et;
    

    void Start () {

        et = gameObject.GetComponent<EnemyTag>();

        player = GameObject.FindGameObjectWithTag("Player");

        if(et.tag == 8)
        {
            bulletSpeed = 16;
            currentFireSpeed = 0.8f;
        }
        else if (et.tag == 9)
        {
            bulletSpeed = 18;
            currentFireSpeed = 1f;
        }
        else if (et.tag == 10)
        {
            bulletSpeed = 20;
            currentFireSpeed = 1.2f;
        }
        else if (et.tag == 11)
        {
            bulletSpeed = 22;
            currentFireSpeed = 1.4f;
        }

        if (et.tag > 7)
        {
            StartCoroutine(FireControl(bulletSpeed, weapon1));
        }
    }
    IEnumerator FireControl(int fireSpeed, GameObject weapon)
    {
        while (SceneManager.GetActiveScene().buildIndex.Equals(0))
        {            
            StartCoroutine(fire(fireSpeed, weapon));
            
            yield return new WaitForSeconds(1 / currentFireSpeed);

        }
    }
    IEnumerator fire(int fireSpeed, GameObject weapon)
    {
        Vector3 currentPos = new Vector3(transform.position.x, transform.position.y);
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y);

        var bullet = Instantiate(weapon, currentPos, Quaternion.LookRotation(targetPos));


        while (bullet != null)
        {
            if (bullet.transform.position != targetPos)
            {
                bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetPos, fireSpeed * Time.deltaTime);
                if (!bullet.activeSelf)
                {
                    break;
                }

                yield return new WaitForSeconds(Time.deltaTime);
            }
            else
            {
                break;
            }
        }

        if (bullet != null)
        {
            Instantiate(explosion, bullet.transform.position, Quaternion.identity);
        }
            Destroy(bullet);
        
    }
}
