using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float followSpeed = 4f;
    [SerializeField] private float yOffset;
    [SerializeField] private Transform target;

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);

        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
