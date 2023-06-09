using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CuttingCounter : BaseCounter, IProgressUIBar
{

    public static event EventHandler OnAnyCut;

    public event EventHandler<IProgressUIBar.OnProgressChangedEventArgs> OnProgressChange;

    
    public event EventHandler OnCut;
        

    [SerializeField] private CuttingRecipeSO[] CuttingRecipeSOArray;

    private int CuttingProgress;
    public override void Interact(Player Player)
    {
        if (!HasKitchenObject())
        {
            if (Player.HasKitchenObject())
            {
                if (HasRecipeWithInput(Player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    Player.GetKitchenObject().SetKitchenObjectParent(this);
                    CuttingProgress = 0;

                    CuttingRecipeSO CuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChange?.Invoke(this, new IProgressUIBar.OnProgressChangedEventArgs 
                    { 
                        ProgressNormalized = (float)CuttingProgress / CuttingRecipeSO.CuttingProgressMax
                    });

                }
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
            }
        }
    }

    public override void InteractAlternate(Player Player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            CuttingProgress++;
            CuttingRecipeSO CuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut.Invoke(this, EventArgs.Empty);
            OnProgressChange?.Invoke(this, new IProgressUIBar.OnProgressChangedEventArgs
            {
                ProgressNormalized = (float)CuttingProgress / CuttingRecipeSO.CuttingProgressMax
            });

            if (CuttingProgress >= CuttingRecipeSO.CuttingProgressMax)
            {
                KitchenObjectSO OutputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(OutputKitchenObjectSO, this);
            }
            
        }
    }    

    private bool HasRecipeWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        CuttingRecipeSO CuttingRecipeSO = GetCuttingRecipeSOWithInput(InputKitchenObjectSO);
        return CuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO InputKitchenObjectSO )
    {
        CuttingRecipeSO CuttingRecipeSO = GetCuttingRecipeSOWithInput(InputKitchenObjectSO);
        if (CuttingRecipeSO != null)
        {
            return CuttingRecipeSO.Output;
        }
        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in CuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.Input == InputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

}
