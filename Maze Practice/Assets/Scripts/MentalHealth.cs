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
    [SerializeField] Image holeImage;
    [SerializeField] Material wall;
    [SerializeField] Material ground;

    [SerializeField] Color normalWallColor;
    [SerializeField] Color normalGroundColor;
    [SerializeField] Color tensionWallColor;
    [SerializeField] Color tensionGroundColor;

    float dps;
    float waitTime;
    bool triggerShaking;

    void Start() {
        dps = health / timeLimitation;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        wall.color = normalWallColor;
        ground.color = normalGroundColor;
        
        FunctionTimer.Create(JumpScare, jumpScareTimeInterval);
        FunctionTimer.Create(DecreaseHealthByTime, 1);
        
        InvokeRepeating("Shaking", 5, 5);
    }

    // Update is called once per frame
    void Update() {
        ChangeVisualization();
    }

    void ChangeVisualization() {
        float heathPercentage = health / maxHealth;
        triggerShaking = heathPercentage < 0.8;
        
        if (heathPercentage < 0.6) {
            if (!crackImage.enabled) {
                crackImage.enabled = true;
            }
        } else {
            crackImage.enabled = false;
        }
        
        if (heathPercentage < 0.2) {
            if (!holeImage.enabled) {
                holeImage.enabled = true;
            }
        } else {
            holeImage.enabled = false;
        }

        if (heathPercentage < 0.4) {
            wall.color = tensionWallColor;
            ground.color = tensionGroundColor;
        } else {
            wall.color = normalWallColor;
            ground.color = normalGroundColor;           
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Heart")) {
            AddHealth(50);
            Destroy(other.gameObject);
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

    void AddHealth(float amount) {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        healthBar.SetHealth(health);
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