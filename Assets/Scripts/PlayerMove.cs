using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private bool isCrouching;
    private bool isRunning;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    [Header("HeadBob")]
    [SerializeField] private float bobSpeed = 14f;
    [SerializeField] private float bobAmount = .05f;
    private float defaultYpos = 0;
    private float Timer;

    private void Awake()
    {
        defaultYpos = Camera.main.transform.localPosition.y;
    }
    private void Update()
    {
        Movement();

    }

    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if ((Input.GetButtonDown("Jump")) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(Mathf.Abs(move.x) >= .5f || Mathf.Abs(move.z) >= .5f)
        {
            HeadBob();
        }
    }

    void HeadBob()
    {
        if (!isGrounded) return;


        Timer += Time.deltaTime * bobSpeed;
        Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, defaultYpos + Mathf.Sin(Timer) * bobAmount, Camera.main.transform.localPosition.z);
    }
}
