using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSoundEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource Slot_SoundEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Slot_SoundEffect.Play();
    }
}
