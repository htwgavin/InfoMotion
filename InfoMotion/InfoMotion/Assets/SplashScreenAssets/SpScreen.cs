using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//references oben einfügen
public class SpScreen : MonoBehaviour
{
    public Image splashImage;
    public string loadlevel;

    IEnumerator Start()
    {
        splashImage.canvasRenderer.SetAlpha(0.0f); //wenn alpha auf 0 bild nicht mehr sichtbar

        FadeIn();
        yield return new WaitForSeconds(2.5f); // 2.5 sekunden warten da es 1.5 sekunden eingefaded wird
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(loadlevel);

    }


    void FadeIn()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false); // fade alpha vom Nichts zu 1 in 1.5 sekunden

    }

   void FadeOut()
    {
        splashImage.CrossFadeAlpha(0f, 2.5f, false);
     

    }

}