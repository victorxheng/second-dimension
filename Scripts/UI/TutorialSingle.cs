using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialSingle : MonoBehaviour {

    public TextMeshProUGUI mesh;

    private void Start()
    {
     //   StartCoroutine("StartTutorial");
    }
    IEnumerator StartTutorial()
    {
        if (PlayerPrefs.GetInt("waveNumber", 0) == 0)
        {
            yield return new WaitForSeconds(2f);

            mesh.text = "<= MOVE FINGER HERE TO MOVE AROUND";
            StartCoroutine(FadeObject(3));

            yield return new WaitForSeconds(5f);

            mesh.text = "	MOVE FINGER HERE TO SHOOT =>";
            StartCoroutine(FadeObject(3));

            yield return new WaitForSeconds(7f);

            mesh.text = "USE THIS TO SHOOT ENEMIES =>";
            StartCoroutine(FadeObject(3));

            yield return new WaitForSeconds(5f);

            mesh.text = "<= USE THIS TO MOVE TO COLLECT CASH";
            StartCoroutine(FadeObject(3));

            yield return new WaitForSeconds(5f);

            mesh.text = "CASH CAN BE SPENT FOR UPGRADES";
            StartCoroutine(FadeObject(3));

            yield return new WaitForSeconds(5f);
        }

    }

    public void tutorialText(string text, int seconds)
    {
        mesh.text = text;
        StartCoroutine(FadeObject(seconds));
    }

    IEnumerator FadeObject(int seconds)
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = mesh.color;
            c.a = f;
            mesh.color = c;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(seconds);

        for (float f = 1; f >= -0.05; f -= 0.05f)
        {
            Color c = mesh.color;
            c.a = f;
            mesh.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
