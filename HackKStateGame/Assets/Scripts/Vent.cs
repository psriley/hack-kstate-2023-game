using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField] FileItem ventItem;
    [SerializeField] FileItem hintItem;
    [SerializeField] private string requiredItemFileName;
    [SerializeField] private AudioSource ventSound;

    GameManager gm;

    private void Start() {
        gm = FindObjectOfType<GameManager>();
    }

    public void AddNote() {
        if (gm.fileManager.DoesPlayerHaveFile(requiredItemFileName)) {
            Debug.Log("Adding a note!");
            gm.fileManager.CreateFile(ventItem.fileName, ventItem.fileText);
            ventSound.Play();
        }else {
            Debug.Log($"Player needs a {requiredItemFileName}");
            gm.fileManager.CreateFile(hintItem.fileName, hintItem.fileText);
        }
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
