using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControl : Ability
{
    public float scale = 2.0f;

    public override void Use(GameObject i, Collider2D c, int dam)
    {
        c.enabled = true;
        Debug.Log(gameObject.name + " " + gameObject.transform.parent.name);

        float hi = (Input.mousePosition.x / Screen.width) - 0.5f;
        float vi = (Input.mousePosition.y / Screen.height) - 0.5f;

        Vector3 tempVecti = new Vector3(hi, vi, 0);
        tempVecti = tempVecti.normalized * scale * Time.deltaTime;
            
        i.transform.position += tempVecti;

        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(i.transform.parent.transform.position);
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        i.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 45));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
