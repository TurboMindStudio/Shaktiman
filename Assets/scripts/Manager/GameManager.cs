using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public bool AllChakrasCollected;
    public int collectedChakras;

    public GameObject chakrasObj;

    public GameObject[] CharacterAvaters;
    public GameObject bots;
    public GameObject powerPanel;

    public bool isEnemyAttacking;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        chakrasObj.SetActive(false);
        CharacterAvaters[1].SetActive(false);
        CharacterAvaters[0].SetActive(true);
        bots.SetActive(false);
        powerPanel.SetActive(false);
    }

}
