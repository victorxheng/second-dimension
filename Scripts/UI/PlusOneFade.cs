using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlusOneFade : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        TextMeshProUGUI currentText = gameObject.GetComponent<TextMeshProUGUI>();
        currentText.color = new Color(Random.Range(0.0f, 0.4f), Random.Range(0.4f, 1), Random.Range(0.0f, 0.4f), 0);
        StartCoroutine(FadeTextPattern());
        if(PlayerPrefs.GetInt("cashAmount", 0) > 999)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(40, 135), 25);
        }
        else if (PlayerPrefs.GetInt("cashAmount", 0) > 99)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(50, 120), 25);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(60, 100), 25);
        }
	}
    IEnumerator FadeTextPattern()
    {
        setAlphaColor(0);
            setAlphaColor(0);
            for (float f = 0.1f; f <= 0.7; f += 0.5f)
            {
                setAlphaColor(f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.2f);
            for (float f = 0.7f; f >= 0.1; f -= 0.06f)
            {
                setAlphaColor(f);
                yield return new WaitForSeconds(0.05f);
            }
        Destroy(gameObject);
    }
    

    private void setAlphaColor(float f)//set renders
    {
        textSet(gameObject.GetComponent<TextMeshProUGUI>(), f);
    }
    private void textSet(TextMeshProUGUI txt, float f)
    {
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, f);
    }
}

