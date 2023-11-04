using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int roomNum;
    [SerializeField] private Transform[] cameraPos = new Transform[0];

    public bool collEnter = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            collEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            collEnter = false;
        }
    }

    public Transform closestRoom(Transform targetObject) {
        float closestDistance = Mathf.Infinity;
        Transform closestTransform = null;

        foreach (Transform transform in cameraPos)
        {
            float distance = Vector3.Distance(targetObject.position, transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTransform = transform;
            }
        }

        return closestTransform;
    }
}
