using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 7f;
    [SerializeField] private float RotateSpeed = 10f;
    [SerializeField] private GameInput GameInput;
    private bool IsWalking;
    private void Update()
    {
        HandleMovement();
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
            CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, MoveDirectionX, MoveDistance);

            if (CanMove)
            {
                MoveDirection = MoveDirectionX;
            }
            else
            {
                Vector3 MoveDirectionZ = new Vector3(0, 0, MoveDirection.z).normalized;
                CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, MoveDirectionZ, MoveDistance);

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
}
