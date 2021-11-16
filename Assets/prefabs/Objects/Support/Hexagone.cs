using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagone : MonoBehaviour
{
    [SerializeField] private Vector2 Coordinates;
    [SerializeField] Vector2 mapGeneratingoffset;

    public GameObject Ground;
    public GameObject Player;


    void Start()
    {

    }

    void Update()
    {
        
    }
    public void setCoordinates(Vector2 coordinates) { Coordinates = coordinates; }
    public Vector2 getCoordinates() { return Coordinates; }
    public Vector2 getmapGeneratingoffset() { return mapGeneratingoffset; }
    
}
