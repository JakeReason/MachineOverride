using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform model;

    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float turnSpeed = 360;
    private Vector3 input;
    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        Vector2 stickValue = Gamepad.current.leftStick.ReadValue();
        input = new Vector3(stickValue.x, 0, stickValue.y);
    }


    private void Move()
    {
        rb.MovePosition(transform.position + input.ToIso() * input.normalized.magnitude * playerSpeed * Time.deltaTime);
    }
    private void Look()
    {
        if (input == Vector3.zero) return;

        Quaternion rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);
        model.rotation = Quaternion.RotateTowards(model.rotation, rot, turnSpeed * Time.deltaTime);
    }
}

public static class Helpers
{
    private static Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => isoMatrix.MultiplyPoint3x4(input);
}
