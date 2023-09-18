using UnityEngine;

[System.Serializable]
public class DialogueLine {
    
    [Header("Dialogue Line Object")]
    [SerializeField] private PNJObject character;
    [SerializeField] [TextAreaAttribute] private string textLine;

    public PNJObject getPNJ()  { return character;            }
    public string    getText() { return textLine;             }
    public string    getName() { return character.getName();  }

}