using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 7f;
    
    [SerializeField]
    private GameInput gameInput;

    private bool isWalking;

    // private LineRenderer lineRenderer;
    
    // void Start()
    // {
    //     lineRenderer = gameObject.AddComponent<LineRenderer>();
    //     lineRenderer.material = Resources.Load<Material>("RaycastLineMaterial");
    //     lineRenderer.startWidth = 0.1f;
    //     lineRenderer.endWidth = 0.1f;
    // }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovingVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        //RaycastHit hit;
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHight = 2f;
        //bool canMove = !Physics.Raycast(transform.position, moveDir, out hit, playerSize);
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //Cannot move towards moveDir
            
            //Attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirX, moveDistance);
            
            if (canMove)
            {
                //Can move only the x
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only the x
            
                //Attempt only z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //Can move only the z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }

            }
        }
            
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        
        isWalking = moveDir != Vector3.zero;
        
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward ,moveDir, Time.deltaTime * rotateSpeed);
        // Отображение луча Raycast в виде линии
        // lineRenderer.SetPosition(0, transform.position);
        // lineRenderer.SetPosition(1, transform.position + moveDir * playerSize);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
