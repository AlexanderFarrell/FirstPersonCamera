using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float Yaw = 0.0f;
    public float Pitch = 0.0f;
    private Vector3 _previousMousePosition;
    private Camera _camera;

    public float MovementSpeed;

    private float _fallSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        _camera = Camera.main;
        Cursor.visible = false;
    }

    void Start()
    {
        _previousMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGravity();
        UpdateRotation();
        UpdateWalk();
        UpdateCamera();
    }

    private void UpdateGravity()
    {
        _fallSpeed += Physics.gravity.y * Time.deltaTime;
        
    }

    private void UpdateWalk()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var unitMovement = new Vector3(x, 0.0f, z);

        var movement = Matrix4x4.Rotate(Quaternion.Euler(0.0f, Yaw, 0.0f)).MultiplyPoint(unitMovement) * MovementSpeed * Time.deltaTime;

        transform.position += movement;
    }

    private void UpdateRotation()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseMovedFromLastUpdate = currentMousePosition - _previousMousePosition;

        Yaw += mouseMovedFromLastUpdate.x;
        Pitch -= mouseMovedFromLastUpdate.y;

        Pitch = Mathf.Min(Pitch, 90.0f);
        Pitch = Mathf.Max(Pitch, -90.0f);
        
        transform.rotation = Quaternion.Euler(0.0f, Yaw, 0.0f);
        
        _previousMousePosition = currentMousePosition;
    }

    private void UpdateCamera()
    {
        _camera.transform.rotation = Quaternion.Euler(Pitch, Yaw, 0.0f);
        _camera.transform.position = transform.position;
    }
}
