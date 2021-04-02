using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Fire : MonoBehaviour
{

    public GameObject weapon1;

    public GameObject player;
    private Vector3 myPos;
    
    private float height;
    private float width;

    public Joystick joystick;
    public MapDimensions md;

    private void Awake()
    {
        height = md.height;
        width = md.width;
    }
    private void Start()
    {
        StartCoroutine("SpawnFire");
    }
    IEnumerator SpawnFire()
    {
        while (SceneManager.GetActiveScene().buildIndex.Equals(0))
        {

            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                //float joystickDistance = (float)Math.Sqrt(joystick.Horizontal * joystick.Horizontal + joystick.Vertical * joystick.Vertical);
               StartCoroutine(fire(weapon1, PlayerPrefs.GetInt("bulletSpeed", 20)));
            }
            yield return new WaitForSeconds(1/ (float)PlayerPrefs.GetInt("fireRate", 12));

        }
    }
    IEnumerator fire(GameObject weapon, int moveSpeed)
    {
        myPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        Vector3 targetPos = new Vector3(joystick.Horizontal * 10000f, joystick.Vertical * 10000f, 0);

        var bullet = Instantiate(weapon, myPos, Quaternion.LookRotation(targetPos));
        
        
            MeshRenderer bulletRender = bullet.GetComponent<MeshRenderer>();
            bulletRender.material.color = Color.blue;
            TrailRenderer bulletTrail = bullet.GetComponent<TrailRenderer>();
            bulletTrail.material.color = Color.blue;
        

        while (bullet.transform.position.x > width / -2 -10&& bullet.transform.position.x < width / 2 +10 && bullet.transform.position.y > height / -2 -10 && bullet.transform.position.y < height / 2 +10)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetPos, moveSpeed * Time.deltaTime);
            if (!bullet.activeSelf)
            {
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(bullet);
    }
}
