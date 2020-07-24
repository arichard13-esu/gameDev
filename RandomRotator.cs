using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble;
    //public float revolveVelocity = 100;
    /*
    GameObject cube;
    public Transform center;
    public Vector3 axis = Vector3.up;
    public Vector3 desiredPosition;
    public float radius = 2.0f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;
    */
    public float xSpread;
    public float zSpread;

    public float xOffset;
    public float yOffset;
    public float zOffset;

    public Transform centerPoint;
    public float rotSpeed;
    float timer = 0;

    void Start()
    {
        tumble = .25f;
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
        /*
        cube = GameObject.FindWithTag("Cube");
        center = cube.transform;
        transform.position = (transform.position - center.position).normalized * radius + center.position;
        radius = 2.0f;
        */
    }
    void Update()
    {
        /*
        transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        */
        timer += Time.deltaTime * rotSpeed;

        float x = -Mathf.Cos(timer) * xSpread;
        float z = Mathf.Sin(timer) * zSpread;
        Vector3 pos = new Vector3(x + xOffset, yOffset, z + zOffset);
        transform.position = pos + centerPoint.position;

        //transform.LookAt(centerPoint);
    }
}