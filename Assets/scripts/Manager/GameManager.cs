using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public bool AllChakrasCollected;
    public int collectedChakras;
    private void Awake()
    {
        Instance = this;
    }
    
}
