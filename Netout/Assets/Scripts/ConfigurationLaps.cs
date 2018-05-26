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
    public int lapsToWin = 1;
    public AudioSource FinishSound;

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
        if(Lap != currentLap)
        {
            Lap = currentLap;
            GetComponent<SetUpLocalPlayer>().AddLap(Lap);
        }

        if(Lap >= lapsToWin)
        {
           GameObject WinPanel = GameObject.FindWithTag("MainCanvas").transform.GetChild(1).gameObject;
           WinPanel.SetActive(true);
           FinishSound.Play();
        }
        checkpointA = checkPointArray;
        Debug.Log("Lap" + Lap);

    }
}
