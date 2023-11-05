using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class FileManager : MonoBehaviour
{
    /// <summary>
    /// Directory that is the root for all files we create/delete (this is input by the user).
    /// </summary>
    public string rootFilePath;
    public float[] nums = new float[5];
    public float keyCode;
    public NotificationUI notificationUI;
    
    private void Start() 
    {
        // TODO: Make a scene that the player enter's a root folder into at the beginning of the game.
        // CheckFolderPathValidity();
        // rootFilePath = "";
        OnSceneLoaded();
    }

    private void OnSceneLoaded() {
        rootFilePath = PlayerPrefs.GetString("rootDirectory");
        Debug.Log("Changed to: " + rootFilePath);
    }

    /// <summary>
    /// Checks if the file exists in the user's file structure. MAKE SURE TO MAKE A NEW DIRECTORY INSIDE OF THIS ROOT DIRECTORY SO THERE ARE NO OVERWRITES
    /// </summary>
    public bool CheckFolderPathValidity(string inputPath)
    {
        if (Directory.Exists(inputPath))
        {
            // The folder path is valid; you can proceed with your game logic here.
            Debug.Log("Valid folder path: " + inputPath);
            return true;
        }
        else
        {
            // Display an error message to the player or handle the invalid path.
            Debug.LogWarning("Invalid folder path: " + inputPath);
            return false;
        }
    }

    public bool DoesPathExist(string path) 
    {
        string fullPath = Path.Combine(rootFilePath, path);
        Debug.Log("Full path: " + fullPath);
        return Directory.Exists(fullPath);
    }

    /// <summary>
    /// Creates the files that should be added to the player's file structure on game load.
    /// </summary>
    public void CreateStarterFiles()
    {
        try
        {
            if (!string.IsNullOrEmpty(rootFilePath))
            {
                string folderPath = Path.Combine(rootFilePath, "Level1");

                Directory.CreateDirectory(folderPath); // Create the directory

                for (int i = 0; i < nums.Length; i++)
                {
                    float randNum = UnityEngine.Random.Range(0, 10.0f);
                    string filename = $"{i}.txt";
                    // Create a new empty text file inside the directory
                    string createdFilePath = Path.Combine(folderPath, filename);
                    using (StreamWriter sw = File.CreateText(createdFilePath))
                    {
                        sw.WriteLine(filename);
                        sw.WriteLine("Hello");
                        sw.WriteLine("Hello again");
                        sw.WriteLine(randNum);
                    }

                    nums[i] = randNum;
                }

                // SetKeyCode();
            }
            else
            {
                Debug.LogWarning("Set a filepath first!");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error creating the folder: " + e.Message);
        }
    }

    /// <summary>
    /// Create a file with content (ex: note).
    /// </summary>
    public void CreateFile(string fileName, string content)
    {
        // Get the full file path by combining the root directory and the file name.
        string filePath = Path.Combine(rootFilePath, fileName);

        CreateEmptyFile(filePath);

        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine(content);
        }
    }

    /// <summary>
    /// Create an empty file (ex: screwdriver).
    /// </summary>
    public void CreateEmptyFile(string fileName)
    {
        string createdFilePath = Path.Combine(rootFilePath, fileName);

        // Check if the file already exists.
        if (!File.Exists(createdFilePath))
        {
            // The file stream is properly closed when this block is exited.
            using (File.Create(createdFilePath))
            {
                string[] fileSplit = fileName.Split("/");
                notificationUI.addFileText(fileSplit[fileSplit.Length-1]);
            }
        } 
    }

    /// <summary>
    /// Deletes a file (single use items?).
    /// </summary>
    public void DeleteFile()
    {

    }

    /// <summary>
    /// Creates a directory to hold collections of files (inventory, notes, etc.).
    /// </summary>
    public void CreateDirectory() 
    {
        
    }

    /// <summary>
    /// Checks if the player has a specific keycode in their key.txt file. 
    /// </summary>
    /// <param name="kC">The keycode to check for.</param>
    /// <returns>Whether the player has the keycode or not.</returns>
    public bool DoesPlayerHaveKeyCode(string kC) 
    {
        // search player's file structure in inventory to see if a key.txt exists with this
        // door's keycode
        string file = $"{rootFilePath}/key.txt";
        if (File.Exists(file)) 
        {
            Debug.Log("File exists!");
            return true;
            // using (StreamReader reader = 
            // return Directory.GetFiles("1234.txt");
        }

        Debug.Log($"File '{file}' does not exist!");
        return false;
    }

    public bool DoesPlayerHaveFile(string fileName, string text = null) {
        string file = $"{rootFilePath}/{fileName}";
        if (File.Exists(file)) 
        {
            Debug.Log("File exists!");
            if ((!string.IsNullOrEmpty(text))) {
                string[] fileText = File.ReadAllLines(file);
                if (Array.Exists(fileText, line => line.Contains(text))) {
                    Debug.Log($"'{text}' found!");
                    return true;
                }
                Debug.Log($"Cannot find '{text}' in '{fileName}'!");
                return false;
            }

            return true;
        }
        
        return false;
    }
}
