using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitManager : MonoBehaviour
{

    [SerializeField] GameObject chakrasObj;
    [SerializeField] Rigidbody caseCover;

    private void Start()
    {
        chakrasObj.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.AllChakrasCollected)
        {
            caseCover.useGravity = true;
            caseCover.AddForce(Vector3.up*1000,ForceMode.Force);
            chakrasObj.SetActive(true);
            Debug.Log("Boom");
        }
    }
} 
