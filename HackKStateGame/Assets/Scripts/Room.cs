using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int roomNum;
    [SerializeField] private Transform cameraNewPos;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private float transitionSpeed = 1f;

    bool collEnter = false;

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

    private void Update() {
        if (collEnter) {
            Vector3 targetPos = new Vector3(cameraNewPos.position.x, cameraPos.position.y, cameraPos.position.z);
            cameraPos.position = Vector3.Slerp(cameraPos.position, targetPos, transitionSpeed * Time.deltaTime);
        }
    }
}
