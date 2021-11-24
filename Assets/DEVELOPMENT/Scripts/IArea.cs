using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IArea : MonoBehaviour
{
    [SerializeField] bool invokeOnce;
    [SerializeField] UnityEvent events;
    private void Start() => gameObject.layer = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            events?.Invoke();
            if (invokeOnce) Destroy(gameObject);
        }
    }
}
