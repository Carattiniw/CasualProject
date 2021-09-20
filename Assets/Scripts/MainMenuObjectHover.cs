using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuObjectHover : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public Vector3 waypointOne;
    public Vector3 waypointTwo;
    public bool isLiquid;
    public bool isSolid;
    private bool facingRight = true;
    private bool waitForLeft = false;
    private bool waitForRight = false;


    void Update()
    {
        transform.position = Vector3.Lerp(waypointOne, waypointTwo, Mathf.PingPong(Time.time * speed, 1.0f));

        if (isSolid == true)
        {
            Debug.Log("rolling");
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.PingPong(Time.time * rotateSpeed, 90.0f));
        }

        if (facingRight == true && isLiquid == true)
        {
            if (waitForLeft == true)
            {
                return;
            }
            //Debug.Log("flip right");
            StartCoroutine(flipSprite());
            waitForLeft = true;
            waitForRight = false;
        }
        else if (facingRight == false && isLiquid == true)
        {
            if (waitForRight == true)
            {
                return;
            }
            //Debug.Log("flip left");
            StartCoroutine(flipSprite());
            waitForRight = true;
            waitForLeft = false;
        }
    }

    IEnumerator flipSprite()
    {
        yield return new WaitForSeconds(0.5f);
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
