using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public string To;
    void Interact()
    {
        SceneManager.LoadSceneAsync(To);
    }
}
