using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = System.Action;
using Array = System.Array;

public class IManager : MonoBehaviour
{
    public void Checkpoint(string name)
    {
        Transform checkpoint = GameObject.Find(name).transform;
        Transform player = FindObjectOfType<IPlayer>().transform;
        player.position = checkpoint.position;
    }

    #region Core
    private void Start()
    {
        ievents = new Queue<IEvent>();
        clips = Resources.LoadAll<AudioClip>("SFX/Announcer");
        Sequence_Entrance();
    }
    #endregion

    #region Doors
    [SerializeField] private IDoor[] doors;

    public void OpenDoor(int index) => doors[index].ForceAction(true);
    public void CloseDoor(int index) => doors[index].ForceAction(false);
    #endregion

    #region Audio
    private AudioClip[] clips;
    [SerializeField] AudioSource[] sources;
    private AudioClip Get(string identifier) => Array.Find(clips, clip => clip.name.Contains(identifier));
    private AudioClip Get(AudioClip[] clips) => clips[Random.Range(0, clips.Length)];
    public void PlayAt(int index, AudioClip clip) => sources[index].PlayOneShot(clip);
    #endregion

    #region Sequences
    public void Sequence_Entrance()
    {
        AudioClip clip01 = Get("greetings_morning");
        AudioClip clip02 = Get("greetings_intro_01");
        AudioClip clip03 = Get("greetings_intro_02");
        Sequence
        (
            new IEvent(1f, () => PlayAt(0, clip01)),
            new IEvent(clip01.length + 1, () => PlayAt(0, clip02)),
            new IEvent(clip02.length + 1, () => PlayAt(0, clip03)),
            new IEvent(clip03.length + 1, () => OpenDoor(0))
        );
    }

    public void Sequence_Level01_Intro()
    {
        AudioClip clip01 = Get("level_01_intro");
        Sequence
        (
            new IEvent(3f, () => PlayAt(1, clip01)),
            new IEvent(clip01.length + 1, () => OpenDoor(1))
        );
    }

    public void Sequence_Level01_Outro()
    {
        AudioClip clip01 = Get("level_01_outro");
        Sequence
        (
            new IEvent(1f, () => PlayAt(2, clip01)),
            new IEvent(clip01.length + 1, () => OpenDoor(2))
        );
    }

    public void Sequence_Level02_Intro()
    {
        AudioClip clip01 = Get("level_02_intro");
        Sequence
        (
            new IEvent(1f, () => PlayAt(3, clip01)),
            new IEvent(clip01.length + 0.25f, () => OpenDoor(3))
        );
    }

    public void Sequence_Level02_Outro()
    {
        AudioClip clip01 = Get("level_02_outro");
        Sequence
        (
            new IEvent(1f, () => PlayAt(4, clip01)),
            new IEvent(clip01.length + 0.25f, () => OpenDoor(4))
        );
    }

    public void Sequence_Level03_Intro()
    {
        AudioClip clip01 = Get("level_03_intro");
        Sequence
        (
            new IEvent(1f, () => PlayAt(5, clip01)),
            new IEvent(clip01.length + 0.25f, () => OpenDoor(5))
        );
    }

    public void Sequence_Level03_Outro()
    {
        AudioClip clip01 = Get("level_03_outro");
        Sequence
        (
            new IEvent(1f, () => PlayAt(6, clip01)),
            new IEvent(clip01.length + 1f, () => OpenDoor(6))
        );
    }

    public void Sequence_Level04_Intro()
    {
        AudioClip clip01 = Get("level_04_intro");
        Sequence
        (
            new IEvent(1f, () => PlayAt(7, clip01)),
            new IEvent(clip01.length + 0.25f, () => OpenDoor(7))
        );
    }

    public void Sequence_Level04_Outro()
    {
        AudioClip clip01 = Get("level_04_outro");
        Sequence
        (
            new IEvent(1f, () => PlayAt(8, clip01)),
            new IEvent(clip01.length + 1f, () => OpenDoor(8))
        );
    }

    public void Sequence_Level05_Intro()
    {
        AudioClip clip01 = Get("level_05_end");
        Sequence
        (
            new IEvent(1f, () => PlayAt(9, clip01)),
            new IEvent(clip01.length + 10f, () => UnityEngine.SceneManagement.SceneManager.LoadScene(0))
        );
    }

    private Queue<IEvent> ievents;
    public void Sequence(params IEvent[] events) {  foreach (var e in events) ievents.Enqueue(e); StartCoroutine(ISequence()); }
    private IEnumerator ISequence()
    {
        var ie = ievents.Peek();
        yield return new WaitForSeconds(ie.delay);
        ie.events?.Invoke(); ievents.Dequeue();
        if (ievents.Count != 0) StartCoroutine(ISequence());
    }

    [System.Serializable]
    public class IEvent
    {
        public float delay;
        public Action events;

        public IEvent(float delay, Action events)
        {
            this.delay = delay;
            this.events = events;
        }
    }
    #endregion
}
