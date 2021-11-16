using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_Animation : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void preGrenade() { animator.SetBool("Grenade", true); updateAnimator(); }
    public void grenade() { animator.SetBool("Grenade", false); updateAnimator(); }
    public void walkOn() { animator.SetBool("Walk", true); updateAnimator(); }
    public void walkOff() { animator.SetBool("Walk", false); updateAnimator(); }
    public void die() { animator.SetBool("Die", true); updateAnimator(); }
    public void preFeu() { animator.SetBool("Feu", true); updateAnimator(); }
    public void feu() { animator.SetBool("Feu", false); updateAnimator(); }

    public Animator getAnimator() { return animator; }
    private void updateAnimator() { animator.Update(0); }

}
