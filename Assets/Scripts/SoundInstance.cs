using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInstance : MonoBehaviour
{
    public static SoundInstance Instance;
    public AudioSource sfxSource;
    public AudioClip clickSound;
    public AudioClip keyboardClickSound;
    public AudioClip doorLockedSound;
    public AudioClip doorUnlockedSound;
    public AudioClip drawerSound;
    public AudioClip fireSound;
    public AudioClip drinkingSound;
    public AudioClip pouringSound;
    public AudioClip keyGetSound;
    public AudioClip meteorHit;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Optional: Helper methods
    public void PlayClick() => PlaySound(clickSound);
    public void PlayKeyboard() => PlaySound(keyboardClickSound);
    public void PlayDoorLocked() => PlaySound(doorLockedSound);
    public void PlayDoorUnlocked() => PlaySound(doorUnlockedSound);
    public void PlayDrawer() => PlaySound(drawerSound);
    public void PlayFire() => PlaySound(fireSound);
    public void PlayDrinking() => PlaySound(drinkingSound);
    public void PlayPouring() => PlaySound(pouringSound);
    public void PlayGetKey() => PlaySound(keyGetSound);
    public void PlayMeteor() => PlaySound(meteorHit);
}
