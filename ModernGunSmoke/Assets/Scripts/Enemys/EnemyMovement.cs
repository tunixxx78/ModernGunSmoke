using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private delegate void MoveDelegate();
    private MoveDelegate methodToCall;

    GameObject plr;
    Rigidbody enemyRigidbody;
    [SerializeField] bool dummyMovementSelected, followerSelected, bossSellected, canMove = true, hasMoved = false;
    [SerializeField] float movementDelay, moveSpeed = 5;
    [SerializeField] Vector3 direction;

    Animator enemyAnim;


    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        plr = GameObject.Find("Player");
        enemyAnim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (dummyMovementSelected)
        {
            methodToCall = DummyMovement;
            DummyMovement();
        }
        if (followerSelected)
        {
            methodToCall = Follower;
            Follower();
        }
        if (bossSellected)
        {
            methodToCall = BossMovement;
            BossMovement();
        }
    }

    private void Update()
    {
        if(followerSelected && canMove)
        {
            enemyRigidbody.AddForce(direction * moveSpeed * Time.deltaTime);
        }

        if(dummyMovementSelected && canMove)
        {
            if (hasMoved == false)
            {
                enemyAnim.SetBool("IsRunning", true);
                enemyRigidbody.AddForce(Vector3.right * (moveSpeed - 2) * Time.deltaTime, ForceMode.Impulse);
            }
            else
            {
                enemyAnim.SetBool("IsRunning", true);
                enemyRigidbody.AddForce(Vector3.left * (moveSpeed - 2) * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }

    private void DummyMovement()
    {
        canMove = true;

        if (hasMoved)
        {
            hasMoved = false;
        }
        else if (hasMoved == false)
        {
            hasMoved = true;
        }

        if (hasMoved == false)
        {
            enemyAnim.SetBool("IsRunning", true);
            enemyRigidbody.AddForce(Vector3.right * moveSpeed * Time.deltaTime);
            
        }
        else
        {
            enemyAnim.SetBool("IsRunning", true);
            enemyRigidbody.AddForce(Vector3.left * moveSpeed * Time.deltaTime);
            
        }
        
        StartCoroutine(MovementDelay());
        
    }

    private void Follower()
    {
        canMove = true;

        var plrPos = plr.transform.position;
        direction = plrPos - this.transform.position;
        enemyRigidbody.AddForce(direction * moveSpeed * Time.deltaTime);

        StartCoroutine(MovementDelay());
        
    }

    private void BossMovement()
    {
        Debug.Log("BossIsAttacking!");
        StartCoroutine(MovementDelay());
    }

    private IEnumerator MovementDelay()
    {

        yield return new WaitForSeconds(movementDelay);

        enemyRigidbody.Sleep();
        canMove = false;
        //moveDelegate();
        //Invoke("moveDelegate", movementDelay + movementDelay);
        enemyAnim.SetBool("IsRunning", false);
        StartCoroutine(RestartMovement(methodToCall));

    }
    private IEnumerator RestartMovement(MoveDelegate moveDelegate)
    {
        yield return new WaitForSeconds(2);

        moveDelegate();

    }
}
