using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private GameObject cameraRotationer;
    [SerializeField]
    private GameObject cameraHolder;
    [SerializeField]
    private float rotationSpeed = 0.5f;
    [SerializeField]
    private float zoomSpeed = 30f;
    [SerializeField]
    private float zoomTime = 0.1f;
    [SerializeField]
    private float mouseMoveDivider = 50f;
    private float minPos = 0;
    private readonly float minZoom = -100;
    private readonly float maxZoom = -5;
    private float minZoomSpeed = 3f;
    private float maxZoomSpeed = 30f;
    private float minSpeed = 10f;
    private float maxSpeed = 100f;
    private float targetZoom;
    private float zoomVelo = 0f;
    private bool isStillPressed = false;
    private float originY = 0;
    private float originX = 0;

    void Start()
    {
        CustomGrid grid = FindObjectOfType<CustomGrid>();
        minPos = grid.GetOffsetX();
        transform.position = new Vector3(0, 1.2f, -50);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        targetZoom = cameraHolder.transform.localPosition.z;

        minZoomSpeed = zoomSpeed / 10f;
        maxZoomSpeed = zoomSpeed;

        minSpeed = speed / 5;
        maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateOrientation();
        CalculateZoom();
    }

    private void MoveCamera(float horizontalInput, float verticalInput)
    {
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos, minPos * -1), transform.position.y, Mathf.Clamp(transform.position.z, minPos, minPos * -1));
    }

    public bool MoveCameraWithMouse()
    {
        float horizontalInput = 0;
        float verticalInput = 0;

        if (!isStillPressed)
        {
            isStillPressed = true;
            originX = Input.mousePosition.x;
            originY = Input.mousePosition.y;
        }
        else if (isStillPressed)
        {
            verticalInput = (Input.mousePosition.y - originY) / mouseMoveDivider;
            horizontalInput = (Input.mousePosition.x - originX) / mouseMoveDivider;
        }

        MoveCamera(horizontalInput, verticalInput);

        if (verticalInput > 0 || verticalInput < -0 || horizontalInput > 0 || horizontalInput < -0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveCameraWithKeysOrRotate()
    {
        float horizontalInput = 0;
        float verticalInput = 0;

        if (isStillPressed)
        {
            isStillPressed = false;
            originX = 0;
            originY = 0;
        }

        if (Input.mousePosition.x == 0 && !Input.GetButton("Camera Rotation"))
        {
            horizontalInput = -1;
        }
        else if (Input.mousePosition.x == Screen.width && !Input.GetButton("Camera Rotation"))
        {
            horizontalInput = 1;
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        if (Input.mousePosition.y == 0 && !Input.GetButton("Camera Rotation") && Input.mousePosition.x <= Screen.width)
        {
            verticalInput = -1;
        }
        else if (Input.mousePosition.y == Screen.height - 1 && !Input.GetButton("Camera Rotation") && Input.mousePosition.x <= Screen.width)
        {
            verticalInput = 1;
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            verticalInput = Input.GetAxis("Vertical");
        }

        MoveCamera(horizontalInput, verticalInput);
    }

    private void CalculateOrientation()
    {
        if (Input.GetButton("Camera Rotation"))
        {
            float horizontalRotation = Input.GetAxis("Mouse X");
            float verticalRotation = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.up, horizontalRotation * rotationSpeed);
            cameraRotationer.transform.Rotate(Vector3.left, verticalRotation * rotationSpeed);
        }
        float minRotation = 5;
        float maxRotation = 88;
        Vector3 currentRotation = cameraRotationer.transform.localRotation.eulerAngles;
        currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
        currentRotation.y = 0;
        currentRotation.z = 0;
        cameraRotationer.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    private void CalculateZoom()
    {
        Vector3 newCamPos = cameraHolder.transform.localPosition;
        float zoomDirection = Input.GetAxis("Camera Zoom");
        targetZoom += zoomDirection * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

        if (zoomDirection != 0)
        {
            if (zoomDirection > 0)
            {
                zoomSpeed = (zoomSpeed - minZoomSpeed) < minZoomSpeed ? minZoomSpeed : zoomSpeed - minZoomSpeed;

                if (zoomSpeed < maxZoomSpeed / 2)
                    speed = (speed - minSpeed) < minSpeed ? minSpeed : speed - minSpeed;
            }
            else if (zoomDirection < 0)
            {
                zoomSpeed = (zoomSpeed + minZoomSpeed) > maxZoomSpeed ? maxZoomSpeed : zoomSpeed + minZoomSpeed;
                speed = (speed + minSpeed) > maxSpeed ? maxSpeed : speed + minSpeed;
            }
        }
        newCamPos.z = Mathf.SmoothDamp(cameraHolder.transform.localPosition.z, targetZoom, ref zoomVelo, zoomTime);

        cameraHolder.transform.localPosition = newCamPos;
    }
}