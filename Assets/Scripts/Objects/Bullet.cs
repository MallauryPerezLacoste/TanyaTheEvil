using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : weapon
{
    
    [SerializeField]  private float damage;
    [SerializeField] private ParticleSystem particules_Feu;
    [SerializeField] private float SpeedMove;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool once = true;
    // Update is called once per frame
    void Update()
    {
        if (once)
        {
            StartCoroutine(bullet());
            once = false;
        }
        
    }

    
    public void applyDamage()
    {
        GameObject enemy = Target.GetComponent<Hexagone>().Player;

        if (enemy != null)
        {
            enemy.GetComponent<anyCharacter>().set_LifePoints(enemy.GetComponent<anyCharacter>().get_LifePoints() - damage);
        }
    }

    public void animationFeu()
    {

    }   
    private IEnumerator bullet()
    {
        animationFeu();
        yield return StartCoroutine(Move(Target.transform.position));
        applyDamage();
        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 destination)
    {
        destination.y = transform.position.y;
        Vector3 direction = destination - transform.position;
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * SpeedMove);
            yield return 0;
        }
    }
}
