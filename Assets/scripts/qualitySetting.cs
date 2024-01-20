using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class qualitySetting : MonoBehaviour
{


    //fps display

    public float pollingTime;
    private float time;
    private int frameCount;
    [SerializeField] TextMeshProUGUI fpsText;


    private void Awake()
    {
        QualitySettings.SetQualityLevel(0);
    }
    private void Update()
    {
        time += Time.deltaTime;
        frameCount++;
        if(time>= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate.ToString() + "FPS";
            time -= pollingTime;
            frameCount = 0;
        }
    }
   
}
