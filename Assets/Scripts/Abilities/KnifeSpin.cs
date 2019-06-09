using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpin : Ability
{

    public float scale = 2.0f;

    public override void Use(GameObject i, Collider2D c, int dam)
    {
        Debug.Log(gameObject.name + " " + gameObject.transform.parent.name);
        c.enabled = true;

        transform.position = new Vector3(transform.position.x + (Mathf.Cos(TTime * 15) * scale) ,transform.position.y + (Mathf.Sin(TTime * 15) * scale) , 0);
    }


}
