using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaogueUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject diaPrefab;
    public GameObject promptWindow;
    void Start()
    {
        DialogueManager.instance.onDialougeUpdateEvent += OnDialougeUpdate;
    }
    public void OnDialougeUpdate()
    {
        foreach (Transform t in transform)
        {
            t.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(t.gameObject);
        }
        DisplayChoices();
    }
    void DisplayChoices()
    {
        AddChoice(DialogueManager.instance.activeChoices[0]);
    }
    void AddChoice(DialogueObject d)
    {
        promptWindow.SetActive(true);
        promptWindow.transform.GetChild(0).GetComponent<Text>().text = d.dialogues[DialogueManager.instance.currentPersonality][0];
        for(int i =1;i< d.dialogues[DialogueManager.instance.currentPersonality].Count; i++)
        {
            GameObject uiObj = Instantiate(diaPrefab);
            uiObj.gameObject.SetActive(true);
            uiObj.GetComponent<Button>().onClick.AddListener(() => OnClickTest());
            uiObj.transform.SetParent(transform, false);
            uiObj.transform.GetChild(0).GetComponent<Text>().text = d.dialogues[DialogueManager.instance.currentPersonality][i];
            
        }
    }
    //Be able to select dialogue options. Doesn't do much else yet
    public void OnClickTest()
    {
        DialogueManager.instance.activeChoices.RemoveAt(0);
        DialogueManager.instance.wait = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
