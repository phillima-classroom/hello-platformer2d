using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    Rigidbody2D playerRb;
    Animator playerAnimator;
    BoxCollider2D boxCollider;

    [SerializeField]
    float playerVeloH;
    [SerializeField]
    float playerPulo;

    Vector2 playerInput;
    
    bool canDoubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Correr();
    }

    void Correr(){

        playerRb.velocity = new Vector2(playerInput.x*playerVeloH, playerRb.velocity.y);

        bool isRunning = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;

        playerAnimator.SetBool("isRunning",isRunning);

        if(isRunning)
            FlipSprite();
    }

    void FlipSprite(){
        transform.localScale = new Vector2(Mathf.Sign(playerRb.velocity.x),1); 
    }

    void OnMove(InputValue inputValue){
        playerInput = inputValue.Get<Vector2>();
    }

    void OnJump(InputValue inputValue){
        
        if(inputValue.isPressed){
          if(boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            playerRb.velocity = new Vector2(0,playerPulo);
            canDoubleJump = true;
          }else if(canDoubleJump){
            playerRb.velocity = new Vector2(0,playerPulo);
            canDoubleJump = false;
          }
        }
        
   
    }
}