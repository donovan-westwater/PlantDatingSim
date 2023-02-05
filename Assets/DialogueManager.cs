using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueObject
{
    public int event_id = -1;
    //public int personality_id = 0; //Represents active personality to use
    //public int sentence_num = 0; //0 is always the prompt. All other sentences are choices
    public Vector3 choiceA = new Vector3(0,0,0);
    public Vector3 choiceB = new Vector3(0, 0, 0);
    public bool isPersonalityChoice = false;
    public Dictionary<int, List<string>> dialogues; //personality id is the key. prompt and choices are the value
    //WIP Add list of effects assoiated with the dialogues
    public DialogueObject()
    {
        dialogues = new Dictionary<int, List<string>>();
    }

}
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public int currentPersonality = 0; //Temp value until we create a player
    [HideInInspector]
    public Dictionary<int, DialogueObject> dialogueDict;
    [HideInInspector]
    public List<DialogueObject> activeChoices;
    public TextAsset spreadsheet;
    public delegate void OnDialogueUpdateEvent();
    public event OnDialogueUpdateEvent onDialougeUpdateEvent;
    public bool wait = false;
    int turn = 1;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Second);
        dialogueDict = new Dictionary<int, DialogueObject>();
        activeChoices = new List<DialogueObject>();
        //Read from spreadsheet
        ReadCSV();
    }
    void ReadCSV()
    {
        string[] data = spreadsheet.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
        int tableSize = data.Length / 8 - 1;
        for(int i = 0; i < tableSize; i++)
        {
            DialogueObject d; 
            int id = System.Int32.Parse(data[8 * (i + 1)]);
            if(!dialogueDict.TryGetValue(id,out d))
            {
                d = new DialogueObject();
                d.event_id = id;
                dialogueDict.Add(d.event_id, d);
            }
            int personality = System.Int32.Parse(data[8 * (i + 1) + 1]);
            int sentence_num = System.Int32.Parse(data[8 * (i + 1) + 2]);
            List<string> diaList;
            if (d.dialogues.TryGetValue(personality, out diaList))
            {
                diaList.Insert(sentence_num, data[8 * (i + 1) + 3] );
            }
            else
            {
                diaList = new List<string>();
                diaList.Add(data[8 * (i + 1) + 3]);
                d.dialogues.Add(personality, diaList);
            }
            if (sentence_num == 1) d.choiceA = new Vector3(System.Int32.Parse(data[8 * (i + 1) + 4])
                 , System.Int32.Parse(data[8 * (i + 1) + 5])
                 , System.Int32.Parse(data[8 * (i + 1) + 6]));
            else if(sentence_num == 2) d.choiceB = new Vector3(System.Int32.Parse(data[8 * (i + 1) + 4])
                 , System.Int32.Parse(data[8 * (i + 1) + 5])
                 , System.Int32.Parse(data[8 * (i + 1) + 6]));
            d.isPersonalityChoice = System.Boolean.Parse(data[8 * (i + 1) + 7]);
        }
    }
    // Update is called once per frame
    public void Update()
    {
        if (!wait)
        {
            //Pick new Events
            int i = Random.Range(0, dialogueDict.Count);
            activeChoices.Add(dialogueDict[i]);
            if (turn % 50 != 0) { //% 5
                while(activeChoices[0].isPersonalityChoice == true)
                {
                    activeChoices.RemoveAt(0);
                    i = Random.Range(0, dialogueDict.Count);
                    activeChoices.Add(dialogueDict[i]);
                }
            }
            else
            {
                while (activeChoices[0].isPersonalityChoice == false)
                {
                    activeChoices.RemoveAt(0);
                    i = Random.Range(0, dialogueDict.Count);
                    activeChoices.Add(dialogueDict[i]);
                }
            }
            onDialougeUpdateEvent();
            //Debug.Log("Dialogue "+dialogueDict[i].dialogues[0][0]);
            wait = true;
            turn++;
        }

    }
}
