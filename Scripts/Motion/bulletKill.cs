using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletKill : MonoBehaviour {

    public ParticleSystem explosion;
    public ParticleSystem explosion2;
    public ParticleSystem explosion3;
    public ParticleSystem explosion4;
    
    private void OnTriggerEnter(Collider other)
    {
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

    public void ExplodeOnHurt()
    {
            Instantiate(explosion3, transform.position, Quaternion.identity);
            Instantiate(explosion4, transform.position, Quaternion.identity);
        
    }
    public void Explode()
    {
            Instantiate(explosion2, transform.position, Quaternion.identity);
            Instantiate(explosion, transform.position, Quaternion.identity);
        
    }
}
