using UnityEngine;

public class FLStyleCameraController : MonoBehaviour
{
    #region Variables

    ////////////////////////////////////////////////
    // Freelancer Style Camera Variables
    ////////////////////////////////////////////////

    // Objects needed
    private Transform mTransform = null;
    public GameObject spaceship = null;

    // Track the cursor
    private Vector2 mousePosition = Vector2.zero;
    private float x_percentageFromScreenCenter = 0.0f;
    private float y_percentageFromScreenCenter = 0.0f;

    // Rotation variables
    public float maximumYawAngle = 10f;
    public float maximumRollAngle = 20f;
    public float maximumPitchAngle = 8f;
    public float rotationAcceleration = 85f;

    // Sway camera left and right / up and down vars
    private Vector3 cameraVelocity = Vector3.zero;
    private float currentLateralDistance = 0.0f;
    private float currentMedialDistance = 0.0f;
    public float swaySmoothTime = 0.60f;
    public float verticalSwayExtent = 3.5f;
    public float horizontalSwayExtent = 5f;
    public float verticalOffset = 2.5f;
    public float minimumMedialDistance = -2.5f;
    public float maximumMedialDistance = 3.0f;

    //Spin player vars
    private float currentSpinSpeed = 0.0f;
    public float spinAcceleration = 1f;
    public float maximumSpinSpeed = 1f;

    // Enable This Camera
    public bool isEnabled = true;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Unity Mono Start Method
    /// </summary>
    public void Start()
    {
        mTransform = this.transform;
    }

    /// <summary>
    /// Unity Mono Update Method
    /// </summary>
    public void Update()
    {
        // Debug - Used to freeze the camera to allow seeing the rotation of the ship object easier.
        if (Input.GetKeyDown(KeyCode.Space))
            isEnabled = !isEnabled;

        mousePosition = MouseInput.CalculateMousePositionOnScreen();
        CalcCursorIntoLinearConstraint();
        DoSpinShip();
        DoBankShip();
        DoRotateShip();
    }

    /// <summary>
    /// Unity Mono LateUpdateMethod.
    /// After alll movements have been calculated the camera needs to be updated.
    /// </summary>
    public void LateUpdate()
    {
        if (!isEnabled)
            return;

        DoSwayCamera();
    }

    #endregion

    #region Class Methods

    /// <summary>
    /// Calculate Cursor Into Linear Constraint
    /// Params: NONE
    /// Description: Takes the mouse cursors position on screen and calculates the linear distances across the screen
    /// the camera is able to move.
    /// Returns: NONE
    /// </summary>
    private void CalcCursorIntoLinearConstraint()
    {
        // Calculate cursor distance over the screen
        x_percentageFromScreenCenter = mousePosition.x / (Screen.width / 2);
        y_percentageFromScreenCenter = mousePosition.y / (Screen.height / 2);
        currentLateralDistance = (x_percentageFromScreenCenter * horizontalSwayExtent);
        currentMedialDistance = (y_percentageFromScreenCenter * verticalSwayExtent) + verticalOffset;
        currentMedialDistance = Mathf.Clamp(currentMedialDistance, minimumMedialDistance, maximumMedialDistance);
    }

    /// <summary>
    /// Bank Ship.
    /// Params: NONE
    /// Description: This will bank the ship object model based on how far from the center of the screen 
    /// the ship is along each axis.
    /// Returns: NONE
    /// </summary>
    private void DoBankShip()
    {
        float currentShipYRotation = x_percentageFromScreenCenter * maximumYawAngle;
        float currentShipZRotation = x_percentageFromScreenCenter * maximumRollAngle;
        float currentShipXRotation = y_percentageFromScreenCenter * maximumPitchAngle;
        spaceship.transform.localRotation = Quaternion.Euler(-currentShipXRotation, currentShipYRotation, -currentShipZRotation);
    }

    /// <summary>
    /// Spin Ship.
    /// Params: NONE
    /// Description: This will spin (AKA roll) the entire ship and camera together.
    /// Returns: NONE
    /// </summary>
    private void DoSpinShip()
    {
        // Spin the ship
        if (Input.GetKey(KeyCode.Q))
        {
            currentSpinSpeed += spinAcceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentSpinSpeed -= spinAcceleration * Time.deltaTime;
        }
        else
        {
            if (currentSpinSpeed > 0)
            {
                currentSpinSpeed -= spinAcceleration * Time.deltaTime;
                currentSpinSpeed = Mathf.Clamp(currentSpinSpeed, 0f, maximumSpinSpeed);
            }
            else if (currentSpinSpeed < 0)
            {
                currentSpinSpeed += spinAcceleration * Time.deltaTime;
                currentSpinSpeed = Mathf.Clamp(currentSpinSpeed, -maximumSpinSpeed, 0f);
            }
        }
        currentSpinSpeed = Mathf.Clamp(currentSpinSpeed, -maximumSpinSpeed, maximumSpinSpeed);
    }

    /// <summary>
    /// Rotate Ship.
    /// Params: NONE
    /// Description: This will rotate the entire object tree the ship and camera are attached to.
    /// The speed is based on distance from the center of the screen and is also controlled by an acceleration value.
    /// Returns: NONE
    /// </summary>
    private void DoRotateShip()
    {
        // Rotate the player
        float yRotation = (x_percentageFromScreenCenter * rotationAcceleration) * Time.deltaTime;
        float xRotation = (y_percentageFromScreenCenter * rotationAcceleration) * Time.deltaTime;
        mTransform.rotation = mTransform.rotation * Quaternion.Euler(-xRotation, yRotation, currentSpinSpeed);
    }

    /// <summary>
    /// Sway Camera.
    /// Params: NONE
    /// Description: This will sway the camera using the distance of the mouse from the screen center.
    /// Returns: NONE
    /// </summary>
    private void DoSwayCamera()
    {
        // Sway the camera left and right / up and down
        Vector3 to = new Vector3(currentLateralDistance, currentMedialDistance, Camera.main.transform.localPosition.z);
        Camera.main.transform.localPosition = Vector3.SmoothDamp(Camera.main.transform.localPosition, to, ref cameraVelocity, swaySmoothTime);
    }

    #endregion
}
