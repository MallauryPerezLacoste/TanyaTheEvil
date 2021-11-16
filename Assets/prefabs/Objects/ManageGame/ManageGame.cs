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



    void Start()
    {
        Team0 = new List<GameObject>();
        Team1 = new List<GameObject>();

        getTeams();
        StartCoroutine(game());
        //StartCoroutine(gameEnd());
    }

   
    
    private void getTeams()
    {
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
    }

    private  IEnumerator game()
    {
        yield return 0;
        Debug.Log("game");

        while (true)
        {
            teamToPlay(turnToPlay);
            yield return StartCoroutine(selectCharater());
            //select action
            //select target
            turnToPlay = !turnToPlay;
            yield return 0;
        }
    }


    private IEnumerator selectCharater()
    {
        bool notDone= true;

        while (notDone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Boutton appuyer");
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    Debug.Log(hitInfo.collider.gameObject.name);
                }
            }
            yield return 0;
        }
    }

    /*
    private IEnumerator selectCharater()
    {
        GameObject anycharcter = null;
        while(anycharcter == null || anycharcter.GetComponent<anyCharacter>().getTeam() != turnToPlay)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);
            Debug.Log(hit.transform.tag);

            yield return 0;
        }
        

            yield return 0;
    }*/

    private void teamToPlay(bool team)
    {
        Debug.Log("Team " + team);
    }

    private IEnumerator gameEnd()
    {
        Team0.Clear();
        Team1.Clear();
        getTeams();
        while (Team0.Count != 0 && Team1.Count != 0)
        {
            yield return 0;
        }
        Debug.Log("End");
    }
}
