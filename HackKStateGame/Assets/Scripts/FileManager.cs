using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileManager : MonoBehaviour
{
    /// <summary>
    /// Directory that is the root for all files we create/delete (this is input by the user).
    /// </summary>
    public string rootFilePath;
    public float[] nums = new float[5];
    public float keyCode;
    
    private void Start() 
    {
        // TODO: Make a scene that the player enter's a root folder into at the beginning of the game.
        CheckFolderPathValidity();
    }

    /// <summary>
    /// Checks if the file exists in the user's file structure. MAKE SURE TO MAKE A NEW DIRECTORY INSIDE OF THIS ROOT DIRECTORY SO THERE ARE NO OVERWRITES
    /// </summary>
    public void CheckFolderPathValidity()
    {
        // string inputPath = fileInputField.text;

        if (Directory.Exists(rootFilePath))
        {
            // The folder path is valid; you can proceed with your game logic here.
            Debug.Log("Valid folder path: " + rootFilePath);
        }
        else
        {
            // Display an error message to the player or handle the invalid path.
            Debug.LogWarning("Invalid folder path: " + rootFilePath);
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
    /// Create a file (inventory item, note, etc.).
    /// </summary>
    public void CreateFile(string filePath, string fileName)
    {
        if (DoesPathExist(filePath))
        {
            string createdFilePath = Path.Combine($"{rootFilePath}/{filePath}", fileName);
            File.Create(createdFilePath);
            File.Open(createdFilePath, FileMode.Open);
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
}