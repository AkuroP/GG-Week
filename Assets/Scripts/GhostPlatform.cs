using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    public Color lifeDimColor;
    public Color deathDimColor;
    public bool LifeOrDeathGhost;
    private SpriteRenderer platformSR;
    public Collider2D platformCollider;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        platformSR = this.GetComponent<SpriteRenderer>();
        platformCollider = this.GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
