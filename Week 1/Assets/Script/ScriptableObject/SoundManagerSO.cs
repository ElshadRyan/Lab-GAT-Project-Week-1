using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundManagerSO : ScriptableObject
{
    public AudioClip[] Chop;
    public AudioClip[] DeliveryFail;
    public AudioClip[] DeliverySuccess;
    public AudioClip[] Footsteps;
    public AudioClip[] ObjectDrop;
    public AudioClip[] ObjectPickup;
    public AudioClip StoveSizzle;
    public AudioClip[] Trash;
    public AudioClip[] Warning;
}
