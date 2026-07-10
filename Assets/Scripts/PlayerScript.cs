using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    enum Type
    {
        Left, Right
    };

    [SerializeField] Type type;

    Rigidbody2D p_Rigidbody;
    [SerializeField] float speed = 20;
    [SerializeField] float jumpPower = 10;

    float pressTimeJump;
    bool StayField;
    bool WasJump;
    bool WasAttack;
    float timerAttackCooldown;

    [SerializeField] GameObject attackRenge;

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p_Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", p_Rigidbody.linearVelocityX);

        if (Gamepad.current != null)
        {

            if (!TimerScript.EndGame)
            {
                switch (type)
                {
                    case Type.Left:
                        {
                            break;
                        }
                    case Type.Right:
                        {
                            if (WasAttack)
                            {
                                timerAttackCooldown += Time.deltaTime;
                                if (timerAttackCooldown >= 0.5f)
                                {
                                    WasAttack = false;
                                    animator.SetBool("IsAttack", false);
                                    timerAttackCooldown = 0;
                                }
                            }
                            if (Gamepad.current.buttonNorth.wasPressedThisFrame == true && WasAttack == false)
                            {
                                Attack();
                                AttackScript.buttonType = AttackScript.Button.North;
                            }
                            else if (Gamepad.current.buttonSouth.wasPressedThisFrame == true && WasAttack == false)
                            {
                                Attack();
                                AttackScript.buttonType = AttackScript.Button.South;
                            }
                            else if (Gamepad.current.buttonEast.wasPressedThisFrame == true && WasAttack == false)
                            {
                                Attack();
                                AttackScript.buttonType = AttackScript.Button.East;
                            }
                            else if (Gamepad.current.buttonWest.wasPressedThisFrame == true && WasAttack == false)
                            {
                                Attack();
                                AttackScript.buttonType = AttackScript.Button.West;
                            }

                            break;
                        }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!TimerScript.EndGame)
        {
            if (Gamepad.current != null)
            {
                PlayerMove();

                switch (type)
                {
                    case Type.Left:
                        {
                            Jump();
                            Fall();
                            break;
                        }
                    case Type.Right:
                        {
                            break;
                        }
                }


            }
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        StayField = true;
        if (type == Type.Left)
        {
            if (animator.GetBool("IsJump"))
                animator.SetBool("IsJump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        StayField = false;
        if (type == Type.Left)
        {
            if (!animator.GetBool("IsJump"))
                animator.SetBool("IsJump", true);
        }
    }

    void PlayerMove()
    {
        switch (type)
        {
            case Type.Left:
                {
                    if (Gamepad.current.leftStick.x.value > 0)
                    {
                        MoveRight();
                        transform.rotation = new Quaternion(0, 0, 0, 0);
                    }
                    else if (Gamepad.current.leftStick.x.value < 0)
                    {
                        transform.rotation = new Quaternion(0, 180, 0, 0);
                        MoveLeft();
                    }
                    else
                    {
                        MoveStop();
                    }
                    break;
                }
            case Type.Right:
                {

                    if (Gamepad.current.rightStick.x.value > 0 && !WasAttack)
                    {
                        transform.rotation = new Quaternion(0, 180, 0, 0);
                        MoveRight();
                    }
                    else if (Gamepad.current.rightStick.x.value < 0 && !WasAttack)
                    {
                        transform.rotation = new Quaternion(0, 0, 0, 0);
                        MoveLeft();
                    }
                    else
                    {
                        MoveStop();
                    }
                    break;
                }
        }
    }
    void MoveRight()
    {
        if (StayField == true)
        {
            if (p_Rigidbody.linearVelocityX < 0)
            {
                p_Rigidbody.AddForce(Vector2.right * speed * 2);
            }
            if (p_Rigidbody.linearVelocityX <= speed)
            {
                p_Rigidbody.AddForce(Vector2.right * speed);
            }
        }
    }
    void MoveLeft()
    {
        if (StayField == true)
        {
            if (p_Rigidbody.linearVelocityX > 0)
            {
                p_Rigidbody.AddForce(-Vector2.right * speed * 2);
            }
            if (p_Rigidbody.linearVelocityX >= -speed)
            {
                p_Rigidbody.AddForce(-Vector2.right * speed);
            }
        }
    }
    void MoveStop()
    {
        if (StayField == true)
        {
            if (p_Rigidbody.linearVelocityX > 0)
            {
                p_Rigidbody.AddForce(-Vector2.right * speed);
            }
            else if (p_Rigidbody.linearVelocityX < 0)
            {
                p_Rigidbody.AddForce(Vector2.right * speed);
            }

            if (p_Rigidbody.linearVelocityX > -1 && p_Rigidbody.linearVelocityX < 1)
            {
                p_Rigidbody.AddForceX(-p_Rigidbody.linearVelocityX);
            }
        }
    }
    void Jump()
    {
        if (Gamepad.current.leftShoulder.isPressed == true)
        {
            pressTimeJump += Time.deltaTime;
        }
        else
        {
            if (pressTimeJump < 0.2f && pressTimeJump != 0 && StayField == true && WasJump == false)
            {
                p_Rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                WasJump = true;
            }
            pressTimeJump = 0;
            WasJump = false;
        }
        if (pressTimeJump >= 0.2f && StayField == true && WasJump == false)
        {
            p_Rigidbody.AddForce(Vector2.up * jumpPower * 1.3f, ForceMode2D.Impulse);
            WasJump = true;
        }
    }
    void Fall()
    {
        if (p_Rigidbody.linearVelocityY < 0)
        {
            p_Rigidbody.AddForceY(p_Rigidbody.linearVelocityY * 3);
        }
    }
    void Attack()
    {
        GameObject o = Instantiate(attackRenge);
        o.transform.parent = transform;
        o.transform.localPosition = new Vector3(-4f, 0, 0);
        WasAttack = true;
        animator.SetBool("IsAttack", true);
        Destroy(o, 0.8f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Right Player")
        {
            TimerScript.Clear = true;
            TimerScript.EndGame = true;
        }
    }

}
