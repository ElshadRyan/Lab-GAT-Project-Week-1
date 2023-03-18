using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO RecipeListSO;
    private List<RecipeSO> WaitingRecipeSOList;
    private float SpawnRecipetimer;
    private float SpawnRecipetimerMAX = 4f;
    private int WaitingRecipeMAX = 4;


    private void Awake()
    {
        Instance = this;
        WaitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        SpawnRecipetimer -= Time.deltaTime;
        if (SpawnRecipetimer <= 0f)
        {
            SpawnRecipetimer = SpawnRecipetimerMAX;
            if (WaitingRecipeSOList.Count < WaitingRecipeMAX)
            {
                SpawnRecipetimer = SpawnRecipetimerMAX;
                RecipeSO WaitingRecipeSO = RecipeListSO.RecipeSOList[UnityEngine.Random.Range(0, RecipeListSO.RecipeSOList.Count)];
                WaitingRecipeSOList.Add(WaitingRecipeSO);

                Debug.Log(WaitingRecipeSO.RecipeName);
                OnRecipeSpawn.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe (PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0;  i < WaitingRecipeSOList.Count; i++)
        {
            RecipeSO WaitingRecipeSO = WaitingRecipeSOList[i];
            if (WaitingRecipeSO.KitchenOBjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                Debug.Log("in");
                bool PlateContentMatchRecipe = true;
                foreach (KitchenObjectSO RecipeKitchenObjectSO in WaitingRecipeSO.KitchenOBjectSOList)
                {
                    bool IngridientFound = false;
                    foreach (KitchenObjectSO PlateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (PlateKitchenObjectSO == RecipeKitchenObjectSO)
                        {
                            IngridientFound = true;
                            break; 
                        }
                    }
                    if (!IngridientFound)
                    {
                        PlateContentMatchRecipe = false;
                    }
                }
                if (PlateContentMatchRecipe)
                {
                    WaitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
            
        }
        OnRecipeFailed.Invoke(this, EventArgs.Empty);

    }
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return WaitingRecipeSOList;
    }
}
