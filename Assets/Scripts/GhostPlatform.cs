using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    public Color lifeDimColor;
    public Color deathDimColor;
    public bool LifeOrDeathGhost;
    public Collider platformCollider;
    public Material platformMat;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        platformMat = this.GetComponent<MeshRenderer>().material;
        platformCollider = this.GetComponent<Collider>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
