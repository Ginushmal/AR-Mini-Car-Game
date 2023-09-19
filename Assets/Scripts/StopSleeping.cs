using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSleeping : MonoBehaviour
{

        void Awake() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}


