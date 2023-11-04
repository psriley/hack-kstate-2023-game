using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action InteractEvent;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            InteractEvent?.Invoke();
        }
    }
}
