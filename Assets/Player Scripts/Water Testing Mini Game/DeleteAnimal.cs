using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeleteAnimal : MonoBehaviour
{
    public float destroyTime = 2f; // time in seconds before its is deleted

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
