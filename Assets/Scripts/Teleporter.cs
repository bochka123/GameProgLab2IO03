using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            var sceneNumber = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(1-sceneNumber);
        }
    }
}
