using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Follow Attributes")]
    Transform player;
    [SerializeField] float followHeight = 10.0f;
    [SerializeField] float followDistance = 8.5f;

    [Header("Player Camera Attributes")]
    [SerializeField] float targetHeight;
    [SerializeField] float CurrentHeight;
    [SerializeField] float currentRotation;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        CameraFollowPlayer();
    }

    void CameraFollowPlayer()
    {
        targetHeight = player.position.y + followHeight;

        currentRotation = transform.eulerAngles.y;

        CurrentHeight = Mathf.Lerp(transform.position.y,targetHeight,0.9f * Time.deltaTime);

        Quaternion euler = Quaternion.Euler(0.0f,currentRotation,0.0f);

        Vector3 targetPosition = player.position - (euler * Vector3.forward) *  followDistance;

        targetPosition.y = CurrentHeight;

        transform.position = targetPosition;

        transform.LookAt(player);
    }
}
