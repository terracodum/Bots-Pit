using UnityEngine;

public class Sounds : MonoBehaviour

    
{
    public AudioClip[] stepSounds_AR;
    public AudioSource playSounds => GetComponent<AudioSource>();
    void Start()
    {
        
    }
    public void Step_sound_play()
    {
        playSounds.PlayOneShot(stepSounds_AR[Random.Range(0, stepSounds_AR.Length)],1f);
    }
}
