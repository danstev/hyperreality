using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpin : Ability
{

    public void Use(GameObject i, Collider2D c, int dam)
    {
        c.enabled = true;

            //POS
            i.transform.position = new Vector3(transform.position.x + (Mathf.Cos(TimerTemp * 15) * 2) ,transform.position.y + (Mathf.Sin(TimerTemp * 15) * 2) , 0);
    }


}
