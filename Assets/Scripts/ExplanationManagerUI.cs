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
    {   //singleton
        if (Instance != null)
        {
            Destroy(this.gameObject);
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
    //show the explanation and hide after x seconds
    public void ShowAnExplanation(string explanation, int seconds)
    {
        explanationPanel.gameObject.SetActive(true);
        explanationText.text = explanation;
        StartCoroutine(RemoveAfterSeconds(seconds, explanationPanel.gameObject));
    }
    public void HideExplanation() { explanationPanel.gameObject.SetActive(false); }
    private IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
    }
}
