using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Image pillar;
    [SerializeField] bool IsLeft;

    float compositePosition = 4.63f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x) >= compositePosition)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            if (IsLeft)
            {
                transform.position = new Vector3(-compositePosition, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(compositePosition, transform.position.y, transform.position.z);
            }
            pillar.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
