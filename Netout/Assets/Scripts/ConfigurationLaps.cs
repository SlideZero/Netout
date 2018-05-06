using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationLaps : MonoBehaviour {

   
    public Transform[] checkPointArray;
    public static Transform[] checkpointA;
    public static int currentCheckpoint = 0;
    public static int currentLap = 0;
    public Vector3 startPos;
    public int Lap;

    void Start()
    {
        startPos = transform.position;
        currentCheckpoint = 0;
        currentLap = 0;
        checkPointArray = new Transform[3];
        checkPointArray[0] = GameObject.FindWithTag("CheckpointList").transform.GetChild(0);
        checkPointArray[1] = GameObject.FindWithTag("CheckpointList").transform.GetChild(1);
        checkPointArray[2] = GameObject.FindWithTag("CheckpointList").transform.GetChild(2);
    }

    void Update()
    {
        Lap = currentLap;
        checkpointA = checkPointArray;
    }
}
