using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    //フェードインは、明るくなる(アルファー)
    //フェードアウトは、暗くなる(アルファ＋)

    [SerializeField] CanvasGroup titleCanvas;
    [SerializeField] CanvasGroup descriptionCanvas1;
    [SerializeField] CanvasGroup descriptionCanvas2;
    [SerializeField] CanvasGroup descriptionCanvas3;
    [SerializeField] CanvasGroup blackCanvas;

    float fadeTimer;
    float timer;
    [SerializeField] float descriptionDisplayDuration = 2;
    [SerializeField] float blackFadeDuration = 1;

    int fadeFase;

    [SerializeField] CanvasGroup clearCanvas;
    [SerializeField] CanvasGroup gameoverCanvas;

    [SerializeField] Image pillar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            switch (fadeFase)
            {
                case 0:
                    {
                        timer += Time.deltaTime;
                        if (timer > 0.5)
                        {
                            FadeIn(blackCanvas, blackFadeDuration);
                        }
                        break;
                    }
                case 1:
                    {
                        timer += Time.deltaTime;
                        if (timer > descriptionDisplayDuration)
                        {
                            FadeIn(descriptionCanvas1, blackFadeDuration);
                        }
                        break;
                    }
                case 2:
                    {
                        timer += Time.deltaTime;
                        if (timer > descriptionDisplayDuration)
                        {
                            FadeIn(descriptionCanvas2, blackFadeDuration);
                        }
                        break;
                    }
                case 3:
                    {
                        timer += Time.deltaTime;
                        if (timer > descriptionDisplayDuration)
                        {
                            FadeOut(blackCanvas, blackFadeDuration);
                        }
                        break;
                    }
                case 4:
                    {
                        descriptionCanvas3.alpha = 0;
                        titleCanvas.alpha = 1;
                        fadeFase++;
                        break;
                    }
                case 5:
                    {
                        FadeIn(blackCanvas, blackFadeDuration);
                        break;
                    }
                case 6:
                    {
                        if (Gamepad.current != null)
                        {
                            if (Gamepad.current.buttonNorth.wasPressedThisFrame == true || Gamepad.current.buttonSouth.wasPressedThisFrame == true
                                || Gamepad.current.buttonEast.wasPressedThisFrame == true || Gamepad.current.buttonWest.wasPressedThisFrame == true)
                            {
                                fadeFase++;
                            }

                        }
                        break;
                    }
                case 7:
                    {
                        FadeOut(blackCanvas, blackFadeDuration);
                        break;
                    }
                case 8:
                    {
                        SceneManager.LoadScene("Game");
                        break;
                    }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            if (TimerScript.Clear == true)
            {
                clearCanvas.alpha = 1;
                pillar.color = new Color(1, 1, 1, 0);
            }
            else if (TimerScript.EndGame == true)
            {
                gameoverCanvas.alpha = 1;
                pillar.color = new Color(1, 1, 1, 1);
            }
        }

    }


    void FadeIn(CanvasGroup canvasGroup, float fadeDuration)
    {
        fadeTimer += Time.deltaTime;

        canvasGroup.alpha = 1 - (fadeTimer / fadeDuration);
        if (fadeTimer > fadeDuration)
        {
            fadeFase++;
            fadeTimer = 0;
            timer = 0;
        }
    }
    void FadeOut(CanvasGroup canvasGroup, float fadeDuration)
    {
        fadeTimer += Time.deltaTime;

        canvasGroup.alpha = (fadeTimer / fadeDuration);

        if (fadeTimer > fadeDuration)
        {
            fadeFase++;
            fadeTimer = 0;
            timer = 0;
        }
    }
}
