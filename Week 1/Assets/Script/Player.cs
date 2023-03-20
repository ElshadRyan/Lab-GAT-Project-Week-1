using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }
    public event EventHandler OnPickedSomething;
    public event EventHandler <OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }


    [SerializeField] private float MoveSpeed = 7f;
    [SerializeField] private float RotateSpeed = 10f;
    [SerializeField] private GameInput GameInput;
    [SerializeField] private float InteractDistance = 2f;
    [SerializeField] private Transform KitchenObjectHoldPoint;
    private bool IsWalking;
    private Vector3 LastInteractionDirection;
    private BaseCounter SelectedCounter;
    private KitchenObject KitchenObject;

    private void Awake()
    {
        Instance = this;
    }
 
    private void Start()
    {
        GameInput.OnInteractAction += GameInput_OnInteractAction;
        GameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (SelectedCounter != null)
        {
            SelectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
       if (SelectedCounter != null)
        {
            SelectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector2 InputVector = GameInput.GetMovementVectorNormalized();

        Vector3 MoveDirection = new Vector3(InputVector.x, 0f, InputVector.y);

        if (MoveDirection != Vector3.zero)
        {
            LastInteractionDirection = MoveDirection;
        }

        if (Physics.Raycast(transform.position, LastInteractionDirection, out RaycastHit RaycastHit, InteractDistance))
        {
            if (RaycastHit.transform.TryGetComponent(out BaseCounter BaseCounter))
            {
                if (BaseCounter != SelectedCounter)
                {
                    SetSelectedCounter(BaseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter SelectedCounter)
    {
        this.SelectedCounter = SelectedCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs
        {
            SelectedCounter = SelectedCounter
        });
    }
    private void HandleMovement()
    {

        Vector2 InputVector = GameInput.GetMovementVectorNormalized();

        Vector3 MoveDirection = new Vector3(InputVector.x, 0f, InputVector.y);
        float MoveDistance = MoveSpeed * Time.deltaTime;
        float PlayerRadius = .7f;
        float PlayerHeight = 2f;
        bool CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, MoveDirection, MoveDistance);

        if (!CanMove)
        {
            Vector3 MoveDirectionX = new Vector3(MoveDirection.x, 0, 0).normalized;
            CanMove = MoveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, MoveDirectionX, MoveDistance);

            if (CanMove)
            {
                MoveDirection = MoveDirectionX;
            }
            else
            {
                Vector3 MoveDirectionZ = new Vector3(0, 0, MoveDirection.z).normalized;
                CanMove = MoveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, MoveDirectionZ, MoveDistance);

                if (CanMove)
                {
                    MoveDirection = MoveDirectionZ;
                }
            }
        }

        if (CanMove)
        {
            transform.position += MoveDirection * MoveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, MoveDirection, Time.deltaTime * RotateSpeed);
        IsWalking = MoveDirection != Vector3.zero;



    }

    public bool isWalking ()
    {
        return IsWalking;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
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
