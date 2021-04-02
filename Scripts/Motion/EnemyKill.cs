using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    public ParticleSystem explosion;
    public ParticleSystem explosion2;
    public ParticleSystem explosion3;
    public ParticleSystem explosion4;

    MeshRenderer currentMesh;
    TrailRenderer trailRender;
    Vector3 position;


    Color yellow = Color.yellow;
    Color grey = Color.grey;
    Color green = Color.green;
    Color cyan = Color.cyan;
    Color blue = Color.blue;

    private EnemyMovement em;
    private GameObject Spawner;
    private EnemySpawn es;
    private EnemyTag et;

    private int tag;
    
    public Material cracked1;
    public Material cracked2;
    public Material cracked3;

    private float health;
    private void Start()
    {
        em = gameObject.GetComponent<EnemyMovement>();
        et = gameObject.GetComponent<EnemyTag>();
        es = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawn>();
        PlayerPrefs.SetInt("point", 0);

        if (et.tag == 3 || et.tag == 5)
            health = 3;
        else if (et.tag == 6)
            health = 12;
        else if (et.tag == 7)
            health = 20;
        else if (et.tag == 9)
            health = 3;
        else if (et.tag == 10)
            health = 3;
        else if (et.tag == 11)
            health = 4;
        else
            health = 2;
        
    }
    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            if(et.tag == 1 || et.tag == 0 || et.tag == 8)
            {
                PlayerPrefs.SetInt("point", 1);
                Explode();
                gameObject.SetActive(false);
            }
            else if(health <= 0)
            {
                tag = et.tag;

                if (tag == 7) spawnFourMore(1, 1);
                else if (tag == 6) spawnFourMore(1, 0.8f);
                else if (tag == 4 || tag == 11) spawnTwoMore(1, 0.4f);
                else
                    es.SpawnMore(tag - 1, transform.position);
                
                
                gameObject.SetActive(false);
            }
            else
            {
                MeshRenderer enemyRender = gameObject.GetComponent<MeshRenderer>();
                Color currentColor = enemyRender.material.color;
                if(health == 1 && (et.tag == 3 || et.tag == 5)) enemyRender.material = cracked1;                
                else if(health == 8) enemyRender.material = cracked2;
                else if(health > 8) enemyRender.material = cracked3;
                else enemyRender.material = cracked2;
                

                enemyRender.material.color = currentColor;
            }

          if(other.gameObject.CompareTag("Bullet"))other.gameObject.SetActive(false);  
           
        }



        if (other.gameObject.CompareTag("Player"))
        {
            ExplodeOnHurt();

            PlayerPrefs.SetInt("playerHealth", PlayerPrefs.GetInt("playerHealth", 10) - 1);

            this.gameObject.SetActive(false);

        }
        if (other.gameObject.CompareTag("Border"))
        {
            Explode();
            

            this.gameObject.SetActive(false);

        }
    }
    
    
    public void Explode()
    {
            Instantiate(explosion2, transform.position, Quaternion.identity);
            Instantiate(explosion, transform.position, Quaternion.identity);
        
    }

    public void ExplodeOnHurt()
    {
            Instantiate(explosion3, transform.position, Quaternion.identity);
            Instantiate(explosion4, transform.position, Quaternion.identity);
        
    }

    private void spawnFourMore(int removeLayers, float factor)
    {
        es.SpawnMore(tag - removeLayers, new Vector3(transform.position.x + factor, transform.position.y + factor, 0));
        es.SpawnMore(tag - removeLayers, new Vector3(transform.position.x - factor, transform.position.y + factor, 0));
        es.SpawnMore(tag - removeLayers, new Vector3(transform.position.x + factor, transform.position.y - factor, 0));
        es.SpawnMore(tag - removeLayers, new Vector3(transform.position.x - factor, transform.position.y - factor, 0));

    }
    private void spawnTwoMore(int removeLayers, float factor)
    {
        es.SpawnMore(tag - removeLayers, new Vector3(transform.position.x + factor, transform.position.y + factor, 0));
        es.SpawnMore(tag - removeLayers, new Vector3(transform.position.x - factor, transform.position.y - factor, 0));

    }

}
