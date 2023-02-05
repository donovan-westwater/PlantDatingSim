using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float SolarMeter = 100;
    public float SoilMeter = 100;
    public float WaterMeter = 100;
    public GameObject FailScreen;
    public static PlayerManager instance;
    public float[] personalityRankings = { 0, 0, 0, 0, 0 };
    public int growthState = 0;
    Text deathText;
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
        FailScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(SolarMeter < 0 || WaterMeter < 0 || SoilMeter < 0)
        {
            FailScreen.SetActive(true);
        }
    }
}
