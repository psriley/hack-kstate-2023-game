using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    GameManager gm;

    private void Start() {
        gm = GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // TODO: prompt the user to press "Interact" (E)

    }

    private void OnTriggerExit2D(Collider2D other) {
        // TODO: hide the "Interact" (E)
    }
}
