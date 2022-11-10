using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour {
    [SerializeField] Transform player;
    [SerializeField] float speed = 2f;

    AudioSource ghostSound;
    
    // Start is called before the first frame update
    void Start() {
        ghostSound = GetComponent<AudioSource>();
    }

    void OnEnable() {
        ghostSound.Play();
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, player.position) < 0.001f) {
            gameObject.SetActive(false);
        }
    }
}