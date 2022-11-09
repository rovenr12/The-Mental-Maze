using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer {

    public static FunctionTimer Create(Action action, float timer) {
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject);
        
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

        return functionTimer;
    }
    
    public class MonoBehaviourHook: MonoBehaviour {
        public Action onUpdate;
        void Update() {
            onUpdate?.Invoke();
        }
    }

    Action action;
    float timer;
    bool isDestoryed;
    GameObject gameObject;
    
    private FunctionTimer(Action action, float timer, GameObject gameObject) {
        this.action = action;
        this.timer = timer;
        this.gameObject = gameObject;
        isDestoryed = false;
    }

    public void Update() {
        if (isDestoryed) return;
        timer -= Time.deltaTime;
        if (timer < 0) {
            action();
            DestroySelf();
        }
    }

    void DestroySelf() {
        isDestoryed = true;
        UnityEngine.Object.Destroy(gameObject);
    }
    
}