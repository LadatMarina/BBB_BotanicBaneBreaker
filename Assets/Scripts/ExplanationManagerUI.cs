using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplanationManagerUI : MonoBehaviour
{
    public static ExplanationManagerUI Instance { get; private set; }
    public Transform explanationPanel;
    public TextMeshProUGUI explanationText;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        explanationPanel.gameObject.SetActive(false);
    }

    public void ShowAnExplanation(string explanation)
    {
        explanationPanel.gameObject.SetActive(true);
        explanationText.text = explanation;
        ShowTime();
        
    }
    public void HideExplanation() { explanationPanel.gameObject.SetActive(false); }

    private IEnumerator ShowTime()
    {
        yield return new WaitForSeconds(5);
        explanationPanel.gameObject.SetActive(false);
    }
}
