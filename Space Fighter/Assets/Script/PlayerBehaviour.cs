using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceFighter;

public class PlayerBehaviour : Movement
{
  
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectPoint;
    [Range(100f, 1000f)]
    [SerializeField] private float bulletForce = 100f;
    [SerializeField] private bool fire;
    private Rigidbody2D bulletRb;
    private int shootTimes;

    [Range (0f, 5f)]
    [SerializeField] private float timeBetweenShot = 0.5f;

   
    private int shootingHash;

    [SerializeField] private bool useMouse;

   

    protected override void Start()
    {
        base.Start();

        shootingHash = Animator.StringToHash("shooting");
    }
    

    protected override void Update()
    {
        base.Update();

        if (fire)                                                                        
        {
            if(_anim != null)
                _anim.SetBool(shootingHash, true);  
            Shoot();
        }

        if(shootTimes != 0)
        {
            timeBetweenShot--;
            if(timeBetweenShot <= 0)
            {
                shootTimes = 0; 
            }
        }
    }

    protected override void GetInputs()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.y = Input.GetAxis("Vertical");

        fire = Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1");

        if(!useMouse)
        {
            rotate.x = Input.GetAxis("Right Analog X"); //+ Input.GetAxis("Mouse X");
        }else
        {
            rotate.x = Input.GetAxis("Mouse X");
        }
        rotate.y = Input.GetAxis("Right Analog Y");
    }



    private void Shoot()
    {
       
            GameObject newBullet = Instantiate(_projectile, _projectPoint.position, transform.rotation);

            bulletRb = newBullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(_projectPoint.transform.up * bulletForce);
        //shootTimes = 0;
    }
   
}
