using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngredientAddedEventArgs>        OnIngridientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> ValidKitchenObjectSOList;
    private List<KitchenObjectSO> KitchenObjectSOList;
    public void Awake()
    {
        KitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient (KitchenObjectSO kitchenObjectSO)
    {
        if (!ValidKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        if (KitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            KitchenObjectSOList.Add(kitchenObjectSO);
            OnIngridientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                KitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return KitchenObjectSOList;
    }
}
