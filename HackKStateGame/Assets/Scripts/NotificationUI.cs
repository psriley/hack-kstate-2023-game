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

    private void Awake() {
        panel.SetActive(false);
    }

    public void addFileText(string fileName) {
        textMesh.text = $"New file '{fileName}' added.";
        StartCoroutine(ShowUI());
    }

    public void removeFileText(string fileName) {
        textMesh.text = $"File '{fileName}' removed.";
        StartCoroutine(ShowUI());
    }

    private IEnumerator ShowUI() {
        panel.SetActive(true);

        yield return new WaitForSeconds(lifeTime);
        panel.SetActive(false);
    }
}
