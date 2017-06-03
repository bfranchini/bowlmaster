using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptManager : MonoBehaviour {

    private GameObject promptsParent { get; set; }
    private Transform instructionsPrompt { get; set; }
    private const string INSTRUCTIONS_SEEN_KEY = "instructions_seen";

    void Start()
    {
        promptsParent = GameObject.Find("Prompts");

        if (!promptsParent)
        {
            Debug.LogError("Could not find prompts parent object!");
            return;
        }

        instructionsPrompt = promptsParent.transform.Find("InstructionsPrompt");

        if (!instructionsPrompt)
            Debug.LogError("Could not find a replay prompt!");

        if (PlayerPrefs.HasKey(INSTRUCTIONS_SEEN_KEY))
            HideInstructions();
        else
        {
            ShowInstructions();
            PlayerPrefs.SetInt(INSTRUCTIONS_SEEN_KEY, 1);
        }            
    }

    public void HideInstructions()
    {
        instructionsPrompt.gameObject.SetActive(false);
    }

    public void ShowInstructions()
    {
        instructionsPrompt.gameObject.SetActive(true);
    }
}
