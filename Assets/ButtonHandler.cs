using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerController playerController;
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Button attackButton;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isAttacking = false;  

    void Start()
    {
        jumpButton.onClick.RemoveAllListeners();

        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(Jump);
        }

        if (attackButton != null)
        {
            attackButton.onClick.AddListener(() => {  });
        }
    }

    public void MoveLeft()
    {
        isMovingLeft = true;
    }

    public void MoveRight()
    {
        isMovingRight = true;
    }

    public void StopMoving()
    {
        isMovingLeft = false;
        isMovingRight = false;
        playerController.StopMoving();
    }

    public void Jump()
    {
        jumpButton.interactable = false;
        playerController.Jump();
        Invoke(nameof(EnableJumpButton), 0.5f);
    }

    void EnableJumpButton()
    {
        jumpButton.interactable = true;
    }

    public void Attack()
    {
        if (isAttacking) return; 
        isAttacking = true;

        playerController.Attack();
        Invoke(nameof(ResetAttack), 0.1f); 
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == leftButton.gameObject)
        {
            MoveLeft();
        }
        else if (eventData.pointerCurrentRaycast.gameObject == rightButton.gameObject)
        {
            MoveRight();
        }
        else if (eventData.pointerCurrentRaycast.gameObject == attackButton.gameObject)
        {
            Attack();  
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopMoving();
    }

    void Update()
    {
        if (isMovingLeft)
        {
            playerController.MoveLeft();
        }

        if (isMovingRight)
        {
            playerController.MoveRight();
        }
        
        // //keyboard movement 
        // float horizontal = Input.GetAxis("Horizontal");  
        // if (horizontal < 0)  
        // {
        //     if (!isMovingLeft)
        //     {
        //         MoveLeft();
        //     }
        // }
        // else if (horizontal > 0)  
        // {
        //     if (!isMovingRight)  
        //     {
        //         MoveRight();
        //     }
        // }
        // else  
        // {
        //     StopMoving();  
        // }
    

    }
}