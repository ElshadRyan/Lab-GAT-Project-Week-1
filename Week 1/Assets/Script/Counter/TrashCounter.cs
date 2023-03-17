using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public override void Interact(Player Player)
    {
        if (Player.HasKitchenObject())
        {
            Player.GetKitchenObject().DestroySelf();
        }
    }

}
