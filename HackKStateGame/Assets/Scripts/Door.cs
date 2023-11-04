using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Transform doorTransform;
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private BoxCollider2D myCollider;
    [SerializeField] private string keyCode;

    private bool isOpen = false;
    private GameManager gm;

    private void Start() {
        gm = FindObjectOfType<GameManager>();
    }

    // DO WE NEED TO ALLOW THE PLAYER TO CLOSE THE DOOR? SHOULD IT JUST CLOSE BY ITSELF?
    private void ToggleDoor() {
        if (isOpen){
            CloseDoor();
        }else{
            Debug.Log(keyCode);
            // check that player has keyCode
            if (gm.fileManager.DoesPlayerHaveKeyCode(keyCode)) {
                OpenDoor();
            }
            else {
                Debug.Log("I don't think you have the facilities for that fam");
            }
        }
    }
    
    private void OpenDoor() {
        Vector3 targetPosition = doorTransform.position + Vector3.up;

        // Smoothly move the door to the target position
        StartCoroutine(MoveToPosition(targetPosition));
        myCollider.enabled = false;

        isOpen = true;
    }

    private void CloseDoor()
    {
        // Calculate the target position (move the door upwards)
        Vector3 targetPosition = doorTransform.position + Vector3.down;

        // Smoothly move the door to the target position
        StartCoroutine(MoveToPosition(targetPosition));
        myCollider.enabled = true;

        isOpen = false;
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (doorTransform.position != targetPosition)
        {
            doorTransform.position = Vector3.MoveTowards(doorTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            EventManager.InteractEvent += ToggleDoor;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            EventManager.InteractEvent -= ToggleDoor;
        }
    }

    // TODO; Find a way to remove ToggleDoor from InteractEvent if it still exists
}