using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroy : MonoBehaviour
{
    public float seconds;
    private SpriteRenderer coinRender;
    public ParticleSystem explosion;
    public ParticleSystem explosion2;
    void Start()
    {
        StartCoroutine("DestroyObject");
    }

    public IEnumerator DestroyObject()
    {

        yield return new WaitForSeconds(seconds);
        if(PlayerPrefs.GetInt("waveNumber", 0) == 0)
        {
            yield return new WaitForSeconds(5);
        }

        coinRender = gameObject.GetComponent<SpriteRenderer>();
        for (float f = 1; f >= -0.05; f -= 0.05f)
        {
            Color c = coinRender.material.color;
            c.a = f;
            coinRender.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }

        gameObject.SetActive(false);
    }
    public GameObject plusOne;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("cashAmount", PlayerPrefs.GetInt("cashAmount", 0) + 1);
            Vector3 position = transform.position;
            this.gameObject.SetActive(false);
            
                Instantiate(explosion2, position, Quaternion.identity);
                Instantiate(explosion, position, Quaternion.identity);
            Instantiate(plusOne, GameObject.FindGameObjectWithTag("CashTextParent").transform);

        }
    }
}
