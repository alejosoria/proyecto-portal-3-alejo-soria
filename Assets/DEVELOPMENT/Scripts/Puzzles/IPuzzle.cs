using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IPuzzle : MonoBehaviour
{
    [SerializeField] private UnityEvent onPress;
    private AudioSource source;
    private Renderer r;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        r = GetComponent<Renderer>();
    }

    public void ForceTrigger()
    {
        r.material.color = Color.green;
        onPress?.Invoke();
        source.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube") && !other.attachedRigidbody.isKinematic)
        {
            Destroy(other.attachedRigidbody);
            r.material.color = Color.green;
            onPress?.Invoke();
            source.Play();
        }
    }
}
