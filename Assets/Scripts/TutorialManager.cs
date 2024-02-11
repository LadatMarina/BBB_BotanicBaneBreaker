using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public enum TutorialState
    {
        fase_1,
        fase_2,
        fase_3
    }

    public TextMeshPro introductionText;

    private TutorialState tutorialState;

    void Start()
    {
        tutorialState = TutorialState.fase_1;
    }

    void Update()
    {
        switch (tutorialState)
        {
            case TutorialState.fase_1:
                int index = 0;
                introductionText.text = Introduction.introduction[index];
                if (Input.GetKeyDown(KeyCode.A))
                {
                    index++;
                }
                //get the text and put the string Introduction.introduction[index];
                //if(press A) { show  Introduction.introduction[index++];}
                
                //
                break;
            case TutorialState.fase_2:
                //ss
                break;
        }
    }
}
public static class Introduction
{
    public static string[] introduction = new string[] 
    { "Here you are! Are we going to fight the witch today?",                                                       //0
      "Haven't you heard?! You must not be ready then either...",                                                   //1
      "Look, right now Búger is dominated by the witch Magdalena and she has cast a spell to make everyone sick.",  //2
      "But since you are Cati's daughter, you have the magic book of recipes to cure all the villagers."            //3
    };

}
