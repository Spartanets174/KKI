using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour
{
    //[SerializeField] private Transform cameraHolder;
    public Camera sceneCamera;
    public Transform target;

    [Range(5f, 15f)]
    //[Tooltip("Ќасколько чувствительно перетаскивание мыши к повороту камеры")]
    public float mouseRotateSpeed = 5f;

    //[Range(10f, 50f)]
    //[Tooltip("Ќасколько чувствительно сенсорное перетаскивание к повороту камеры")]
    //public float touchRotateSpeed = 10f;

    //[Tooltip("ћеньшее положительное значение означает более плавное вращение, 1 означает отсутствие плавного применени€")]
    public float slerpSmoothValue = 0.3f;
    //[Tooltip("—колько времени занимает плавное перемещение прокрутки мыши")]
    public float scrollSmoothTime = 0.12f;
    public float editorFOVSensitivity = 5f;

    //ћожем повернуть камеру, что означает, что не блокируем обзор
    private bool canRotate = true;

    private Vector2 swipeDirection; //swipe delta vector2
    private Quaternion currentRot; // store the quaternion after the slerp operation - “елефон
    private Quaternion targetRot;

    //Mouse rotation related
    private float rotX; // around x
    private float rotY; // around y
    //Mouse Scroll
    private float cameraFieldOfView;
    private float cameraFOVDamp; //Damped value
    private float fovChangeVelocity = 0;

    private float distanceBetweenCameraAndTarget;
    //Clamp Value

    [SerializeField] private float minXRotAngle = -65; //ћинимальный угол ¬ращени€ ’ оси
    [SerializeField] private float maxXRotAngle = 60; //ћаксимальный угол ¬ращени€ ’ оси

    [SerializeField] private float minCameraFieldOfView = 10; //ѕриближение
    [SerializeField] private float maxCameraFieldOfView = 50; //ќтдаление

    Vector3 dir;
    private void Awake()
    {
        GetCameraReference();

    }
    // Start is called before the first frame update
    void Start()
    {
        distanceBetweenCameraAndTarget = Vector3.Distance(sceneCamera.transform.position, target.position);
        //ƒобавить другой вариант???
        dir = new Vector3(3, 3, distanceBetweenCameraAndTarget);//–ассто€ние между основной камерой и целью
        sceneCamera.transform.position = target.position + dir; //”становка позиции камеры

        cameraFOVDamp = sceneCamera.fieldOfView;
        cameraFieldOfView = sceneCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canRotate)
        {
            return;
        }
        //We are in editor
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            EditorCameraInput();
        }
        //“ыкай кнопки, чтобы поставить камеру в другое положение
        if (Input.GetKeyDown(KeyCode.F))
        {
            FrontView();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TopView();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LeftView();
        }

    }

    private void LateUpdate()
    {
        RotateCamera();
        SetCameraFOV();
    }

    public void GetCameraReference()
    {
        if (sceneCamera == null)
        {
            sceneCamera = Camera.main;
        }

    }

    //May be the problem with Euler angles
    public void TopView()
    {
        rotX = -85;
        rotY = 0;
    }

    public void LeftView()
    {
        rotY = 90;
        rotX = 0;
    }

    public void FrontView()
    {
        rotX = 0;
        rotY = 0;
    }

    private void EditorCameraInput()
    {
        //Camera Rotation
        if (Input.GetMouseButton(1))
        {
            rotX += Input.GetAxis("Mouse Y") * mouseRotateSpeed; // around X
            rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;

            if (rotX < minXRotAngle)
            {
                rotX = minXRotAngle;
            }
            else if (rotX > maxXRotAngle)
            {
                rotX = maxXRotAngle;
            }
        }
        //Camera Field Of View
        if (Input.mouseScrollDelta.magnitude > 0)
        {
            cameraFieldOfView += Input.mouseScrollDelta.y * editorFOVSensitivity * -1;//-1 make FOV change natual
        }
    }


    private void RotateCamera()
    {

        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Vector3 tempV = new Vector3(rotX, rotY, 0);
            targetRot = Quaternion.Euler(tempV); //ћы устанавливаем вращение вокруг осей X, Y, Z соответственно
        }
        else
        {
            targetRot = Quaternion.Euler(-swipeDirection.y, swipeDirection.x, 0);
        }
        //Rotate Camera
        currentRot = Quaternion.Slerp(currentRot, targetRot, Time.smoothDeltaTime * slerpSmoothValue * 50);  //пусть значение cameraRot постепенно достигнет нового Q, соответствующего нашему прикосновению
        //”множение кватерниона на вектор3, по сути, заключаетс€ в применении поворота к вектору 3
        //¬ данном случае это все равно, что повернуть палку на рассто€ние между камерой и целью, а затем посмотреть на цель, чтобы повернуть камеру.
        sceneCamera.transform.position = target.position + currentRot * dir;
        sceneCamera.transform.LookAt(target.position);

    }

    //„тобы работало приближение
    void SetCameraFOV()
    {
        //Set Camera Field Of View
        //Clamp Camera FOV value
        if (cameraFieldOfView <= minCameraFieldOfView)
        {
            cameraFieldOfView = minCameraFieldOfView;
        }
        else if (cameraFieldOfView >= maxCameraFieldOfView)
        {
            cameraFieldOfView = maxCameraFieldOfView;
        }

        cameraFOVDamp = Mathf.SmoothDamp(cameraFOVDamp, cameraFieldOfView, ref fovChangeVelocity, scrollSmoothTime);
        sceneCamera.fieldOfView = cameraFOVDamp;

    }
}
