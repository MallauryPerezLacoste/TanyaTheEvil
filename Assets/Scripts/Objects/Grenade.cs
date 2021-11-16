using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : weapon
{
    [SerializeField] private float damage;
    [SerializeField] private ParticleSystem particules_Explosion;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float radius;
    [SerializeField] private float offsetDamage;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(grenade());
    }
    private IEnumerator grenade()
    {
        yield return StartCoroutine(launch(Target.transform.position));
        applyDamage();
        Explosion();
    }

    private void applyDamage()
    {
        List<GameObject> listHex = getHexZone();

        foreach(GameObject g in listHex)
        {
            
            if (g.GetComponent<Renderer>() != null)
            {
                g.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            }
            else { Debug.Log("pas de material  "+ (listHex.Count).ToString());  }
            
            if (g.GetComponent<Hexagone>().Player != null)
            {
                DamagePlayer(g.GetComponent<Hexagone>().Player, (Target.transform.position - g.transform.position).sqrMagnitude);
            }

            if (g.GetComponent<Hexagone>().Ground != null)
            {
                DamageGround(g.GetComponent<Hexagone>().Ground);
            }
        }
        return;
    }

    private void DamagePlayer(GameObject enemy,float distance)
    {
        float pDamage= (float)(Mathf.Pow((1-distance/radius),3)+ offsetDamage);
        enemy.GetComponent<anyCharacter>().set_LifePoints(enemy.GetComponent<anyCharacter>().get_LifePoints() - pDamage);
        effectEnemy(enemy);
    }
    private void DamageGround(GameObject ground)
    {
        return;
    }
    private void effectEnemy(GameObject enemy)
    {
        enemy.GetComponent<anyCharacter>().setStuckUnderFire();
    }
    private IEnumerator launch(Vector3 destination)
    {
        destination.y = transform.position.y;
        Vector3 direction = destination - transform.position;
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * initialSpeed);
            yield return 0;
        }
    }


    private void Explosion()
    {
        Instantiate(particules_Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private List<GameObject> getHexZone()
    {

        List<GameObject> listeHex= new List<GameObject>();
        Vector2 p = Target.GetComponent<Hexagone>().getCoordinates();


        listeHex.Add(Target);

        //get all support
        GameObject[] allsuports = GameObject.FindGameObjectsWithTag("Support");

        //get in a radius
        foreach(GameObject g in allsuports)
        {
            if((Target.transform.position - g.transform.position).sqrMagnitude < radius)
            {
                listeHex.Add(g);
            }
        }

        return listeHex;
    }
}
