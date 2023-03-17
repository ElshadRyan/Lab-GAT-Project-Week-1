using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter, IProgressUIBar
{
    public event EventHandler<IProgressUIBar.OnProgressChangedEventArgs> OnProgressChange;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] FryingRecipesSOArray;
    [SerializeField] private BurningRecipeSO[] BurningRecipesSOArray;


    private State state;
    private float FryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO BurningRecipeSO;
    private float BurningTimer;

    private void Start()
    {
        state = State.idle;
    }

    private void Update()
    {
        

        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.idle:
                    break;
                case State.Frying:
                    FryingTimer += Time.deltaTime;
                    OnProgressChange?.Invoke(this, new IProgressUIBar.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = FryingTimer / fryingRecipeSO.FryingProgressMax
                    });
                    if (FryingTimer > fryingRecipeSO.FryingProgressMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.Output, this);
                        state = State.Fried;
                        BurningTimer = 0f;
                        BurningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                    }
                    break;
                case State.Fried:
                    BurningTimer += Time.deltaTime;
                    OnProgressChange?.Invoke(this, new IProgressUIBar.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = BurningTimer / BurningRecipeSO.BurningTimerMax
                    });
                    if (BurningTimer > BurningRecipeSO.BurningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(BurningRecipeSO.Output, this);
                        
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChange?.Invoke(this, new IProgressUIBar.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = 0f
                        });

                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player Player)
    {
        if (!HasKitchenObject())
        {
            if (Player.HasKitchenObject())
            {
                if (HasRecipeWithInput(Player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    Player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    FryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChange?.Invoke(this, new IProgressUIBar.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = FryingTimer / fryingRecipeSO.FryingProgressMax
                    });
                }
            }
        }
        else
        {
            if (!Player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(Player);
                state = State.idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnProgressChange?.Invoke(this, new IProgressUIBar.OnProgressChangedEventArgs
                {
                    ProgressNormalized = 0f
                });
            }
            else
            {
                if (Player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        OnProgressChange?.Invoke(this, new IProgressUIBar.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = 0f
                        });
                    }
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSOWithInput(InputKitchenObjectSO);
        return FryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO InputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSOWithInput(InputKitchenObjectSO);
        if (FryingRecipeSO != null)
        {
            return FryingRecipeSO.Output;
        }
        return null;
    } 

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        foreach (FryingRecipeSO FryingRecipeSO in FryingRecipesSOArray)
        {
            if (FryingRecipeSO.Input == InputKitchenObjectSO)
            {
                return FryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        foreach (BurningRecipeSO BurningRecipeSO in BurningRecipesSOArray)
        {
            if (BurningRecipeSO.Input == InputKitchenObjectSO)
            {
                return BurningRecipeSO;
            }
        }
        return null;
    }

}
