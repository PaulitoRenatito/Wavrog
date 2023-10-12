using System;
using UnityEngine;

public class Player : MonoBehaviour
{
        public static Player Instance { get; private set; }

        private Movement movement;
        private Interacter interacter;
        private Holder holder;
        [SerializeField] private GameInput gameInput;

        private Vector3 moveDir;
    
        private void Awake()
        {
            movement = GetComponent<Movement>();
            interacter = GetComponent<Interacter>();
            holder = GetComponent<Holder>();
            
            if (Instance != null)
            {
                Debug.LogError("There is more than one Player Instance");
            }
            
            Instance = this;
        }

        private void OnEnable()
        {
            gameInput.OnDashAction += GameInput_OnDashAction;
            gameInput.OnInteractAction += GameInput_OnInteractAction;
            gameInput.OnAttackAction += GameInput_OnAttackAction;
        }

        private void OnDisable()
        {
            gameInput.OnDashAction -= GameInput_OnDashAction;
            gameInput.OnInteractAction -= GameInput_OnInteractAction;
            gameInput.OnAttackAction -= GameInput_OnAttackAction;
        }
        
        private void GameInput_OnDashAction(object sender, GameInput.OnDashEventsArgs e)
        {
            Vector3 dashDir = new Vector3(e.dashDirection.x, 0f, e.dashDirection.y);

            dashDir = Camera.main.transform.TransformDirection(dashDir);
            
            movement.Dash(dashDir);
        }

        private void GameInput_OnAttackAction(object sender, EventArgs e)
        {
            if (holder.HasHoldableObject())
            {
                IWeapon weapon = holder.GetHoldableObject().GetComponent<IWeapon>();
                weapon?.Attack();
            }
        }

        private void GameInput_OnInteractAction(object sender, EventArgs e)
        {
            interacter.Interact();
        }

        private void Update()
        {
            if (!interacter.IsInteracting())
            {
                HandleMovement();
            }

            HandleInteractions();
            
        }

        private void HandleMovement()
        {
            Vector2 inputVector = gameInput.GetMovementVectorNormalized();
    
            moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

            moveDir = Camera.main.transform.TransformDirection(moveDir);
            
            movement.Move(moveDir);
            
            float yRotation = Camera.main.transform.rotation.eulerAngles.y;
            Vector3 lookRotation = new Vector3(0f, yRotation, 0f);
            Quaternion rotateDirection = Quaternion.Euler(lookRotation);

            if (movement.IsWalking)
            {
                movement.Rotate(rotateDirection);
            }

        }
        
        private void HandleInteractions()
        {
            interacter.HandleInteractions();
        }

        public Movement GetMovement()
        {
            return movement;
        }

        public Interacter GetInteracter()
        {
            return interacter;
        }
        
}
