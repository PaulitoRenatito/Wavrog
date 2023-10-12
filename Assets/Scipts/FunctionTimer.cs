using System;
using UnityEngine;

public class FunctionTimer
{

    private class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            onUpdate?.Invoke();
        }
    }
    
    private Action action;
    private float timer;
    private bool isDestroyed;

    public static FunctionTimer Create(Action action, float timer)
    {
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        
        FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject);
        
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

        return functionTimer;
    }
    
    private GameObject gameObject;
    
    private FunctionTimer(Action action, float timer, GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        this.gameObject = gameObject;
        isDestroyed = false;
    }

    private void Update()
    {
        if (!isDestroyed)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                action();
                DestroySelf();
            }
        }
        
    }

    private void DestroySelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
    }

    public float GetElapsedTimer()
    {
        return timer;
    }
}
