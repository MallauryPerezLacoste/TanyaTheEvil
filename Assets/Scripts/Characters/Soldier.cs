using System.Collections;
using UnityEngine;
public class Soldier : anyCharacter
{

    private Soldier_Animation soldier_Animation;

    [SerializeField] private Vector3 bulletOffset ;

    private void Start()
    {
        gameObject.transform.position = new Vector3(currentHexagone.transform.position.x,
                                                        gameObject.transform.position.y,
                                                         currentHexagone.transform.position.z);
        currentHexagone.GetComponent<Hexagone>().Player = gameObject;
        soldier_Animation = gameObject.GetComponentInParent<Soldier_Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LifePoints<=0) StartCoroutine(die());

        if (TurnToPlay)
        {
            action();
        }
    }


    protected override  void deplacement(GameObject positionToGO) {
        if (positionToGO.GetComponent<Hexagone>().Player != null)
        {
            manageAlreadyCharacter();
            return;
        }
        //Change hexagone anyCharacter
        currentHexagone.GetComponent<Hexagone>().Player = null;
        currentHexagone = positionToGO;
        positionToGO.GetComponent<Hexagone>().Player = gameObject;
        StartCoroutine(Move(positionToGO.transform.position));
    }
    protected override void attqueNormale(GameObject ennemy) {StartCoroutine(Feu(ennemy));}
    protected override void attaqueSpeciale(GameObject ennemy) { StartCoroutine(grenade(ennemy)); }
    protected override void mort() {StartCoroutine(die());}



    private IEnumerator die()
    {
        soldier_Animation.die();
        yield return StartCoroutine(waitAnimation());
        apresMort();
        actionFinished = true;
    }
    private IEnumerator Orientation(Vector3 destination)
    {
        Vector3 movementDirection = (destination - transform.position).normalized;
        if (movementDirection == new Vector3(0, 0, 0))
        {
            movementDirection = transform.forward.normalized;
        }
        while (transform.forward.normalized != movementDirection)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, SpeedRotation * Time.deltaTime);
            yield return 0;
        }
    }
    private IEnumerator Walk(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        soldier_Animation.walkOn();
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * SpeedMove);
            yield return 0;
        }
        soldier_Animation.walkOff();
    }
    private IEnumerator Move(Vector3 destination)
    {
        yield return StartCoroutine(Orientation(destination));
        yield return StartCoroutine(Walk(destination));
        actionFinished = true;
    }
   
    private IEnumerator Feu(GameObject target)
    {
        yield return StartCoroutine(Orientation(target.transform.position));
        soldier_Animation.preFeu();
        yield return StartCoroutine(waitAnimation());

        GameObject bullet = gameObject_attaqueNormale;
        bullet.GetComponent<weapon>().setTarget(target);
        Instantiate(bullet, positionBullet(), transform.rotation);
        

        soldier_Animation.feu();
        yield return StartCoroutine(waitAnimation());
        actionFinished = true;
    }
    private IEnumerator grenade(GameObject target)
    {
        yield return StartCoroutine(Orientation(target.transform.position));
        soldier_Animation.preGrenade();
        yield return StartCoroutine(waitAnimation());

        GameObject grenade = gameObject_attaqueSpeciale;
        grenade.GetComponent<weapon>().setTarget(target);
        Instantiate(grenade, positionGrenade(), Quaternion.identity);
        

        soldier_Animation.grenade();
        yield return StartCoroutine(waitAnimation());
        actionFinished = true;
    }


    private IEnumerator waitAnimation(){yield return new WaitForSeconds(soldier_Animation.getAnimator().GetCurrentAnimatorStateInfo(0).length * soldier_Animation.getAnimator().GetCurrentAnimatorStateInfo(0).speed);}
    private Vector3 positionBullet()
    {
        Vector3 position = transform.position+transform.forward;
        
        position.y = bulletOffset.y;
        
        return position;
    }
    private Vector3 positionGrenade()
    {
        return transform.position;
    }
    private void apresMort()
    {
        Destroy(gameObject);
    }

    private void manageAlreadyCharacter()
    {
        //TODO:en fonction: coup de baionnette?
        actionFinished = true;
    }
}
