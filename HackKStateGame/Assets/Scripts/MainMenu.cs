using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // public InputField inputField;
    public TextMeshProUGUI message;
    
    [SerializeField]
    private GameObject firstScreen;
    [SerializeField]
    private GameObject secondScreen;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        firstScreen.SetActive(true);
        secondScreen.SetActive(false);
        message.text = "Choose an empty folder for this game to use";
    }

    public void Play() {
        // go to the set directory screen if save file doesn't exist
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
    }

    public void Exit() {
        // call function to exit the game
        gm.ExitGame();
    }

    public void SetRootDirectory() {
        // THIS IS ONLY USABLE IN THE UNITY EDITOR!
        string path = EditorUtility.OpenFolderPanel("Choose Empty Root Folder", "", "");

        // bool validDirectory = !Directory.EnumerateFileSystemEntries(path).Any();

        // if (validDirectory) {
        //     // Show success and file path
        //     // gm.SetRootDirectory(path);
        //     gm.player.rootDirectory = path;
        //     message.text = $"Successfully chose '{path}'";
        // }
        // else {
        //     // Show error
        //     message.text = $"Error, this directory cannot be chosen because it is not empty! Please choose an empty directory!";
        // }

        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] files = dirInfo.GetFiles();

        FileInfo[] filtered = files.Select(f => f).Where(f => (f.Attributes & FileAttributes.Hidden) ==
            0).ToArray();

        if (filtered.Length > 0) {
            message.text = $"Error, this directory cannot be chosen because it is not empty! Please choose an empty directory!";
        }
        else {
            gm.fileManager.rootFilePath = path;
            message.text = $"Successfully chose '{path}'";
        }

        // // load the next scene
        // string inputPath = inputField.text;
        // string rootDirectory = inputPath;

        // if (rootDirectory[rootDirectory.Length - 1] == '/') {
        //     rootDirectory = rootDirectory.Remove(rootDirectory.Length - 1);
        // }
        // if (gm.fileManager.CheckFolderPathValidity(inputPath)) {
        //     gm.SetRootDirectory(rootDirectory);
        // }
    }

    public void Continue() {
        if (!string.IsNullOrEmpty(gm.fileManager.rootFilePath) && message.text == $"Successfully chose '{gm.fileManager.rootFilePath}'") {
            PlayerPrefs.SetString("rootDirectory", gm.fileManager.rootFilePath);
            gm.LoadNextScene();
        }
        else {
            message.text = "A valid empty root directory must be chosen to continue";
        }
    }
}
