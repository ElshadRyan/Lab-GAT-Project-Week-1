using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IsWalking = "IsWalking";
    private Animator animator;
    [SerializeField] private Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    
    }

    private void Update()
    {
        animator.SetBool(IsWalking, player.isWalking());
    }
}
