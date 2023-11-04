using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action InteractEvent;

    GameManager gm;

    private void Start() {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            InteractEvent?.Invoke();
        }
    }
}
