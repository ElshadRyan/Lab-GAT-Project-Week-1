 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
   [SerializeField] private KitchenObjectSO KitchenObjectSO;

     
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
            else
            {
                if (Player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(Player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            Player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
             
            
        }
    }

}
