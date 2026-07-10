using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public enum Button
    {
        North, South, East, West
    }

    public static Button buttonType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {        
        switch (buttonType)
        {
            case Button.North:
                {
                    if (collision.gameObject.tag == "Y")
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                }
            case Button.South:
                {
                    if (collision.gameObject.tag == "A")
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                }
            case Button.East:
                {
                    if (collision.gameObject.tag == "B")
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                }
            case Button.West:
                {
                    if (collision.gameObject.tag == "X")
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                }
        }
    }
}
