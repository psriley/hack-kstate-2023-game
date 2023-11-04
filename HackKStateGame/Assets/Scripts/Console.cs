using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField] FileItem item;

    GameManager gm;

    private void Start() {
        gm = FindObjectOfType<GameManager>();
    }

    public void AddNote() {
        Debug.Log("Adding a note!");
        gm.fileManager.CreateFile(item.fileName, item.fileText);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // TODO: prompt the user to press "Interact" (E)
        EventManager.InteractEvent += AddNote;
    }

    private void OnTriggerExit2D(Collider2D other) {
        // TODO: hide the "Interact" (E)
        EventManager.InteractEvent -= AddNote;
    }
}
