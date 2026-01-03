using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SirenAlert : MonoBehaviour
{
    [SerializeField] private ResponseZone _responseZone;
    [SerializeField] private AudioClip _sirenAudioClip;
    [SerializeField, Range(0f, 5f)] private float _volumeIncreaseRate = 1f;
    [SerializeField, Range(0f, 1f)] private float _volumeIncreaseValue = 0.1f;

    private AudioSource _audioSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private bool _isPlaying = false;

    private Coroutine _volumeCoroutine;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;

        _delay = new WaitForSeconds(_volumeIncreaseRate);

    }

    private void OnEnable()
    {
        _responseZone.OnPlayerEntered += StartSound;
        _responseZone.OnPlayerLeft += StopSound;
    }

    private void OnDisable()
    {
        _responseZone.OnPlayerEntered -= StartSound;
        _responseZone.OnPlayerLeft -= StopSound;

        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
            _volumeCoroutine = null;
        }
    }

    private void StartSound()
    {
        _audioSource.clip = _sirenAudioClip;

        if(_audioSource.isPlaying == false)
        _audioSource.Play();

        _isPlaying = true;

        _volumeCoroutine = StartCoroutine(ChangeVolumeLoop());
    }

    private void StopSound()
    {
        _isPlaying = false;

        _volumeCoroutine = StartCoroutine(ChangeVolumeLoop());
    }

    private IEnumerator ChangeVolumeLoop()
    {
        bool isVolumePeak = false;

        while (isVolumePeak == false)
        {
            yield return _delay;

            if (_isPlaying == true)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _volumeIncreaseValue);

                if (_audioSource.volume >= _maxVolume)
                    isVolumePeak = true;
            }
            else
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _volumeIncreaseValue);

                if (_audioSource.volume <= _minVolume)
                {
                    _audioSource.volume = _minVolume;
                    _audioSource.Stop();

                    isVolumePeak = true;
                }
            }
        }

        _volumeCoroutine = null;
    }
}
