using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform CounterTopPoint;

    private KitchenObject KitchenObject;


    public virtual void Interact (Player Player)
    {

    }
    public virtual void InteractAlternate (Player Player)
    {

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return CounterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return KitchenObject;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }

}
