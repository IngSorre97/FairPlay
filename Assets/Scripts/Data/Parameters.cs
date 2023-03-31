using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    public static Parameters Instance;
    public float attackDuration = 1.0f;

    public float paladinWait;

    private void Start()
    {
        Instance = this;
    }
}
