using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueObject
{
    public int event_id = -1;
    public int personality_id = 0;
    public int sentence_num = 0;
    public Dictionary<int, List<string>> dialogues;
    public DialogueObject()
    {
        dialogues = new Dictionary<int, List<string>>();
    }

}
public class DialogueManager : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<int, DialogueObject> dialogueDict;
    public TextAsset spreadsheet;
    public delegate void OnDialogueUpdate();
    public event OnDialogueUpdate onDialougeUpdateEvent;
    bool wait = false;
    int turn = 0;
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Second);
        dialogueDict = new Dictionary<int, DialogueObject>();
        //Read from spreadsheet
        ReadCSV();
    }
    void ReadCSV()
    {
        string[] data = spreadsheet.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
        int tableSize = data.Length / 4 - 1;
        for(int i = 0; i < tableSize; i++)
        {
            DialogueObject d = new DialogueObject();
            d.event_id = System.Int32.Parse(data[4 * (i + 1)]);
            d.personality_id = System.Int32.Parse(data[4 * (i + 1) + 1]);
            d.sentence_num = System.Int32.Parse(data[4 * (i + 1) + 2]);
            List<string> diaList;
            if (d.dialogues.TryGetValue(d.personality_id, out diaList))
            {
                diaList.Insert(d.sentence_num, data[4 * (i + 1) + 3] );
            }
            else
            {
                diaList = new List<string>();
                diaList.Add(data[4 * (i + 1) + 3]);
                d.dialogues.Add(d.personality_id, diaList);
            }
            dialogueDict.Add(d.event_id, d);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!wait)
        {
            int i = Random.Range(0, 3);
            Debug.Log("Dialogue "+dialogueDict[i].dialogues[0][0]);
            wait = true;
        }
        if ((int)Time.time % 10 == 0) wait = false;
    }
}
