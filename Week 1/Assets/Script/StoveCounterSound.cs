using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter StoveCounter;
    private AudioSource AudioSource;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool PlaySound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (PlaySound)
        {
            AudioSource.Play();
        }
        else
        {
            AudioSource.Pause();
        }
    }
}
