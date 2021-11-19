using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y>5.6)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collieded with :" + other.name);
        if (other.name == "Enemy")
        {
            DestroyThatObject();
            Enemy laser = other.GetComponent<Enemy>();
            laser.DestroyThatObject();
        }
    }

    public void DestroyThatObject()
    {
        Destroy(this.gameObject);
    }
}

