using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundIntro : MonoBehaviour
{
    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        transform.position += Vector3.left * Time.deltaTime * 10f;

        if (transform.position.x <= -19)
            transform.position = Vector3.right * 19;

        if (timer > 2.5f)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
