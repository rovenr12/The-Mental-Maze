using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour {
    [SerializeField] GameObject[] ghosts;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void ActiveGhost(Vector3 playerPosition, Vector3 startPosition) {
        GameObject activeGhost = ghosts[Random.Range(0, ghosts.Length)];
        activeGhost.transform.position = startPosition;
        activeGhost.transform.LookAt(playerPosition);
        activeGhost.SetActive(true);
    }
    
}