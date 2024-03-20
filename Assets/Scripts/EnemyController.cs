using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health = 3;
    public GameObject explosion;

    public float playerRange = 5f;

    public Rigidbody2D theRB;
    public float moveSpeed;
    // Start is called before the first frame update

    public bool shouldShoot;
    public float fireRate = .5f;

    private float shotCounter;
    public GameObject bullet;

    public Transform fireOrigin;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < playerRange)
        {
            // move in direction of player if we are close enough.
            Debug.Log("\nMoving at Player");
            Vector3 playerDirection = PlayerMovement.instance.transform.position - transform.position;
            theRB.velocity = playerDirection.normalized * moveSpeed;

            if (shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    Instantiate(bullet, fireOrigin.position, fireOrigin.rotation);
                    shotCounter = fireRate;
                }
            }
        }
        else
        {
            Debug.Log("\nStopping Enemy movement");
            theRB.velocity = Vector2.zero;
        }

    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
