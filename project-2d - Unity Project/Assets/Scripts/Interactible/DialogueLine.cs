using UnityEngine;
using System;

[System.Serializable]
public class DialogueLine {
    
    [Header("Dialogue Line Object")]
    [SerializeField] private NPCObject character;
    [SerializeField] [TextAreaAttribute] private string textLine;

    public NPCObject getPNJ()  { return character;            }
    public string    getName() { return character.getName();  }

    public string getText() {         
        
        char[] array = textLine.ToCharArray();

        string start   = "";
        string end     = "";
        string replace = "";

        bool charSpe = false;

        int index = 0;

        foreach (char letter in array) {
            switch(letter) {
                case '$':
                    charSpe = true;

                    index = Array.IndexOf(array, letter);
                    char c = array[index+1];

                    start   = textLine.Substring(0, index);
                    end     = textLine.Substring(index+2, textLine.Length - (index+2));
                    replace = "";

                    switch(c) {
                        case 'n':
                            replace = "%n" + character.getName();
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;

            }
		}

        if(charSpe) {
            return start + replace + end;
        } else {
            return textLine;
        }
    }
    
}