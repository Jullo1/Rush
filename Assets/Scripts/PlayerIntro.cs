using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntro : MonoBehaviour
{
    void Awake()
    {

    }

    void Update()
    {
        transform.position -= Vector3.right * Time.deltaTime * 5f;
    }
}
