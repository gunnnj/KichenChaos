using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent //Kế thừa lớp Interface
{
  
    public static Player Instance { get;private set; }


    public static Player instanceField;
    public static Player GetInstanceField() { 
        return instanceField;
    }
    public static void SetInstanceField(Player instanceField) { 
        Player.instanceField = instanceField;
    }


    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;

    [SerializeField] private GameInput gameInput;

    [SerializeField] private LayerMask countLayerMask;

    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteracDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if(Instance!= null)
        {
            Debug.LogError("Khong qua 1 nguoi choi");
        }
        Instance = this; //Gọi hàm Instance access OnSelectedCounterChanged
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction; //Nhấn E truy cập
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
        

    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter!=null)
        {
            selectedCounter.Interact(this);
        }
       
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVec = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);

        if (moveDir != Vector3.zero)
        {
            lastInteracDir = moveDir;
        }

        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteracDir, out RaycastHit raycastHit, interactionDistance, countLayerMask)) { 
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Has ClearCounter
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);

                    
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

    }
    private void HandleMovement() {
        Vector2 inputVec = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playeRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playeRadius, moveDir, moveDistance);
        // Trường hợp di chuyển chéo
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playeRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playeRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        //Xoay
        float rotatespeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotatespeed);
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter }); //Gọi thuộc tính tùy vào giá trị selectedCounter
    }

    public Transform GetKitchenobjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject() { 
        return kitchenObject; 
    }
    public bool isPlate(){
        if(kitchenObject is PlateKitchenObj plate){
            return true;
        }
        else{
            return false;
        }
    }
    public PlateKitchenObj GetPlate(){
        return (PlateKitchenObj)kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
