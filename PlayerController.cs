using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController mainFPSCamera;
    public static Camera mainCamera;

    public bool lockCursor = false;

    public float Sensitivity = 50;
    [HideInInspector] public float mouseX, mouseY;
    private float rotationX, rotationY;
    [HideInInspector] public float aimedZRotation = 0, rotationZ;

    public Transform Player;
    public Transform CameraPosition;

    public static CharacterController cc;
    public static PlayerController mainPlayerMovement;


    [Header("Player Movement")]
    public float Speed = 2.0f;
    [HideInInspector] public float v, h;
    [HideInInspector] public Vector3 velocity, inputVelocity;
    [HideInInspector] public bool isThrusting = false;

    public ParticleSystem hyperSpeed;
    public GameObject Mirror;

    void Awake()
    {
        Application.targetFrameRate = 90;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        mainFPSCamera = this;
        mainCamera = this.GetComponent<Camera>();

        cc = this.GetComponent<CharacterController>();

        mainPlayerMovement = this;
    }
    // Update is called once per frame
    void Update()
    {
        // Input from mouse
        mouseX = Input.GetAxis("Mouse X") * Sensitivity * TimeManager.currentTimeScale;
        mouseY = Input.GetAxis("Mouse Y") * Sensitivity * TimeManager.currentTimeScale;
        // Input from keyboard
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        // Calculating the rotation
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        rotationY += mouseX;
        rotationZ = -45 * h;

        // Rotating
        //rotationZ = Mathf.Lerp(rotationZ, aimedZRotation, Time.deltaTime * 8);
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        Player.Rotate(Vector3.up * mouseX);

        transform.position = CameraPosition.position;

        inputVelocity = (transform.forward * v + transform.right * h) * Speed;
        //t += Time.deltaTime * TimeManager.currentTimeScale;

        cc.Move((velocity + inputVelocity) * Time.deltaTime * TimeManager.currentTimeScale);

        if ((v != 0 || h != 0) && !FindObjectOfType<AudioManager>().isPlaying("Thrusting"))
        {
            FindObjectOfType<AudioManager>().Play("Thrusting");
        }
        // Transitions in and out of hyper speed
        if (Input.GetButtonDown("Jump"))
        {
            hyperSpeed.Play();
            FindObjectOfType<AudioManager>().Play("HyperSpeed");
            Speed += 10.0f;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            Speed -= 10.0f;
            hyperSpeed.Stop();
            hyperSpeed.Clear();
        }
        // Transitions in and out of hyper speed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Mirror.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            Mirror.SetActive(false);
        }
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 initialPosition = transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, initialPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = initialPosition;
    }
}