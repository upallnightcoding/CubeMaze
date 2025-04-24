using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ip
{
    public class RookieCntrl : MonoBehaviour
    {
        [SerializeField] private float animationBlendSpeed = 8.9f;
        [SerializeField] private Transform cameraRoot;
        [SerializeField] private Transform playerCamera;
        [SerializeField] private float upperLimit = -40.0f;
        [SerializeField] private float buttomLimit = 70.0f;
        [SerializeField] private float mouseSensitivity = 21.9f;
        [SerializeField] private float walkSpeed = 1.0f;
        [SerializeField] private float runSpeed = 3.0f;

        private Rigidbody rigidBody;
        private InputCntrl inputCntrl;
        private Animator animator;

        private int xVelocity;
        private int yVelocity;

        private Vector2 currentVelocity;

        private float xRotation;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
            inputCntrl = GetComponent<InputCntrl>();

            xVelocity = Animator.StringToHash("xVelocity");
            yVelocity = Animator.StringToHash("yVelocity");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Move();
        }

        private void LateUpdate()
        {
            CameraMovement();
        }

        private void Move()
        {
            float targetSpeed = inputCntrl.Run ? runSpeed : walkSpeed;
            if (inputCntrl.Move == Vector2.zero) targetSpeed = 0.1f;

            Debug.Log($"Target Speed: {targetSpeed}");

            currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputCntrl.Move.x * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);
            currentVelocity.y = Mathf.Lerp(currentVelocity.y, inputCntrl.Move.y * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);

            float xVelDiff = currentVelocity.x - rigidBody.velocity.x;
            float zVelDiff = currentVelocity.y - rigidBody.velocity.z;

            rigidBody.AddForce(transform.TransformVector(new Vector3(xVelDiff, 0, zVelDiff)), ForceMode.VelocityChange);

            animator.SetFloat(xVelocity, currentVelocity.x);
            animator.SetFloat(yVelocity, currentVelocity.y);
        }

        private void CameraMovement()
        {
            float mouseX = inputCntrl.Look.x;
            float mouseY = inputCntrl.Look.y;

            playerCamera.position = cameraRoot.position;

            xRotation -= mouseY * mouseSensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, upperLimit, buttomLimit);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
            transform.Rotate(Vector3.up, mouseX * mouseSensitivity * Time.deltaTime);
        }
    }
}

