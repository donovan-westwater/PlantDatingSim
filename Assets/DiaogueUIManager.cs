using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaogueUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject diaPrefab;
    public GameObject promptWindow;
    public Slider[] bars = new Slider[3];
    Vector3 choiceA;
    Vector3 choiceB;
    bool personalityChoice = false;
    void Start()
    {
        bars[0].maxValue = PlayerManager.instance.SolarMeter;
        bars[1].maxValue = PlayerManager.instance.SoilMeter;
        bars[2].maxValue = PlayerManager.instance.WaterMeter;
        DialogueManager.instance.onDialougeUpdateEvent += OnDialougeUpdate;
    }
    public void UpdateBar()
    {
        bars[0].value = Mathf.Clamp(PlayerManager.instance.SolarMeter, 0, 100);
        bars[1].value = Mathf.Clamp(PlayerManager.instance.SoilMeter, 0, 100);
        bars[2].value = Mathf.Clamp(PlayerManager.instance.WaterMeter, 0, 100);
    }
    public void OnDialougeUpdate()
    {
        foreach (Transform t in transform)
        {
            t.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(t.gameObject);
        }
        UpdateBar();
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
        choiceA = d.choiceA;
        choiceB = d.choiceB;
        personalityChoice = d.isPersonalityChoice;
        if (personalityChoice) promptWindow.transform.GetChild(0).GetComponent<Text>().text = "Your plant friend wants to expand their interests. What will you do?";
        
        int i = 1;
        if (d.isPersonalityChoice) {  
            i = 0;
            for (;i< d.dialogues.Count; i++)
            {
                GameObject uiObj = Instantiate(diaPrefab);
                uiObj.tag = "Choice" + i;
                uiObj.gameObject.SetActive(true);
                uiObj.GetComponent<Button>().onClick.AddListener(() => OnClickTest(uiObj.tag));
                uiObj.transform.SetParent(transform, false);
                uiObj.transform.GetChild(0).GetComponent<Text>().text = d.dialogues[i][0];
            
            }
        }
        else
        {
            for (; i < d.dialogues[DialogueManager.instance.currentPersonality].Count; i++)
            {
                GameObject uiObj = Instantiate(diaPrefab);
                if (i == 1) {
                    uiObj.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Solar: " + choiceA.x
                    + " Soil: " + choiceA.y + " Water: " + choiceA.z;
                }
                else
                {
                    uiObj.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Solar: " + choiceB.x
                    + " Soil: " + choiceB.y + " Water: " + choiceB.z;
                }
                
                uiObj.tag = "Choice" + i;
                uiObj.gameObject.SetActive(true);
                uiObj.GetComponent<Button>().onClick.AddListener(() => OnClickTest(uiObj.tag));
                uiObj.transform.SetParent(transform, false);
                uiObj.transform.GetChild(0).GetComponent<Text>().text = d.dialogues[DialogueManager.instance.currentPersonality][i];

            }
        }
    }
    //Be able to select dialogue options. Doesn't do much else yet
    public void OnClickTest(string t)
    {
        if (personalityChoice)
        {
            switch (t)
            {
                case "Choice0":
                    PlayerManager.instance.personalityRankings[0] += 1f;
                    break;
                case "Choice1":
                    PlayerManager.instance.personalityRankings[1] += 1f;
                    break;
                case "Choice2":
                    PlayerManager.instance.personalityRankings[2] += 1f;
                    break;
                case "Choice3":
                    PlayerManager.instance.personalityRankings[3] += 1f;
                    break;
                case "Choice4":
                    PlayerManager.instance.personalityRankings[4] += 1f;
                    break;
                default:
                    break;
            }
            PlayerManager.instance.growthState += 1;
        }
        else
        {
            if (t.Equals("Choice1"))
            {
                PlayerManager.instance.SolarMeter += choiceA.x;
                PlayerManager.instance.SoilMeter += choiceA.y;
                PlayerManager.instance.WaterMeter += choiceA.z;
            }
            else
            {
                PlayerManager.instance.SolarMeter += choiceB.x;
                PlayerManager.instance.SoilMeter += choiceB.y;
                PlayerManager.instance.WaterMeter += choiceB.z;
            }
            PlayerManager.instance.SolarMeter = Mathf.Clamp(PlayerManager.instance.SolarMeter, 0f, 100f);
            PlayerManager.instance.SoilMeter = Mathf.Clamp(PlayerManager.instance.SoilMeter, 0f, 100f);
            PlayerManager.instance.WaterMeter = Mathf.Clamp(PlayerManager.instance.WaterMeter, 0f, 100f);
            UpdateBar();
        }
        DialogueManager.instance.activeChoices.RemoveAt(0);
        DialogueManager.instance.wait = false;
        int maxIndex = 0;
        float max = 0;
        for(int j = 0; j < PlayerManager.instance.personalityRankings.Length; j++)
        {
            if(max < PlayerManager.instance.personalityRankings[j])
            {
                maxIndex = j;
                max = PlayerManager.instance.personalityRankings[j];
            }
        }
        DialogueManager.instance.currentPersonality = maxIndex;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
