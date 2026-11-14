using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle1Script : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private InputAction horizontalControls;
    [SerializeField] private InputAction verticalControls;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float xValue = 6f;
    [SerializeField] private float minY = -3.995f;
    [SerializeField] private float maxY = 3.995f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 0.3f;
    private float horDirection = 0f;
    private float vertDirection = 0f;
    private Transform _transform;

    void OnEnable()
    {
        horizontalControls.Enable();
        verticalControls.Enable();
    }

    void OnDisable()
    {
        horizontalControls.Disable();
        verticalControls.Disable();
    }

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        horDirection = horizontalControls.ReadValue<float>();
        vertDirection = verticalControls.ReadValue<float>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 newPos = new Vector3 (xValue, _transform.localPosition.y + vertDirection * moveSpeed, 0f);
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
            _transform.SetPositionAndRotation(newPos, 
                                              new Quaternion (0f, 0f, horDirection * rotationSpeed, 1f));
        }
    }
}
