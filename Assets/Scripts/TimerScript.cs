using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    public static int timer = 90;
    [SerializeField] int SFTimer;
    float time;
    TextMeshProUGUI textTimer;

    public static bool Clear;
    public static bool EndGame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = SFTimer;
        textTimer = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Clear)
        {
            time += Time.deltaTime;
            if (time > 1)
            {
                timer--;
                time = 0;
            }
            if (timer % 60 < 10)
            {
                textTimer.text = (timer / 60 + " : 0" + timer % 60);
            }
            else
            {
                textTimer.text = (timer / 60 + " : " + timer % 60);
            }
        }
        if (timer < 0)
        {
            textTimer.text = ("Time Up !");
            EndGame = true;
        }
    }
}
