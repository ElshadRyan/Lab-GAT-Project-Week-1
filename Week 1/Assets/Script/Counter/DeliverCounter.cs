using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverCounter : BaseCounter
{

    public static DeliverCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player Player)
    {
        if (Player.HasKitchenObject())
        {
            if (Player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                Player.GetKitchenObject().DestroySelf();
            }
        }
    }

}
