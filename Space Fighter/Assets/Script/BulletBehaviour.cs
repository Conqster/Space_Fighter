using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BulletBehaviour : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float OnScreenDelay = 1f;
    [Range(0f,100f)]
    [SerializeField] private int damagePercentage;

    private GameObject Target;
    private HealthSystem TargetHealth;
    private float maxHealth;



    private Light2D _light;
    private bool alert;


    private void Start()
    {
        Destroy(gameObject, OnScreenDelay);
    }

    private void Update()
    {
        
        if(alert == true)
        {
            _light.intensity -= Time.deltaTime;
            if(_light.intensity < 0f)
            {
                alert = false;
            }
        }

    }

    private float DamageRatio(float maxHealth)
    {
        float damage = (float)damagePercentage/100;
        float dealDmg = damage * maxHealth;
        return dealDmg; 
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemies")
        {
            DealDamage(collision.gameObject);

            Destroy(gameObject);

        }

        if (collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
    }


    private void DealDamage(GameObject Target)
    {
        TargetHealth = Target.GetComponent<HealthSystem>();
        Light2D _light = Target.GetComponentInChildren<Light2D>();
        Alert(_light);
        //_light.gameObject.SetActive(true);
        maxHealth = TargetHealth.FullHealth;
        float dealDmg = DamageRatio(maxHealth);
        TargetHealth.Health -= dealDmg;
        //print(damagePercentage);
        //Debug.Log("Max health " + maxHealth + " Enemy Health: " + TargetHealth.Health + " Bullet Damage dealing: " + DamageRatio(maxHealth));
        //print(TargetHealth.Health);
        //_light.gameObject.SetActive(false);
        //Invoke()
    }

    private bool Alert(Light2D _light)
    {
        _light.intensity = 4;
        return alert = true;
    }
}
