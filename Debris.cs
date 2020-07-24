using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public ParticleSystem explosion;

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
    }
}
