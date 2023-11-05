using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public FileManager fileManager;
    public Transform cameraPos;
    public Room[] rooms = new Room[0];
    public float transitionSpeed = 2.0f;
    public Player player;

    Room inRoom;

    private void MoveCamera() {
        foreach (Room room in rooms) {
            if (room.collEnter) {
                inRoom = room;
                Transform closestRoom = room.closestRoom(player.transform);
                Vector3 targetPos = new Vector3(closestRoom.position.x, closestRoom.position.y, cameraPos.position.z);
                cameraPos.position = Vector3.Lerp(cameraPos.position, targetPos, transitionSpeed * Time.deltaTime);
            }
        }
    }

    private void Update() {
        MoveCamera();
    }

    public void LoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    // public void SetRootDirectory(string path) {
    //     // string rootDirectory = path;

    //     // if (path[path.Length - 1] == '/') {
    //     //     rootDirectory = path.Remove(path.Length - 1);
    //     // }
    //     // if (fileManager.CheckFolderPathValidity(rootDirectory)) {
    //     //     player.rootDirectory = rootDirectory;
    //     // }
    //     // else {

    //     // }

    //     player.rootDirectory = path;


    //     LoadNextScene();
    // }

    public void ExitGame() {
        // SCARY CODE THAT DELETES A FOLDER!
        // if (Directory.Exists(player.rootDirectory)) { Directory.Delete(player.rootDirectory, true); }
        // Directory.CreateDirectory(player.rootDirectory);
        Application.Quit();
    }
}
