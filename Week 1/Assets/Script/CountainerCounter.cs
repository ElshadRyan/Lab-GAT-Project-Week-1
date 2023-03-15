using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    public event EventHandler OnPlayerGrabObject;


    public override void Interact(Player Player)
    {
        if (!Player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(KitchenObjectSO, Player);

            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
        
    }
   
}
