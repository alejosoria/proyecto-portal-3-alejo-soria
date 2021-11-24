using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) other.transform.SetParent(transform); }
    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) other.transform.SetParent(null); }
}
