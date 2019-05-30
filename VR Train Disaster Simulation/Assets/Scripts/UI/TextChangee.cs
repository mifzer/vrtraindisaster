using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextChangee : MonoBehaviour {
    public Text MyName;
    public string myName;

    int wordIndex = -1;
    string word = null;
    string alpha = null;
    string alpha2 = null;
    char[] answerChar = new char[100];
    public static TextChangee Instance = null;
    public Text answerText = null;
    public string Question;
    public string[] Answer;
    public GameObject[] ButtonToShowAnswer;
    public Text[] TextToShowAnswer;
    public Text TextToShowQuestion;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
       // MyName.text = "" + myName;

        if(Input.GetKeyDown(KeyCode.A)){
            alphabetKey("A");
        }
        if(Input.GetKeyDown(KeyCode.Backspace)){
            BackspaceFunction();
        }
	}

    public void alphabetKey(string alphabet)
    {
        wordIndex++;
        char[] keepChar = alphabet.ToCharArray();
        answerChar[wordIndex] = keepChar[0];
        alpha = answerChar[wordIndex].ToString();
        word = word + alphabet;
        MyName.text = word;
    }
    public void BackspaceFunction()
    {
        if (wordIndex >= 0)
        {
            wordIndex--;
            alpha2 = null;
            for (int i = 0; i < wordIndex + 1; i++)
            {
                alpha2 = alpha2 + answerChar[i].ToString();

            }
            word = alpha2;
            MyName.text = word;
        }
    }
    public void alphabetSpace()
    {
        string alphabet = " ";
        wordIndex++;
        char[] keepChar = alphabet.ToCharArray();
        answerChar[wordIndex] = keepChar[0];
        alpha = answerChar[wordIndex].ToString();
        word = word + alphabet;
        MyName.text = word;
    }
}
