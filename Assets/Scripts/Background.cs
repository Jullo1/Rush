using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2.9f)
        {
            transform.position += Vector3.left * Time.deltaTime * 20f;
            if (transform.position.x <= -19)
                transform.position = Vector3.right * 19;
        }
    }
}
