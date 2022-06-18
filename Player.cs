using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public Animator animator;
    public Transform player;
    [SerializeField] GameObject inventory;

    float inputX;
    float inputY;
    float maxSpeed = 2;

    Vector3 stickDirection;
    public float damp;
    float normalFov;
    public float sprintFov;
    public float aimFov;

    [Range(1, 20)]
    public float turnSpeed;
    [Range(1, 20)]
    public float strafeTurnSpeed;

    Camera mainCamera;


    //

    //
    public enum MovementType
    {
        Directional,
        inArms,
        BeginStrafe
    }
    public MovementType movementType;

    private void Awake()
    {
        //
       // inventory = new InventoryNew();
        //uiInventory.SetInventory(inventory);
        //
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        mainCamera = Camera.main;
        normalFov = mainCamera.fieldOfView;
    }

    private void Update()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), strafeTurnSpeed * Time.deltaTime);
        switch (movementType)
        {
            case MovementType.Directional:
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFov, Time.deltaTime * 2);
                DirectionalMove();
                break;
            case MovementType.inArms:
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFov, Time.deltaTime * 2);
                DirectionalMove();
                break;
            case MovementType.BeginStrafe:
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, aimFov, Time.deltaTime * 2);

                BeginStrafe();
                break;
        }
    }

    void DirectionalMove()
    {
        InputMove();
        InputRotation();

        stickDirection = new Vector3(inputX, 0, inputY);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, sprintFov, Time.deltaTime * 2);
            maxSpeed = 2f;
            InputAxis(2f);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFov, Time.deltaTime * 2);
            maxSpeed = 0.2f;
            InputAxis(1f);
        }
        else
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFov, Time.deltaTime * 2);
            maxSpeed = 1f;
            InputAxis(1f);
        }
    }
    void BeginStrafe()
    {
        InputAxis(1f);
        animator.SetFloat("iX", inputX);
        animator.SetFloat("iY", inputY);

        var moving = inputX != 0 || inputY != 0;
        if (moving)
        {
            float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), strafeTurnSpeed * Time.deltaTime);
        }
    }
    void InputAxis(float value)
    {
        inputX = value * Input.GetAxis("Horizontal");
        inputY = value * Input.GetAxis("Vertical");
    }
    void InputMove()
    {
        animator.SetFloat("speed", Vector3.ClampMagnitude(stickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }
    void InputRotation()
    {
        Vector3 rootOffset = mainCamera.transform.TransformDirection(stickDirection);
        rootOffset.y = 0;
        player.forward = Vector3.Slerp(player.forward, rootOffset, Time.deltaTime * turnSpeed);
    }
}
