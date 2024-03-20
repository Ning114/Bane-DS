using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public int damageAmt;
    public float bulletSpd = 5f;
    public Rigidbody2D theRB;

    private Vector3 trajectory;

    // Start is called before the first frame update
    void Start()
    {
        trajectory = PlayerMovement.instance.transform.position - transform.position;
        trajectory.Normalize();
        trajectory = trajectory * bulletSpd;
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = trajectory * bulletSpd;
        
    }

    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            PlayerMovement.instance.TakeDamage(damageAmt);
            Destroy(gameObject);
        }
        
    }
}
