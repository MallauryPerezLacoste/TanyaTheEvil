using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField] protected GameObject Target;

    public void setTarget(GameObject Target) { this.Target = Target; }
    public GameObject getTarget() { return Target; }

    protected virtual void effect() {  }
}
