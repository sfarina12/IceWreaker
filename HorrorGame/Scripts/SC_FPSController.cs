using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    [Header("Movement settings")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float crouchingSpeed = 3.75f;
    public float jumpSpeed = 8.0f;
    [Space]
    public float gravity = 20.0f;

    [Space,Header("Look settings")]
    public GameObject playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Header("Audio settings")]
    public audioPlayer audioWalking;
    public audioPlayer audioRunning;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    [HideInInspector]
    public float movementDirectionY;
    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isCrouch = Input.GetKey(KeyCode.LeftControl);

        float curSpeedX = 0f;
        float curSpeedY = 0f;
        if (canMove)
        {
            if (isRunning && !isCrouch)
            {
                curSpeedX = runningSpeed * Input.GetAxis("Vertical");
                curSpeedY = runningSpeed * Input.GetAxis("Horizontal");
            }
            else if (isCrouch)
            {
                curSpeedX = crouchingSpeed * Input.GetAxis("Vertical");
                curSpeedY = crouchingSpeed * Input.GetAxis("Horizontal");
            }
            else
            {
                curSpeedX = walkingSpeed * Input.GetAxis("Vertical");
                curSpeedY = walkingSpeed * Input.GetAxis("Horizontal");
            }
        

            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (moveDirection == Vector3.zero) { audioWalking.stopAudio(); audioRunning.stopAudio(); }
            else if (isRunning) { audioRunning.playAudio(); audioWalking.stopAudio(); }
            else { audioWalking.playAudio(); audioRunning.stopAudio(); }


            if (Input.GetKeyDown("space") && canMove && characterController.isGrounded)
                moveDirection.y = jumpSpeed;
            else
                moveDirection.y = movementDirectionY;

            if (!characterController.isGrounded)
                moveDirection.y -= gravity * Time.deltaTime;       

            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }
}