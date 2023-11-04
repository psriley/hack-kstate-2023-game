using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FileItem", menuName = "FileItem", order = 0)]
public class FileItem : ScriptableObject {
    public string fileName;
    [TextArea]
    public string fileText;
}
    