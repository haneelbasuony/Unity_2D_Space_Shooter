using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject ExplosionPrefab;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-5.05f, 6.14f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Motion();
    }
    private void Motion()
    {

        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -6.17f)
        {
            transform.position = new Vector3(Random.Range(-7.45f, 7.92f), 6.14f, 0);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(" Enemy Collieded with :" + other.name);
        if (other.name == "Laser")
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            DestroyThatObject();
            Lasers laser = other.GetComponent<Lasers>();
            laser.DestroyThatObject();
        }
        

        
    }
        public void DestroyThatObject()
        {
            Destroy(this.gameObject);
        }
   
}