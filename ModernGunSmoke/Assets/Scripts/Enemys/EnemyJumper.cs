using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumper : MonoBehaviour
{
    [SerializeField] float jumpForce, gravity = -9.81f, groundDistance = 0.4f;
    [SerializeField] bool canJump, isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    Vector3 velocity;
    Rigidbody enemyRigidbody;
    EnemyBase enemyBase;


    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyBase = GetComponent<EnemyBase>();

    }

    private void Update()
    {
        if (enemyBase.canSeePlr)
        {
            JumpToRoad();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void JumpToRoad()
    {
        if (isGrounded)
        {
            GetComponent<EnemyBase>().getAnimatorActions();
            enemyRigidbody.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            enemyRigidbody.AddForce(Vector3.right * 30 * Time.deltaTime, ForceMode.Impulse);
            enemyRigidbody.AddForce(Vector3.forward * 30 * Time.deltaTime, ForceMode.Impulse);
        }
        
    }
}
