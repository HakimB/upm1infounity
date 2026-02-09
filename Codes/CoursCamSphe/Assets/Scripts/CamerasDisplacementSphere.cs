using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamerasDisplacementSphere : MonoBehaviour
{


    [SerializeField] private float delta_horizontal = 5f;
    public float delta_vertical = 5f;

    [SerializeField] [Range(0.0001f, 0.8f)] private float speed_camera = 0.1f;

    List<CameraSpherique> cameras = new List<CameraSpherique>();
    
    private Button buttonLeft;
    private Button buttonRight;
    private Button buttonUp;
    private Button buttonDown;

    private Dropdown allCamerasDropdown;

    CameraSpherique currentCamera = null;
    CameraSpherique previousCamera = null;
    float previousDeltaTime = 0.0f;


    void Awake()
    {
        // allCamerasDropdown = GetComponent<Transform>().Find("Cameras").GetComponent<Dropdown>();
        allCamerasDropdown = GameObject.Find("Cameras").GetComponent<Dropdown>();
        // allCamerasDropdown = FindFirstObjectByType<Dropdown>();
        allCamerasDropdown.options.Clear();
        
        print("Finding cameras in the scene...");
        // cameras.AddRange(FindObjectsOfType<Camera>());
        foreach (Camera cam in UnityEngine.Object.FindObjectsByType<Camera>(FindObjectsSortMode.None))
        {
            cameras.Add(new CameraSpherique(cam));
            print("Camera added: " + cam.name);
            allCamerasDropdown.options.Add(new Dropdown.OptionData(cam.name));
        }

        print("Total cameras found: " + cameras.Count);
        int cameraIndex = 0;
        previousCamera = cameras[cameraIndex];
        currentCamera = cameras[cameraIndex];
        previousDeltaTime = 0f;
        
        buttonLeft = GameObject.Find("Left").GetComponent<Button>();
        buttonRight = GameObject.Find("Right").GetComponent<Button>();
        buttonUp = GameObject.Find("Up").GetComponent<Button>();
        buttonDown = GameObject.Find("Down").GetComponent<Button>();

        buttonLeft.onClick.AddListener(OnClickLeft);
        buttonRight.onClick.AddListener(OnClickRight);
        buttonUp.onClick.AddListener(OnClickUp);
        buttonDown.onClick.AddListener(OnClickDown);

        allCamerasDropdown.onValueChanged.AddListener(delegate { OnCameraDropdownChanged(); });
    }

    public void OnCameraDropdownChanged()
    {
        int cameraIndex = allCamerasDropdown.value;
        previousCamera = currentCamera;
        currentCamera = cameras[cameraIndex];
        print("Selected camera index: " + cameraIndex);
        previousCamera.disable(); 
        currentCamera.enable();
        previousDeltaTime = 0f; 
    }

    void Start()
    {
        
    }

    void Update()
    {
        previousDeltaTime += speed_camera;
        CameraSpherique cam = CameraSpherique.Lerp(previousCamera, currentCamera, previousDeltaTime);
        cam.DeplacerCamera();
    }

    public void OnClickLeft()
    {
        previousCamera = (CameraSpherique)currentCamera.Clone();
        previousDeltaTime = 0f;
        MoveLeft(delta_horizontal);
    }
    public void OnClickRight()
    {
        previousCamera = (CameraSpherique)currentCamera.Clone();
        previousDeltaTime = 0f;
        MoveRight(delta_horizontal);
    }

    public void OnClickUp()
    {
        previousCamera = (CameraSpherique)currentCamera.Clone();
        previousDeltaTime = 0f;
        MoveUp(delta_vertical);
    }
    public void OnClickDown()
    {
        previousCamera = (CameraSpherique)currentCamera.Clone();
        previousDeltaTime = 0f;
        MoveDown(delta_vertical);
    }

    public void MoveUp(float delta) { currentCamera.moveUp(delta); }
    public void MoveDown(float delta) { currentCamera.moveDown(delta); }
    public void MoveLeft(float delta) { currentCamera.moveLeft(delta); }
    public void MoveRight(float delta) { currentCamera.moveRight(delta); }
}
