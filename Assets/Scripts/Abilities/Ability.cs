using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string AbilityName;
    public string AbilityDescription;
    public int AblityDamage = 0;
    public float Timer = 0;
    public float TTime = 0;
    public float TimerTemp = 0;

    void Activate()
    {

    }

    void Update()
    {
        TimerTemp -= Time.deltaTime;
        TTime += Time.deltaTime;
    }


}