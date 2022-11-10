using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MentalHealth : MonoBehaviour {
    [SerializeField] HealthBar healthBar;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float health;
    [SerializeField] int jumpScareDamage = 10;
    [SerializeField] int timeLimitation = 60;
    [SerializeField] float jumpScareTimeInterval = 10f;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] Image crackImage;
    
    
    float dps;
    float waitTime;
    bool triggerShaking = false;

    void Start() {
        dps = health / timeLimitation;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        FunctionTimer.Create(JumpScare, jumpScareTimeInterval);
        FunctionTimer.Create(DecreaseHealthByTime, 1);
        
        InvokeRepeating("Shaking", 5, 5);
    }

    // Update is called once per frame
    void Update() {
        float heathPercentage = health / maxHealth;
        if (heathPercentage < 0.75) {
            triggerShaking = true;
        }

        if (heathPercentage < 0.5) {
            crackImage.enabled = true;
        }
    }

    void Shaking() {
        if (triggerShaking) {
            StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
        }
    }

    bool IsAlive() {
        return health > 0;
    }

    void DecreaseHealth(float amount) {
        health = Mathf.Clamp(health - amount, 0, maxHealth);
        healthBar.SetHealth(health);
    }

    void JumpScare() {
        if (IsAlive()) {
            DecreaseHealth(jumpScareDamage);
            FunctionTimer.Create(JumpScare, jumpScareTimeInterval);
        }
    }

    void DecreaseHealthByTime() {
        if (IsAlive()) {
            DecreaseHealth(dps);
            FunctionTimer.Create(DecreaseHealthByTime, 1);
        }
    }

    


}