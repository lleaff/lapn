﻿using UnityEngine;
using System.Collections;

public class SM : MonoBehaviour
{

    public static SM i = null; /* Scene manager instance */

	public Vector2 CellDimensions = new Vector2(5f, 5f);


    void Awake()
    {
        if (i == null) {
            i = this;
        } else if (i != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {

    }
}
