﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int nextLevelid;

    public void Next()
    {
        SceneManager.LoadScene(nextLevelid);
    }
}
