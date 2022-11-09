using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalHealth : MonoBehaviour {
    [SerializeField] HealthBar healthBar;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float health;
    [SerializeField] int jumpScareDamage = 10;
    [SerializeField] int timeLimitation = 60;
    [SerializeField] float jumpScareTimeInterval = 10f;

    float dps;
    
    void Start() {
        dps = health / timeLimitation;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        FunctionTimer.Create(JumpScare, jumpScareTimeInterval);
        FunctionTimer.Create(DecreaseHealthByTime, 1);
    }

    // Update is called once per frame
    void Update() {
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