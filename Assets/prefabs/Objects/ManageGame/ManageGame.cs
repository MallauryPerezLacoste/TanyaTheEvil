using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour
{
    [SerializeField] private List<GameObject> Team0;
    [SerializeField] private List<GameObject> Team1;
    [SerializeField] private string tagCharacters;
    bool turnToPlay = false;
    public Camera camera;
    public float time;

    [SerializeField] private GameObject characterSelected;
    [SerializeField] private GameObject enemySelected;
    [SerializeField] private anyCharacter.enumAtion chosenAction=anyCharacter.enumAtion.actionNormale;
    

    [SerializeField]  private bool gameFinished = false;


    void Start()
    {
        Team0 = new List<GameObject>();
        Team1 = new List<GameObject>();

        
        StartCoroutine(game());
    }

   
    
    private bool getTeams()
    {
        Team0.Clear();
        Team1.Clear();

        GameObject[] allCharacters = GameObject.FindGameObjectsWithTag(tagCharacters);
        foreach (GameObject character in allCharacters)
        {
            if (character.GetComponent<anyCharacter>().getTeam())
            {
                Team1.Add(character);
            }
            else
            {
                Team0.Add(character);
            }
        }
        if (Team0.Count != 0 && Team1.Count!=0) return true;
        else return false;
    }

    private  IEnumerator game()
    {
        yield return 0;

        while (true && !gameFinished && getTeams())
        {
            Debug.Log("Team " + turnToPlay);

            //Select character to play
            yield return StartCoroutine(selectCharater());
            //select action
            yield return StartCoroutine(selectAction());
            Debug.Log("Action"+ chosenAction);
            //select target selectEnemy()
            yield return StartCoroutine(selectEnemy());
            //do action
            yield return StartCoroutine(doAction());
            Debug.Log(chosenAction+" Done");

            turnToPlay = !turnToPlay;
            yield return 0;
            
        }
        yield return StartCoroutine(end());
    }


    private IEnumerator selectCharater()
    {
        bool notDone= true;

        while (notDone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    GameObject objectHit = hitInfo.collider.gameObject;
                    if (objectHit.tag == tagCharacters)
                    {
                        if (objectHit.GetComponent<anyCharacter>().getTeam() == turnToPlay)
                        {
                            characterSelected= objectHit;
                            notDone = false;
                        }
                    }
                }
            }
            yield return 0;
        }
    }

    private IEnumerator selectEnemy()
    {
        bool notDone = true;

        while (notDone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    GameObject objectHit = hitInfo.collider.gameObject;
                    if (objectHit.tag == tagCharacters)
                    {
                        if (objectHit.GetComponent<anyCharacter>().getTeam() == !turnToPlay)
                        {
                            enemySelected = objectHit.GetComponent<anyCharacter>().getCurrentHexagone();
                            notDone = false;
                        }
                    }
                }
            }
            yield return 0;
        }
    }

    private IEnumerator selectAction()
    {
        yield return 0;
    }

    private IEnumerator doAction()
    {
        characterSelected.GetComponent<anyCharacter>().setAction(chosenAction);
        characterSelected.GetComponent<anyCharacter>().setSelectedGameObject(enemySelected);
        yield return StartCoroutine(action());
    }

    private IEnumerator action()
    {
        characterSelected.GetComponent<anyCharacter>().action();
        yield return StartCoroutine(actionFinished());
    }

    private IEnumerator actionFinished()
    {
        while (!characterSelected.GetComponent<anyCharacter>().actionFinished)
        {
            yield return 0;
        }
        yield return new WaitForSeconds(time);
    }


    private IEnumerator end()
    {
        Debug.Log("End");
        yield return 0;
    }
}
