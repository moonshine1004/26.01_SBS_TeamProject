using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;

    public void PlaySound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}