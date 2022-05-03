using UnityEngine;

public enum ControlScheme
{
    HUB,
    KEYBOARD
}

public class PlayerController : MonoBehaviour
{

    
    [SerializeField] private Transform spaceShip;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float screenXPadding, screenYPadding = 100f;
    [SerializeField] private GameObject hubGameObject;
    [SerializeField] private string engineSoundPath;
    public ControlScheme controlScheme = ControlScheme.KEYBOARD;
    
    private HubBase hubBase;
    private OrientationSensor orientationSensor;
    private ForceSensor forceSensor;
    
    private float maxX, maxY, minX, minY;

    private Vector3 direction;
    [SerializeField]
    private Camera cam;

    public FMOD.Studio.EventInstance engineSoundInstance;
    // Start is called before the first frame update
    void Start()
    {
        
        engineSoundInstance = AudioManager.instance.CreateEventInstance(engineSoundPath);
        engineSoundInstance.start();
        // Hub
        hubBase = HubBase.instance;
        if (hubBase != null && hubBase.IsConnected)
        {
            controlScheme = ControlScheme.HUB;
            orientationSensor = hubBase.GetComponent<OrientationSensor>();
            forceSensor = hubBase.GetComponent<ForceSensor>();
        }
        
        
        // Camera settings
        var zDistanceToCam = Mathf.Abs(spaceShip.transform.position.z - cam.transform.position.z);
        minX = cam.ScreenToWorldPoint(new Vector3(screenXPadding, 0, zDistanceToCam)).x;
        maxX = cam.ScreenToWorldPoint(new Vector3(Screen.width - screenXPadding, 0, zDistanceToCam)).x;
        minY = cam.ScreenToWorldPoint(new Vector3(0, screenYPadding, zDistanceToCam)).y;
        maxY = cam.ScreenToWorldPoint(new Vector3(0, Screen.height - screenYPadding, zDistanceToCam)).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.currentState == GameState.Paused)
            return;
        
        if (controlScheme == ControlScheme.KEYBOARD)
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        }
        else
        {
            var orientation = orientationSensor.Orientation.normalized;
            // Deadzone
            if (!(Mathf.Abs(orientation.z) <= 0.15f && Mathf.Abs(orientation.x) <= 0.15f))
                direction = new Vector3(orientation.x, orientation.z, 0.0f);
            else
            {
                direction = Vector3.zero;
            }
        }

        MoveSpaceship(direction * speed * Time.deltaTime);
        RotateSpaceShip(direction);
    }

    private void RotateSpaceShip(Vector3 direction)
    {
        var maxRot = 30f;
        // Calculate yaw, pitch, and roll based on direction
        var yaw = Mathf.Clamp(direction.x * rotationSpeed, -maxRot, maxRot);
        var pitch = Mathf.Clamp(direction.y * -rotationSpeed, -maxRot, maxRot);
        var roll = Mathf.Clamp(direction.z * rotationSpeed, -maxRot, maxRot);
        
        // Lerp rotate the spaceship
        spaceShip.transform.rotation = Quaternion.Lerp(spaceShip.transform.rotation, Quaternion.Euler(pitch, yaw, roll), Time.deltaTime * rotationSpeed);
        
    }


    public void MoveSpaceship(Vector3 direction)
    {
        // Move spaceship and limit position to screen max and min
        spaceShip.position = new Vector3(
            Mathf.Clamp(spaceShip.position.x + direction.x, minX, maxX),
            Mathf.Clamp(spaceShip.position.y + direction.y, minY, maxY),
            0);
    }

    // private void OnGUI()
    // {
    //     // Set UI style and font size
    //     GUIStyle style = new GUIStyle();
    //     style.fontSize = 20;
    //     style.normal.textColor = Color.white;
    //     
    //     // Set IMGUI style to style object
    //     GUI.skin.label = style;
    //
    //     // Show a textbox with transform position
    //     GUI.Label(new Rect(10, 10, 300, 20), $"Position: {spaceShip.position}");
    //     
    //     //show a textbox with minX, maxX, minY, maxY
    //     GUI.Label(new Rect(10, 30, 300, 20), $"minX: {minX}, maxX: {maxX}, minY: {minY}, maxY: {maxY}");
    //     
    //     // Show a textbox with screen width and height
    //     GUI.Label(new Rect(10, 50, 300, 20), $"Screen width: {Screen.width}, height: {Screen.height}");
    //     
    //     // Show a textbox with mouse position in world space
    //     GUI.Label(new Rect(10, 70, 300, 20), $"Mouse position: {Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
    //     
    //     // show a textbox with direction
    //     GUI.Label(new Rect(10, 90, 300, 20), $"Direction: {direction}");
    //     
    // }


    public void SetControlSchemeToHub(bool connected)
    {
        if(hubBase.IsConnected) 
            controlScheme = ControlScheme.HUB;
        else 
            controlScheme = ControlScheme.KEYBOARD;
    }
    
}
