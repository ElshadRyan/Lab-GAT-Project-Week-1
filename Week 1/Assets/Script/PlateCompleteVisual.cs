using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject PlateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> KitchenObjectSOGameObjectList;

    private void Start()
    {
        PlateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in KitchenObjectSOGameObjectList)
        {         
                kitchenObjectSO_GameObject.gameObject.SetActive(false);
            
        }
    }

    private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in KitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSO_GameObject.KitchenObjectSO == e.KitchenObjectSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
        
    }
}
