using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        //Detectamos si es el player el que entra en el collider.
        if (!other.CompareTag("Player"))
            return; //Si no es, no continua.


        if (transform == ConfigurationLaps.checkpointA[ConfigurationLaps.currentCheckpoint].transform)
        {
            //Se hace una comprobación para no pasarnos las cantidades de Checkpoints
            if (ConfigurationLaps.currentCheckpoint + 1 < ConfigurationLaps.checkpointA.Length)
            {
                //Si el checkpoint es = a 0 se agrega una vuelta.
                if (ConfigurationLaps.currentCheckpoint == 0)
                    ConfigurationLaps.currentLap++;
                ConfigurationLaps.currentCheckpoint++;
            }
            else
            {
                //Si ya no hay checkpoint regresan a 0.
                ConfigurationLaps.currentCheckpoint = 0;
            }
        }


    }
}