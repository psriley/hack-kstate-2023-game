using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FileManager fileManager;
    public Transform cameraPos;
    public Room[] rooms = new Room[0];
    public float transitionSpeed = 2.0f;
    public Transform player;

    Room inRoom;

    private void MoveCamera() {
        foreach (Room room in rooms) {
            if (room.collEnter) {
                inRoom = room;
                Vector3 targetPos = new Vector3(room.closestRoom(player).position.x, cameraPos.position.y, cameraPos.position.z);
                cameraPos.position = Vector3.Lerp(cameraPos.position, targetPos, transitionSpeed * Time.deltaTime);
            }
        }
    }

    private void Update() {
        MoveCamera();
    }
}
