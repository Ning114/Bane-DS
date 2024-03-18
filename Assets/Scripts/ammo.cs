using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour
{

    public int ammoAmount = 25;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("\nDetected collision with ammoBox");
        if (other.tag == "Player") {
            // Debug.Log("\nDetected Player, consuming ammo!");
            PlayerMovement.instance.currentAmmo += ammoAmount;
            Destroy(gameObject);
        }
    }

}
