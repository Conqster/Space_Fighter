using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Range(0f, 15f)]
    [SerializeField] private float moveSpeed = 5f, xSpeed = 5f, ySpeed = 1f;

    [SerializeField]private Vector2 moveDir;
    private Rigidbody2D Rb;

    //[Range(1,10)]
    //[SerializeField] private float health;

    //[HideInInspector] public float healthSys;  


    private Animator _anim;
    int shootingHash, movingHash;


    private void Start()
    {
        moveDir = new Vector2(-1f, -1f);
        Rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        shootingHash = Animator.StringToHash("shooting");
        movingHash = Animator.StringToHash("movement");

    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    private void Update()
    {
        MovementAnim();
    }


    private void EnemyMovement()
    {
        Rb.velocity = moveSpeed * DirectionSpeed(moveDir,xSpeed,ySpeed) * Time.deltaTime;  
        //transform.Translate(moveDir.x * moveSpeed * Time.deltaTime, moveDir.y * Time.deltaTime);   
    }

    private void MovementAnim()
    {

        if (moveDir.x != 0f || moveDir.y != 0f)
        {
            _anim.SetBool(movingHash, true);
        }
        else
        {
            _anim.SetBool(movingHash, false);
        }
    }
    private Vector2 DirectionSpeed(Vector2 moveDir, float xSpeed, float ySpeed)
    {
        Vector2 dirSpeed;
        dirSpeed.x = moveDir.x * xSpeed;   
        dirSpeed.y = moveDir.y * ySpeed;    

        return dirSpeed;    
    }

    private Vector2 EnemeyDirection(ref Vector2 moveDir)
    {
        float changeDir = moveDir.x * -1;
        moveDir = new Vector2(changeDir, moveDir.y);
        return moveDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            EnemeyDirection(ref moveDir);
        }
        if(collision.gameObject.tag == "DangerZone")
            Destroy(gameObject);
    }






}
