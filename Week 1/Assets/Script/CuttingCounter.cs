using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player Player)
    {
        if (!HasKitchenObject())
        {
            if (Player.HasKitchenObject())
            {
                Player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!Player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(Player);
            }
        }
    }

    public override void InteractAlternate(Player Player)
    {
        if (HasKitchenObject())
        {   
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }    

    

}
