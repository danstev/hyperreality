using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAt : Ability
{
   public float scale = 2.0f;
   public GameObject projectile;

    public override void Use(GameObject i, Collider2D c, int dam)
    {

        float hi = (Input.mousePosition.x / Screen.width) - 0.5f;
        float vi = (Input.mousePosition.y / Screen.height) - 0.5f;

        Vector3 tempVecti = new Vector3(hi, vi, 0);
        tempVecti = tempVecti.normalized;

        Vector3 pos = transform.position;
        pos+= tempVecti;
            
        GameObject g = Instantiate(projectile, pos, Quaternion.identity);
        g.GetComponent<Rigidbody2D>().velocity = tempVecti * 10.0f;

    }



}
