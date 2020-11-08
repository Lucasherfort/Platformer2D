using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    public static SoundBox Instance;

    private AudioSource source;

    [SerializeField]
    private AudioClip JumpSound = null;
    private float jumpSoundVolume = 0.5f;
    [SerializeField]
    private AudioClip JumpSecondSound = null;
    private float jumpSecondSoundVolume = 0.5f;
    [SerializeField]
    private AudioClip DeadSound = null;
    private float deadSoundVolume = 0.5f;
    [SerializeField]
    private AudioClip TeleportSound = null;
    private float teleportSoundVolume = 0.5f;
    [SerializeField]
    private AudioClip HitGroundSound = null;
    private float hitGroundSoundVolume = 0.5f;
    [SerializeField]
    private AudioClip BounceSound = null;
    private float bounceSoundVolume = 0.5f;
    [SerializeField]
    private AudioClip CheckPointSound = null;
    private float checkPointSoundVolume = 0.5f;

    private void Awake () {
        if(Instance){
            Destroy(this);
            return;
        }

        Instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlayJumpSound () {
        source.PlayOneShot(JumpSound, jumpSoundVolume);
    }

    public void SetJumpSoundVolume (float volume) {
        jumpSoundVolume = volume;
    }

    public void PlayJumpSecondSound () {
        source.PlayOneShot(JumpSecondSound, jumpSecondSoundVolume);
    }

    public void SetJumpSecondVolume (float volume) {
        jumpSecondSoundVolume = volume;
    }

    public void PlayDeadSound () {
        source.PlayOneShot(DeadSound, deadSoundVolume);
    }

    public void SetDeadSoundVolume (float volume) {
        deadSoundVolume = volume;
    }

    public void PlayTeleportSound () {
        source.PlayOneShot(TeleportSound, teleportSoundVolume);
    }

    public void SetTeleportSoundVolume (float volume) {
        teleportSoundVolume = volume;
    }

    public void PlayHitGroundSound () {
        source.PlayOneShot(HitGroundSound, hitGroundSoundVolume);
    }

    public void SetHitGroundSoundVolume (float volume) {
        hitGroundSoundVolume = volume;
    }

    public void PlayBounceSound () {
        source.PlayOneShot(BounceSound, bounceSoundVolume);
    }

    public void SetBounceSoundVolume (float volume) {
        bounceSoundVolume = volume;
    }

    public void PlayCheckPointSound () {
        source.PlayOneShot(CheckPointSound, checkPointSoundVolume);
    }

    public void SetCheckPointSoundVolume (float volume) {
        checkPointSoundVolume = volume;
    }
}
