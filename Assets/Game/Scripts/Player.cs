using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lives = 3;
    public int powerupID = 0;
    [SerializeField]
    public bool canSpeedboost = false;
    public bool canTripleshoot = false;
    [SerializeField]
    private float fireRate = 0.25f;
    [SerializeField]
    private float canFire = 0.0f;
    [SerializeField]
    private float speed = 5.0f;
    public float speedboost = 10.0f;
    [SerializeField]
    private GameObject laserPrefab;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

    }


    void Update()
    {
        Movement();
        shoot();

    }
    
    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (Time.time > canFire)
            {
                if (canTripleshoot == true)
                {
                    Instantiate(laserPrefab, transform.position + new Vector3(0.55f, 0.06f, 0), Quaternion.identity);
                    Instantiate(laserPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                    Instantiate(laserPrefab, transform.position + new Vector3(-0.55f, 0.06f, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(laserPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);

                }
                canFire = Time.time + fireRate;
            }
        }
    }
    private void Movement()
    {
        if (canSpeedboost == false)
        {
            float horizontaInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontaInput);
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);
        }
        if (canSpeedboost == true)
        {
            float horizontaInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * Time.deltaTime * speedboost * horizontaInput);
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * Time.deltaTime * speedboost * verticalInput);
        }
        if (transform.position.y > 4.18)
        {
            transform.position = new Vector3(transform.position.x, 4.18f, 0);
        }
        if (transform.position.x < -8.247418f)
        {
            transform.position = new Vector3(-8.247418f, transform.position.y, 0);
        }
        if (transform.position.x > 8.338574f)
        {
            transform.position = new Vector3(8.338574f, transform.position.y, 0);
        }
        if (transform.position.y < -4.259687f)
        {
            transform.position = new Vector3(transform.position.x, -4.259687f, 0);
        }
    }
    public void TripleshootPowerup_On()
    {
        canTripleshoot = true;
        StartCoroutine(TripleShootPower_OFF());
    }
    public IEnumerator TripleShootPower_OFF()
    {
        yield return new WaitForSeconds(5);
        canTripleshoot = false;
    }
    public void SpeedboostPowerUp_ON()
    {
        canSpeedboost = true;
        StartCoroutine(SpeedboostPowerUp_OFF());
    }
    public IEnumerator SpeedboostPowerUp_OFF()
    {
        yield return new WaitForSeconds(5);
        canSpeedboost = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Collieded with :" + other.name);
        //Damage system
        if (other.name == "Enemy")
        {
            lives--;
            if (lives < 1)
            {
                Destroy(this.gameObject);
            }
        }
        //TripleShootpowerUp
        if (other.name == "TripleShoot_PowerUp")
        {
            PowerUp powerup = other.GetComponent<PowerUp>();
            powerup.DestroyThatObject();
            TripleshootPowerup_On();
        }
        // speed boost =1
        if (other.name == "Speed_PowerUp")
        {
            PowerUp powerup = other.GetComponent<PowerUp>();
            powerup.DestroyThatObject();
            SpeedboostPowerUp_ON();
        }
        //sheild =2
        // if (powerupID == 2)
        // {

        // }
       
    }
    public void DestroyThatObject()
    {
        Destroy(this.gameObject);
    }

}
