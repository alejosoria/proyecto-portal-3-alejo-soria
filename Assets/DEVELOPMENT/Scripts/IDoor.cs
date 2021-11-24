using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array = System.Array;

public class IDoor : MonoBehaviour
{
    public bool locked;
    private Animator anima;
    private AudioClip[] clips;
    private AudioSource source;
    private AudioClip Get(string identifier) => Array.Find(clips, clip => clip.name.Contains(identifier));
    private AudioClip Get(AudioClip[] clips) => clips[Random.Range(0, clips.Length)];

    private void Awake()
    {
        anima = GetComponent<Animator>();
        clips = Resources.LoadAll<AudioClip>("SFX/Door");
        source = GetComponentInChildren<AudioSource>();
    }

    private void Play(string name)
    {
        source.clip = Get(name + Random.Range(1, 3));
        source.Play();
    }

    private void Action(bool enter)
    {
        if (locked || anima.GetBool("Open") == enter) return;
        else
        {
            if (anima.GetBool("Open") == enter) return;
            Play(enter ? "open " : "close ");
            anima.SetBool("Open", enter);
        }
    }

    public void ForceAction(bool enter)
    {
        if (anima.GetBool("Open") == enter) return;
        Play(enter ? "open " : "close ");
        anima.SetBool("Open", enter);
        locked = true;
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) Action(true); }

    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) Action(false); }
}
