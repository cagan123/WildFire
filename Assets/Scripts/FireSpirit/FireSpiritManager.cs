using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritManager : MonoBehaviour
{
    public static FireSpiritManager instance{get;private set;}

    public FireSpirit fireSpirit;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }

    }
}
