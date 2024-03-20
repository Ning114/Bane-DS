using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;


    public Rigidbody2D playerRigidBody;

    public float moveSpeed = 5f;




    //Vector for player movement (wasd)
    private Vector2 moveInput;

    //Vector for the player moving the mouse and looking around (mouse movement)
    private Vector2 mouseInput;
    public float mouseSens = 1f;

    public Camera playerCam;

    public GameObject bulletImpact;

    public Animator gunAnim;
    public int currentAmmo;
    // Start is called before the first frame update

    public int currHealth;
    public int maxHealth;
    public GameObject deadScreen;
    private bool hasDied;

    public Text healthText, ammoText;

    private void Awake()
    {

        instance = this;
    }

    void Start()
    {
        currHealth = maxHealth;
        updateHealthText();
        updateAmmoText();

    }

    private void updateHealthText()
    {

        if (currHealth <= 0) {
            healthText.text = "0%";
        } else {
            healthText.text = currHealth.ToString() + "%";
        }
    }

    private void updateAmmoText()
    {
        ammoText.text = currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDied)
        {


            // wasd movement 
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            // fixes horizontal movement to move horizontally relative to where we are facing
            Vector3 moveHorizontal = transform.up * -moveInput.x;
            // fixes vertical movement to move horizontally relative to where we are facing
            Vector3 moveVertical = transform.right * moveInput.y;
            playerRigidBody.velocity = (moveHorizontal + moveVertical) * moveSpeed;


            // camera movement
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSens;

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                transform.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);

            playerCam.transform.localRotation = Quaternion.Euler(playerCam.transform.localRotation.eulerAngles +
                                                       new Vector3(0f, mouseInput.y, 0f));


            // shooting

            if (Input.GetMouseButtonDown(0))
            {
                if (currentAmmo > 0)
                {
                    Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                    RaycastHit hit;

                    // if we recieve info outputted into hit, then something was hit.
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Debug.Log("\nI'm looking at " + hit.transform.name);
                        Instantiate(bulletImpact, hit.point, transform.rotation);

                        if (hit.transform.tag == "Enemy")
                        {
                            Debug.Log(hit.transform.parent);
                            hit.transform.GetComponent<EnemyController>().TakeDamage();
                        }

                    }
                    else
                    {
                        // Debug.Log("\nI'm looking at nothing");
                    }
                    currentAmmo--;
                    updateAmmoText();
                    gunAnim.SetTrigger("Shoot");
                }
            }

        }
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        updateHealthText();

        if (currHealth <= 0)
        {
            deadScreen.SetActive(true);
            hasDied = true;
        }

    }

    public void AddHealth(int amount)
    {
        currHealth += amount;
        if (currHealth > maxHealth) currHealth = maxHealth;
        updateHealthText();
    }

    public void AddAmmo(int amount) {
        currentAmmo += amount;
        updateAmmoText();
    }


}
