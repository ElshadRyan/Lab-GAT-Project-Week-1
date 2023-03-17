using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter StoveCounter;
    [SerializeField] private GameObject StoveOnGameObject;
    [SerializeField] private GameObject ParticlesGameObject;

    private void Start()
    {
        StoveCounter.OnStateChanged += StoveCounter_OnStateChanged1;
    }

    private void StoveCounter_OnStateChanged1(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool ShowVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        StoveOnGameObject.SetActive(ShowVisual);
        ParticlesGameObject.SetActive(ShowVisual);

    }

}
