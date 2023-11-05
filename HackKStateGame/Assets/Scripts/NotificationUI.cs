using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject panel;
    [SerializeField] private float lifeTime = 3.0f;
    [SerializeField] private int truncTextAmount = 11;

    private void Awake() {
        panel.SetActive(false);
    }

    public void addFileText(string fileName) {
        string truncText = Truncate(fileName, truncTextAmount);
        textMesh.text = $"New file '{truncText}' added.";
        StartCoroutine(ShowUI());
    }

    public void removeFileText(string fileName) {
        string truncText = Truncate(fileName, truncTextAmount);
        textMesh.text = $"File '{truncText}' removed.";
        StartCoroutine(ShowUI());
    }

    private IEnumerator ShowUI() {
        panel.SetActive(true);

        yield return new WaitForSeconds(lifeTime);
        panel.SetActive(false);
    }

    public string Truncate(string value, int maxLength, string truncationSuffix = "…")
    {
        return value.Length > maxLength
            ? value.Substring(0, maxLength) + truncationSuffix
            : value;
    }
}
