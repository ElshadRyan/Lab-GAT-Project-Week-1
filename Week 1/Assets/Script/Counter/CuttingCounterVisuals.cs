using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisuals : MonoBehaviour
{
    [SerializeField] private CuttingCounter CuttingCounter;
    private Animator Animator;
    private const string CUT = "Cut";

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        Animator.SetTrigger(CUT);
    }
}
