using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] 
public class SoundSource : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private List<Clip> _audioClips;

    private void OnValidate()
    {
        _audioSource = _audioSource != null ? _audioSource : GetComponent<AudioSource>();
    }

    public void Play(string sound)
    {
        _audioSource.PlayOneShot(_audioClips.First((i) =>
        {
            return i.Name == sound;
        }).AudioClip);
    }

    [Serializable]
    private class Clip
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _audioClip;

        public string Name => _name;
        public AudioClip AudioClip => _audioClip;
    }
}
