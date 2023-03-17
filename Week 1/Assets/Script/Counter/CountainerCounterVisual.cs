using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountainerCounterVisual : MonoBehaviour
{
    [SerializeField] private CountainerCounter CountainerCounter;
    private Animator Animator;
    private const string OPEN_CLOSE = "OpenClose";

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CountainerCounter.OnPlayerGrabObject += CountainerCounter_OnPlayerGrabObject;
    }

    private void CountainerCounter_OnPlayerGrabObject(object sender, System.EventArgs e)
    {
        Animator.SetTrigger(OPEN_CLOSE);
    }
}
