using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;

public class HealthSystem : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] private int fullHealth = 10;
    [SerializeField] private float health;

    private float phaseShift = 0;
    private float x, oscillation;
    [HideInInspector] public float verticalShift = 0.5f;     //defines the starting point of the oscillaton and mid point of the whole value
    [Range(0f, 5f)]
    [SerializeField] public float period = 3f, amplitude = 0.5f;     //half of the total value to oscillate from 

    private Light2D _light;
    private float _lightIntensity;

    //private static FieldInfo m_FalloffField = typeof(Light2D).GetField("m_FalloffIntensity", BindingFlags.NonPublic | BindingFlags.Instance);

    public float Health
    {
        get { return health; }
        set { health = value; }
    }


    public float FullHealth { get { return fullHealth; } }

    

    private void Start()
    {
        health = FullHealth;
        //_light = GetComponentInChildren<Light2D>();
        try
        {
            _light = GetComponentInChildren<Light2D>();
        }
        catch(ArgumentException error)
        {
            Debug.Log("Light is need to use its functioality " + error);
        }
        //_light.color = Color.white; 
    }

    private void Update()
    {
        Dead();
        Danger();   
        Oscillation();
        //print(oscillation);
        //print(verticalShift);
        //print(Mathf.PI.Rad2Deg);
        //print(GetHealthPercentage());

        if (Input.GetKeyDown(KeyCode.P))
            health -= 2;
        //_light.intensity = _lightIntensity;
    }

  
    private void Dead()
    {
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void Danger()
    {
        if (GetHealthPercentage() <= 20)
        {
            _light.intensity = oscillation;
        }
    }



    private void Get2DLight()
    {
        _lightIntensity = _light.intensity;
    }

    //private void Get2DLight(float oscillation)
    //{

    //}


    private void Oscillation()
    {
        x += Time.deltaTime;

        oscillation = amplitude * Mathf.Sin(((2 * Mathf.PI) / period) * (x + phaseShift)) + verticalShift;

    }


    private float GetHealthPercentage()
    {
        float ratio = health / fullHealth;
        float healthPer = ratio * 100;
        return healthPer;
    }

}
