using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform Prefab;
    public Sprite Sprite;
    private string ObjectName;
}
