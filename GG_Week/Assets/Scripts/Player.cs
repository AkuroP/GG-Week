using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject lifeDimension;
    public GameObject deathDimension;
    
    [Header("DIMENSION VIE/MORT -> TRUE/FALSE")]
    [SerializeField]
    private bool lifeDeath;

    // Start is called before the first frame update
    void Start()
    {
        lifeDeath = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(lifeDeath)
            {
                lifeDimension.SetActive(false);
                deathDimension.SetActive(true);
                lifeDeath = false;
            }
            else
            {
                lifeDimension.SetActive(true);
                deathDimension.SetActive(false);
                lifeDeath = true;
            }
        }
    }
}
