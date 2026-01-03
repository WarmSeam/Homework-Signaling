using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SirenAlert : MonoBehaviour
{
    [SerializeField] private AudioClip _sirenAudioClip;
    [SerializeField, Range(0f, 5f)] private float _volumeIncreaseRate = 1f;
    [SerializeField, Range(0f, 1f)] private float _volumeStep = 0.1f;

    private AudioSource _audioSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    private Coroutine _volumeCoroutine;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _audioSource.clip = _sirenAudioClip;

        _delay = new WaitForSeconds(_volumeIncreaseRate);
    }

    public void IncreaseVolume()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        StartVolumeChange(_maxVolume);
    }

    public void DecreaseVolume()
    {
        StartVolumeChange(_minVolume);
    }

    private void StartVolumeChange(float targetVolume)
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine = StartCoroutine(ChangeVolume(targetVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            yield return _delay;

            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeStep);
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();

        _volumeCoroutine = null;
    }
}
