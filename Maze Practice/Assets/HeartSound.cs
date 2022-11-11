using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSound : MonoBehaviour
{
    [SerializeField] float radiusRange = 20f;
    [SerializeField] AudioSource heartBeatSound;
    SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = radiusRange;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        heartBeatSound.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if(heartBeatSound.isPlaying)
        {
            heartBeatSound.Stop();
        }
    }
}
