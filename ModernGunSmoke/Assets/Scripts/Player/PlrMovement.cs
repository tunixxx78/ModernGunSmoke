using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrMovement : MonoBehaviour
{
    GameManager gameManager;
    PlrShoot plrShoot;
    Rigidbody plrRb;
    [SerializeField] float plrSpeed = 20f, plrBaseSpeed = 10f;
    Vector3 direction;
    [SerializeField] int health = 10;
    public bool onTheWay;

    Animator plrAnimator;
    [SerializeField] GameObject plrAvatar;

    SFXHandler sFXHandler;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        plrRb = GetComponent<Rigidbody>();
        plrShoot = GetComponent<PlrShoot>();

        gameManager.plrHealth = health;

        plrAnimator = GetComponentInChildren<Animator>();

        sFXHandler = FindObjectOfType<SFXHandler>();
    }

    private void Start()
    {
        onTheWay = true;
    }

    private void Update()
    {
        if (onTheWay)
        {
            transform.Translate(Vector3.forward * plrBaseSpeed * Time.deltaTime);
            plrAnimator.SetBool("IsRunning", true);
        }
        else
        {
            plrAnimator.SetBool("IsRunning", false);
        }
        

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            movement();
        }
        else { if (onTheWay == false) { plrAnimator.SetBool("IsRunning", false); } }

        if (Input.GetKey(KeyCode.A))
        {
            plrAvatar.transform.localRotation = Quaternion.Euler(0, -20, 0);
        }
        //else { plrAvatar.transform.localRotation = Quaternion.Euler(0, 15, 0); }

        else if (Input.GetKey(KeyCode.D))
        {
            plrAvatar.transform.localRotation = Quaternion.Euler(0, 50, 0);
        }
        else { plrAvatar.transform.localRotation = Quaternion.Euler(0, 15, 0); }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            plrAnimator.SetTrigger("Jump");
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("NextBlockCollider"))
        {
            Destroy(collider.gameObject);
            Debug.Log("OSUMAA TULEE");
            gameManager.SpawnNextBlock();
            
        }
        if (collider.CompareTag("AmmoCollectible"))
        {
            gameManager.plrAmmoCount = gameManager.plrAmmoCount + collider.GetComponent<AmmoCollectible>().AmmoAmount;
            GetComponent<PlrShoot>().ammoCount = GetComponent<PlrShoot>().ammoCount + collider.GetComponent<AmmoCollectible>().AmmoAmount;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Mover")
        {
            Debug.Log("Osuma moveriin!");
        }
        if(collision.collider.tag == "EnemyBullet")
        {
            health--;
            sFXHandler.getHit.Play();
            gameManager.plrHealth = health;
            if(health <= 0)
            {
                gameManager.ShowGameOverPanel();
                sFXHandler.dying.Play();
                Destroy(this.gameObject, 1f);
            }
            
        }
      
    }

    private void movement()
    {
        plrRb.AddForce(direction * plrSpeed * Time.deltaTime, ForceMode.Impulse);
        if(onTheWay == false)
        {
            plrAnimator.SetBool("IsRunning", true);
        }
        
    }
}
