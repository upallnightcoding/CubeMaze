using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private Transform gameCamera;
    [SerializeField] private float upperLimit = -40.0f;
    [SerializeField] private float bottomLimit = 70.0f;
    [SerializeField] private float mousesensitivity = 21.9f;

    private Rigidbody playerRigidBody;
    private PlayerInputCntrl playerInputCntrl;
    private Animator animator;
    private int xVelHash;
    private int yVelHash;

    private float animationBlendSpeed = 8.9f;

    private const float walkSpeed = 2.0f;
    private const float runSpeed = 6.0f;

    private float xRotation;

    private Vector2 velocity;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerInputCntrl = GetComponent<PlayerInputCntrl>();

        xVelHash = Animator.StringToHash("xVelocity");
        yVelHash = Animator.StringToHash("yVelocity");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraMovments();
    }

    private void Move()
    {
        float targetSpeed = playerInputCntrl.Run ? runSpeed : walkSpeed;
        if (playerInputCntrl.Move == Vector2.zero) targetSpeed = 0.1f;

        //velocity.x = targetSpeed * playerInputCntrl.Move.x;
        //velocity.y = targetSpeed * playerInputCntrl.Move.y;

        velocity.x = Mathf.Lerp(velocity.x, playerInputCntrl.Move.x * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);
        velocity.y = Mathf.Lerp(velocity.y, playerInputCntrl.Move.y * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);

        float xVelDiff = velocity.x - playerRigidBody.velocity.x;
        float zVelDiff = velocity.y - playerRigidBody.velocity.z;

        Debug.Log($"Diff: ({xVelDiff}/{zVelDiff})");

        playerRigidBody.AddForce(transform.TransformVector(new Vector3(xVelDiff, 0.0f, zVelDiff)), ForceMode.VelocityChange);
        //playerRigidBody.AddForce(new Vector3(xVelDiff, 0.0f, zVelDiff), ForceMode.VelocityChange);

        animator.SetFloat(xVelHash, velocity.x);
        animator.SetFloat(yVelHash, velocity.y);
    }

    private void CameraMovments()
    {
        float mouseX = playerInputCntrl.Look.x;
        float mouseY = playerInputCntrl.Look.y;

        gameCamera.position = cameraRoot.position;

        xRotation -= mouseY * mousesensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, upperLimit, bottomLimit);

        gameCamera.transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        //transform.Rotate(0.0f, mouseX * mousesensitivity * Time.deltaTime, 0.0f);
        transform.Rotate(Vector3.up, mouseX * mousesensitivity * Time.deltaTime);
    }
       
}
