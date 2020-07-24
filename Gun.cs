using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    public GameObject Torpedo;
    [HideInInspector] public GameObject torpedo;
    public GameObject bullet;

    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject part5;
    public GameObject part6;
    public GameObject part7;

    public GameObject shieldImage;

    [HideInInspector] public bool Dead = false;
    [HideInInspector] public int health;
    [HideInInspector] public int heartValue = 1;
    [HideInInspector] public int shield;
    [HideInInspector] public int shieldValue = 1;
    [HideInInspector] public bool shieldActive = true;

    public bool isImmune = false;
    public float immunityDuration = 1.5f;
    private float immunityTime = 0f;
    private float flickDuration = 0.1f;
    private float flickTime = 5f;

    public float fireVelocity = 1000;

    public Camera fpsCam;
    //public PlayerController cameraShake;

    public ParticleSystem flash;

    private Vector3 InitialPosition;

    // Update is called once per frame
    void Update()
    {
        health = GlobalStats.health;
        shield = GlobalStats.shield;

        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
        if (this.isImmune == true) {
            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;
            if (immunityTime >= immunityDuration) {
                this.isImmune = false;
                part1.GetComponent<MeshRenderer>().enabled = true;
                part2.GetComponent<MeshRenderer>().enabled = true;
                part3.GetComponent<MeshRenderer>().enabled = true;
                part4.GetComponent<MeshRenderer>().enabled = true;
                part5.GetComponent<MeshRenderer>().enabled = true;
                part6.GetComponent<MeshRenderer>().enabled = true;
                part7.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        if (!shieldActive || shield == 0) {
            shieldImage.gameObject.SetActive(false);
        }
    }
    public void Fire()
    {
        torpedo = Instantiate(Torpedo, bullet.transform.position, bullet.transform.rotation);
        torpedo.GetComponent<Rigidbody>().AddForce(torpedo.transform.forward * fireVelocity);
        flash.Play();
        Recoil();
        FindObjectOfType<AudioManager>().Play("Firing");
        Destroy(torpedo.gameObject, 5f);
    }
    public void Recoil()
    {
        InitialPosition = transform.localPosition;
        transform.localPosition = transform.localPosition + Vector3.back * .05f;
        StartCoroutine(IERecoil(.05f));
    }
    private IEnumerator IERecoil(float RecoilTime)
    {
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
        do {
            RecoilTime -= Time.deltaTime;
            yield return wfeof;
        } while (RecoilTime > 0.0f);
        transform.localPosition = InitialPosition;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "AlienTorpedo") {
            TakeDamage(1, true);
        }
        else if (other.gameObject.tag == "Energy") {
            HUD.instance.RechargeShield();
            shield = 3;
            shieldImage.gameObject.SetActive(true);
            shieldActive = true;
        }
    }
    public void TakeDamage(int damage, bool playHitReaction)
    {
        if (this.isImmune == false && Dead == false) {
            shield--;

            if (shield >= 0) {
                HUD.instance.SubtractShield(1);
            }
            if (shield < 0) {
                HUD.instance.SubtractHealth(damage); // subtract from global health
                health--;
            }
            if (this.health <= 0) {
                PlayerIsDead();
            }
            else if (playHitReaction == true) {
                PlayHitReaction();
            }
        }
    }
    void PlayHitReaction()
    {
        this.isImmune = true;
        this.immunityTime = 0f;
        FindObjectOfType<AudioManager>().Play("Collision");
        //StartCoroutine(cameraShake.Shake(.15f, .1f));
    }
    void SpriteFlicker()
    {
        if (this.flickTime < this.flickDuration) {
            this.flickTime = this.flickTime + Time.deltaTime;
        }
        else if (this.flickTime >= this.flickDuration) {
            part1.GetComponent<MeshRenderer>().enabled = !(part1.GetComponent<MeshRenderer>().enabled);
            part2.GetComponent<MeshRenderer>().enabled = !(part2.GetComponent<MeshRenderer>().enabled);
            part3.GetComponent<MeshRenderer>().enabled = !(part3.GetComponent<MeshRenderer>().enabled);
            part4.GetComponent<MeshRenderer>().enabled = !(part4.GetComponent<MeshRenderer>().enabled);
            part5.GetComponent<MeshRenderer>().enabled = !(part5.GetComponent<MeshRenderer>().enabled);
            part6.GetComponent<MeshRenderer>().enabled = !(part6.GetComponent<MeshRenderer>().enabled);
            part7.GetComponent<MeshRenderer>().enabled = !(part7.GetComponent<MeshRenderer>().enabled);
            this.flickTime = 0;
        }
    }
    public void PlayerIsDead()
    {
        this.Dead = true;

        this.isImmune = true;
        this.immunityTime = 0f;

        PlayerPrefs.SetInt("Current Score", 0);
        SceneManager.LoadScene("Game Over");
    }
}