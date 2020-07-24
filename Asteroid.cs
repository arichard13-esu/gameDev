using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject Asteroid1;
    [HideInInspector] public GameObject asteroid1;
    public GameObject Asteroid2;
    [HideInInspector] public GameObject asteroid2;
    public GameObject Asteroid3;
    [HideInInspector] public GameObject asteroid3;

    public ParticleSystem explosion;

    float low = -1000;
    float high = 1000;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Torpedo")
        {
            HUD.instance.ChangeScore(1);
            explode();
        }
    }
    public void explode()
    {
        explosion.transform.position = this.transform.position;
        explosion.Play();

        FindObjectOfType<AudioManager>().Play("Explosion");

        Destroy(this.gameObject);

        asteroid1 = Instantiate(Asteroid1, this.transform.position, this.transform.rotation);
        asteroid1.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(low, high), Random.Range(low, high), Random.Range(low, high)));
        asteroid1.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(low, high), Random.Range(low, high), Random.Range(low, high)));

        asteroid2 = Instantiate(Asteroid2, this.transform.position, this.transform.rotation);
        asteroid2.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(low, high), Random.Range(low, high), Random.Range(low, high)));
        asteroid2.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(low, high), Random.Range(low, high), Random.Range(low, high)));

        asteroid3 = Instantiate(Asteroid3, this.transform.position, this.transform.rotation);
        asteroid3.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(low, high), Random.Range(low, high), Random.Range(low, high)));
        asteroid3.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(low, high), Random.Range(low, high), Random.Range(low, high)));

        //Destroy(asteroid1.gameObject, 2f);
        //Destroy(asteroid2.gameObject, 2f);
        //Destroy(asteroid3.gameObject, 2f);
    }
}