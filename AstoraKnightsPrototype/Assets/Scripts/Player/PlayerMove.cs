using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Player Movement Attributes")]
    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;
    [SerializeField] CollisionFlags collisionFlags;
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] bool bCanMove = false;
    public bool finishedMoveemnt = true;

    public Vector3 targetPosition;
    [SerializeField] Vector3 playerMovement;

    [SerializeField] float playerToPointDistance;

    [SerializeField] float gravity = 9.8f;
    [SerializeField] float height;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateHeight();
        CheckIfFinishedMovement();
    }

    bool IsGrounded()
    {
        return collisionFlags == CollisionFlags.CollidedBelow ? true : false;
    }

    void CalculateHeight()
    {
        if(IsGrounded())
        {
            height = 0.0f;
        }
        else
        {
            height -= gravity * Time.deltaTime;
        }
    }

    void CheckIfFinishedMovement()
    {
        //if we didnt finish the movement
        if(!finishedMoveemnt)
        {
            if(!animator.IsInTransition(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Stand")
                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.0f)
            {
                // normalized time of the animation is represented from 0 to 1
                // 0 is the beginning of the animation
                // 0.5 is the middle of the animation
                // 1 is the end of the animation
                finishedMoveemnt = true;
            }
        }
        else
        {
            MoveThePlayer();
            playerMovement.y = height * Time.deltaTime;
            collisionFlags = characterController.Move(playerMovement);
        }
    }


    void MoveThePlayer()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider is TerrainCollider)
                {
                    playerToPointDistance = Vector3.Distance(transform.position, hit.point);

                    if (playerToPointDistance >= 1.0f)
                    {
                        bCanMove = true;
                        targetPosition = hit.point;
                        //Debug.Log("<color=green> DeVBERIA MOVERSE </color>");
                    }
                    else
                    {
                        bCanMove = false;
                    }

                }
            }

        }

        if(bCanMove)
        {
            animator.SetFloat("Walk",1.0f);

            Vector3 tempTarget = new Vector3(targetPosition.x,targetPosition.y,targetPosition.z);

            Vector3 playerLookRotation = tempTarget - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(playerLookRotation),8.0f * Time.deltaTime);

            playerMovement = transform.forward * moveSpeed * Time.deltaTime;

            if(Vector3.Distance(transform.position,targetPosition) <= 0.1f)
            {
                bCanMove = false;
            }
        }
        else
        {
            playerMovement.Set(0f,0f,0f);
            animator.SetFloat("Walk",0f);
        }


    }


    public bool FinishedMovement
    {
        get { return finishedMoveemnt; }
        set { finishedMoveemnt = value; }
    }


    public Vector3 TargetPosition
    {
        get { return targetPosition; }
        set { targetPosition = value; }
    }

}
