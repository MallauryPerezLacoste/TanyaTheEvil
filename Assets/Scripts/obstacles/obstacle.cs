using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    [SerializeField] public GameObject currentSupport;

    public virtual float effect(float damage) { return damage; }
    public virtual void damage() {  }
}
