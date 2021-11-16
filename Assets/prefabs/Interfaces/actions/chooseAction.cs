using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseAction : MonoBehaviour
{
    [SerializeField] public anyCharacter.enumAtion chosenAction;
    [SerializeField] public bool Selected = false;

    public void Action(anyCharacter.enumAtion action) { chosenAction = action; }

    public void attaqueNormale() { chosenAction = anyCharacter.enumAtion.actionNormale;Selected = true; }
    public void attaqueSpeciale() { chosenAction = anyCharacter.enumAtion.attaqueSpeciale; Selected = true; }
    public void Deplacement() { chosenAction = anyCharacter.enumAtion.deplacement; Selected = true; }
    public void Rien() { chosenAction = anyCharacter.enumAtion.rien; Selected = true; }
}
