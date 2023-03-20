using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player Player;
    private float FootStepTimer;
    private float FootStepTimerMAX = .1f;

    private void Awake()
    {
        Player = GetComponent<Player>();
    }

    private void Update()
    {
        FootStepTimer = Time.deltaTime;
        if (FootStepTimer < 0f)
        {
            FootStepTimer = FootStepTimerMAX;

            if (Player.isWalking())
            {
                float Volume = 5f;
                SoundManager.Instance.PlayFootStepSound(Player.transform.position, Volume);
            }
           
        }
    }

}
