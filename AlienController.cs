using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    //public GameObject player;
    public GameObject Torpedo;
    [HideInInspector] public GameObject torpedo;
    public GameObject bullet;
    public float spawnTime;
    public float spawnDelay;
    public float fireVelocity = 500;

    public Transform centerPoint;

    public Vector3 axis = Vector3.up;
    public Vector3 desiredPosition;
    public float radius;
    public float radiusSpeed;
    public float rotationSpeed;

    public ParticleSystem flash;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", spawnTime, spawnDelay);

        transform.position = (transform.position - centerPoint.position).normalized * radius + centerPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(centerPoint.position, axis, rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - centerPoint.position).normalized * radius + centerPoint.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);

        radius -= Time.deltaTime;

        transform.LookAt(centerPoint);
        transform.Rotate(0f, 0f, radiusSpeed + Time.deltaTime);
    }
    public void Fire()
    {
        torpedo = Instantiate(Torpedo, bullet.transform.position, bullet.transform.rotation);
        torpedo.GetComponent<Rigidbody>().AddForce(torpedo.transform.forward * fireVelocity);
        flash.Play();

        FindObjectOfType<AudioManager>().Play("AlienFire");
        Destroy(torpedo.gameObject, 5f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Torpedo")
        {
            explosion.transform.position = this.transform.position;
            explosion.Play();

            FindObjectOfType<AudioManager>().Play("Explosion");

            Destroy(this.gameObject);
        }
    }
}
