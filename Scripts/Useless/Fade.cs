using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
    
    public SpriteRenderer render;
    public GameObject current;
    
    public void PlusOne(Vector3 position)
    {
        var renderNow = Instantiate(render, position, Quaternion.identity);
        StartCoroutine(FadePlusOne(renderNow));
    }

    IEnumerator FadePlusOne(SpriteRenderer rend)
    {
        rend = GetComponent<SpriteRenderer>();
        Color n = rend.material.color;
        n.a = 0f;
        rend.material.color = n;
        for (float f = 0.05f; f <= 1; f += 0.1f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.2f);
        for (float f = 1; f >= -0.05; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
