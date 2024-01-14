using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadGame : MonoBehaviour
{
    public GameObject cam;
    public GameObject timeline;
    public GameObject timelinecam;
    public GameObject startPage;
    private void Start()
    {
        startPage.SetActive(true);
        cam.SetActive(true);
        timeline.SetActive(false);
        timelinecam.SetActive(false);

    }
}
