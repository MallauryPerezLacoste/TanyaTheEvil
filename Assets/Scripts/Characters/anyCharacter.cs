using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anyCharacter : MonoBehaviour
{
    public enum enumAtion {deplacement,actionNormale,attaqueSpeciale,rien};

    //Character stats
    [SerializeField] protected float LifePoints;
    [SerializeField] protected int SpeedMove;
    [SerializeField] protected int SpeedRotation;
    [SerializeField] protected bool StuckUnderFire;
    [SerializeField] protected bool Team;

    //Actions
    [SerializeField] protected bool TurnToPlay;
    [SerializeField] protected enumAtion Action;
    [SerializeField] protected GameObject selectedGameObject;
    [SerializeField] public bool actionFinished;

    //Emplacement
    [SerializeField] protected GameObject currentHexagone;
    [SerializeField] protected GameObject gameObject_attaqueNormale;
    [SerializeField] protected GameObject gameObject_attaqueSpeciale;

    public void action()
    {
        if (StuckUnderFire)
        {
            processStuckUnderFire();
            return;
        }
        switch (Action)
        {
            case enumAtion.deplacement:
                deplacement(selectedGameObject);
                break;
            case enumAtion.actionNormale:
                attqueNormale(selectedGameObject);
                break;
            case enumAtion.attaqueSpeciale:
                attaqueSpeciale(selectedGameObject);
                break;
            case enumAtion.rien:
                actionFinished = true;
                break;
        }
        TurnToPlay = false;
        return ;
    }


    protected virtual void deplacement(GameObject positionToGO) { }
    protected virtual void attqueNormale(GameObject ennemy) { }
    protected virtual void attaqueSpeciale(GameObject enemy) { }
    protected virtual void mort(){}

    public void set_LifePoints(float LifePoints) { this.LifePoints = LifePoints; }
    public float get_LifePoints() {return LifePoints; }

    public void setActionFinished(bool value) { actionFinished = value; }

    public void setStuckUnderFire() { StuckUnderFire = true; }

    public void processStuckUnderFire()
    {
        TurnToPlay = false;
        actionFinished = true;
        StuckUnderFire = false;
    }

    public void setCurrentHexagone(GameObject CurrentHexagone)
    {
        currentHexagone = CurrentHexagone;
    }

    public GameObject getCurrentHexagone() { return currentHexagone; }

    public void setTeam(bool team)
    {
        Team = team;
    }

    public bool getTeam()
    {
        return Team;
    }

    public void setAction(enumAtion action) { Action = action; }

    public void setSelectedGameObject(GameObject SelectedGameObject) { selectedGameObject = SelectedGameObject; }
}
