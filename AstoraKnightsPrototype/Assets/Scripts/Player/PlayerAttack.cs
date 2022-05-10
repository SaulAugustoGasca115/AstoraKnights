using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    [Header("Player Attack UI")]
    [SerializeField] List<Image> fillWaitImage;
    private int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0 };
    Animator animator;
    bool bCanAttack = false;
    PlayerMove playerMove;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            bCanAttack = true;
        }
        else
        {
            bCanAttack = false;
        }

        CheckToFade();
        CheckInput();
    }

    void CheckInput()
    {
        if(animator.GetInteger("Atk") == 0)
        {
            playerMove.finishedMoveemnt = false;

            if(!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
            {
                playerMove.finishedMoveemnt = true;
            }
        }


        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerMove.targetPosition = transform.position;

            if(playerMove.finishedMoveemnt && fadeImages[0] != 1 && bCanAttack)
            {
                fadeImages[0] = 1;
                animator.SetInteger("Atk", 1);
            }

        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerMove.targetPosition = transform.position;

            if (playerMove.finishedMoveemnt && fadeImages[1] != 1 && bCanAttack)
            {
                fadeImages[1] = 1;
                animator.SetInteger("Atk", 2);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerMove.targetPosition = transform.position;

            if (playerMove.finishedMoveemnt && fadeImages[2] != 1 && bCanAttack)
            {
                fadeImages[2] = 1;
                animator.SetInteger("Atk", 3);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerMove.targetPosition = transform.position;

            if (playerMove.finishedMoveemnt && fadeImages[3] != 1 && bCanAttack)
            {
                fadeImages[3] = 1;
                animator.SetInteger("Atk", 4);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerMove.targetPosition = transform.position;

            if (playerMove.finishedMoveemnt && fadeImages[4] != 1 && bCanAttack)
            {
                fadeImages[4] = 1;
                animator.SetInteger("Atk", 5);
            }

        }
        else if (Input.GetMouseButtonDown(1))
        {
            playerMove.targetPosition = transform.position;

            if (playerMove.finishedMoveemnt && fadeImages[5] != 1 && bCanAttack)
            {
                fadeImages[5] = 1;
                animator.SetInteger("Atk", 6);
            }

        }
        else
        {
            animator.SetInteger("Atk",0);
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 targetPosition = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                targetPosition = new Vector3(hit.point.x,transform.position.y,hit.point.z);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(targetPosition - transform.position),15f * Time.deltaTime);
        }



    } // Check Input Function



    void CheckToFade()
    {
        if(fadeImages[0] == 1)
        {
            if(FadeAndWait(fillWaitImage[0],1.0f))
            {
                fadeImages[0] = 0;
            }
        }

        if (fadeImages[1] == 1)
        {
            if (FadeAndWait(fillWaitImage[1], 0.7f))
            {
                fadeImages[1] = 0;
            }
        }

        if (fadeImages[2] == 1)
        {
            if (FadeAndWait(fillWaitImage[2], 0.1f))
            {
                fadeImages[2] = 0;
            }
        }

        if (fadeImages[3] == 1)
        {
            if (FadeAndWait(fillWaitImage[3], 0.2f))
            {
                fadeImages[3] = 0;
            }
        }

        if (fadeImages[4] == 1)
        {
            if (FadeAndWait(fillWaitImage[4], 0.3f))
            {
                fadeImages[4] = 0;
            }
        }

        if (fadeImages[5] == 1)
        {
            if (FadeAndWait(fillWaitImage[5], 0.08f))
            {
                fadeImages[5] = 0;
            }
        }

    }//Check to Fade Function


    bool FadeAndWait(Image fadeImg,float fadeTime)
    {
        bool faded = false;

        if (fadeImg == null) return faded;

        if(!fadeImg.gameObject.activeInHierarchy)
        {
            fadeImg.gameObject.SetActive(true);
            fadeImg.fillAmount = 1;
        }

        fadeImg.fillAmount -= fadeTime * Time.deltaTime;

        if(fadeImg.fillAmount <= 0.0f)
        {
            fadeImg.gameObject.SetActive(false);
            faded = true;
        }

        return faded;
    }


}
