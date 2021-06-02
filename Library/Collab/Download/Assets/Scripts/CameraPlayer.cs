using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private GameObject cameraRotater;
    [SerializeField]
    private GameObject cameraHolder;
    [SerializeField]
    private float rotationSpeed = 30f;
    private float minPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        CustomGrid grid = FindObjectOfType<CustomGrid>();
        this.minPos = grid.GetOffset();
        this.transform.position = new Vector3(0, 0, -200);
        this.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.CalculateMovement();
        this.CalculateOrientation();
        this.CalculateZoom();
    }

    private void CalculateMovement()
    {
        float horizontalInput = 0;


        if (Input.mousePosition.x == 0 && !Input.GetButton("MouseMiddleButton"))
        {
            horizontalInput = -1;
        }
        else if (Input.mousePosition.x == Screen.width && !Input.GetButton("MouseMiddleButton"))
        {
            horizontalInput = 1;
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        transform.Translate(Vector3.right * horizontalInput * this.speed * Time.deltaTime);

        float verticalInput = 0;

        if (Input.mousePosition.y == 0 && !Input.GetButton("MouseMiddleButton"))
        {
            verticalInput = -1;
        }
        else if (Input.mousePosition.y == Screen.height - 1 && !Input.GetButton("MouseMiddleButton"))
        {
            verticalInput = 1;
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            verticalInput = Input.GetAxis("Vertical");
        }

        transform.Translate(Vector3.forward * verticalInput * this.speed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, this.minPos, this.minPos * -1), transform.position.y, Mathf.Clamp(transform.position.z, this.minPos, this.minPos * -1));

    }

    private void CalculateOrientation()
    {
        if (Input.GetButton("MouseMiddleButton"))
        {
            float horizontalRotation = Input.GetAxis("Mouse X");
            float verticalRotation = Input.GetAxis("Mouse Y");
            this.transform.Rotate(Vector3.up * horizontalRotation * this.rotationSpeed * Time.deltaTime, Space.Self);
            this.cameraRotater.transform.Rotate(Vector3.left, verticalRotation * this.rotationSpeed * Time.deltaTime);
        }
        float minRotation = 5;
        float maxRotation = 88;
        Vector3 currentRotation = this.cameraRotater.transform.localRotation.eulerAngles;
        currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
        currentRotation.y = 0;
        currentRotation.z = 0;
        this.cameraRotater.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    private void CalculateZoom()
    {
        float zoomDirection = 0;

        zoomDirection = Input.GetAxis("Mouse ScrollWheel");

        this.cameraHolder.transform.Translate(Vector3.forward * zoomDirection * this.speed * Time.deltaTime);

        this.cameraHolder.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(this.cameraHolder.transform.localPosition.z, -100, -5));

    }
}