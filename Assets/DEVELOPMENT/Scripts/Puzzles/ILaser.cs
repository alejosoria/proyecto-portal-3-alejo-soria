using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILaser : MonoBehaviour
{
    private LineRenderer laser;
    private AudioSource source;
    private Transform root;
    private bool rotating;

    [SerializeField] private AudioClip moveSFX, laserSFX;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        laser = GetComponentInChildren<LineRenderer>();
        root = transform.Find("Root");
        Ray();
    }

    public void Rotate()
    {
        if (rotating) return;
        else rotating = true;
        laser.enabled = false;
        source.clip = moveSFX;
        source.Play();
        transform.LeanRotateY(transform.eulerAngles.y + 90f, 5f).setOnComplete(() => Ray());
    }

    private void Ray()
    {
        rotating = false;
        laser.enabled = true;
        source.clip = laserSFX;
        source.Play();
        if (Physics.Raycast(root.position, root.forward, out RaycastHit hit, 10f, LayerMask.GetMask("Default", "Interact")))
        {
            laser.SetPositions(new Vector3[] { root.position, hit.point });
            if (hit.collider.TryGetComponent(out IPuzzle puzzle))
            {
                puzzle.ForceTrigger();
                Destroy(this);
            }
        }
    }
}
