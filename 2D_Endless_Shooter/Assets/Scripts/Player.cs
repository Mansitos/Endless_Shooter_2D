using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Main script per la gestione dell'entità PLAYER.
 * 
 */
public class Player : MonoBehaviour
{
    // VARAIBILI GENERALE //
    public int life = 10;                    // Vita dell'entità player (hitpoints)
    public int maxLife = 10;                 // Vita massima del player
    public float maxAllowedDistanceToBase;   // Massima distanza consentita dalla base.
    public GameObject PlayerBase;            // Istanza della main base
    public int cashPerHeal;                  // Quanto costa curare un hit-point

    // VARIABILI DI MOVIMENTO //
    public float movementSpeed;           // Fattore moltiplicativo della velocità velocità.

    // MAIN WEAPON //
    public GameObject mainWeaponProjectile;   // Prefab del proiettile da usare con la main weapon.
    public float mainWeaponFireRate;          // Rateo di fuoco della main weapon. in RPM (colpi al min).
    Coroutine MainWeaponFireCoroutine;        // Reference alla coroutine di shooting della main weapon.
    public GameObject shootingPoint;          // Punto dove istanziare i proiettili sparati

    // Variabili relative alla meccanica AbortMission (massima distanza dalla base)
    Coroutine abortMission = null;
    public int returnMaxTime = 10;            // tempo a disposizione per il player di tornare in missione prima del game over



    // ALTRE VARIABILI //
    GameManager gameManager;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        UpdateLifeUI(); // Inizializza l'UI della vita
    }

    void Update()
    {
        Move();
        MainWeaponFireInput();
        checkMaxDistance();
    }

    // Funzione utilizzata in Update per la gestione del movement system.
    // Move() permette di spostare il player nell'asse delle x e y.
    // Inoltre gestisce la rotazione della visuale
    private void Move()
    {
        // X & Y MOVEMENTS ----------------------------------------------------------------------
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);


        // Z ROTATIONS MOVEMENTS // FOLLOW MOUSE CURSOR
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Coroutine del firing.
    IEnumerator AutomaticFire()
    {
        while (true)
        {
            Instantiate(mainWeaponProjectile, shootingPoint.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(60 / mainWeaponFireRate);
        }
    }

    // Funzione di gestione del processo di fuoco. Rileva gli input e starta/stoppa la coroutine di fuoco.
    private void MainWeaponFireInput()
    {
        if (Input.GetButtonDown("MainWeaponFire"))
        {
            MainWeaponFireCoroutine = StartCoroutine(AutomaticFire());
        }
        if (Input.GetButtonUp("MainWeaponFire"))
        {
            StopCoroutine(MainWeaponFireCoroutine);
        }
    }

    // Rileva gli HIT dai proiettili nemici. Controlla inoltre la vita ad ogni danno subito.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyFiring")
        {
            life = life - collision.gameObject.GetComponent<Bullet>().GetDamageValue();
            UpdateLifeUI();
        }
        checkLife();
    }

    // Update Life UI
    public void UpdateLifeUI()
    {
        gameManager.getMainHUDManager().updatePlayerLifeUI(life, maxLife);
    }

    // Controlla la distanza del player rispetto alla base, nel caso ecceda, attiva il processo di "abortMission"
    void checkMaxDistance()
    {
        float distance = Mathf.Abs(Vector2.Distance(PlayerBase.transform.position, transform.position));

        if (distance > maxAllowedDistanceToBase)
        {
            gameManager.getMainHUDManager().getDistanceReachedUI().SetActive(true);
            if (abortMission == null)
            {
                abortMission = StartCoroutine(AbortMission());
            }
        }
        else
        {
            gameManager.getMainHUDManager().getDistanceReachedUI().SetActive(false);
            if (abortMission != null)
            {
                StopCoroutine(abortMission);
                gameManager.getMainHUDManager().updateAbortMissionTimer(returnMaxTime);
                abortMission = null;
            }
        }
    }

    //Coroutine AbortMission -> timer e distruzione del player a fine timer
    IEnumerator AbortMission()
    {
        for(int i=0; i <= 10; i++)
        {
            yield return new WaitForSeconds(1);
            gameManager.getMainHUDManager().updateAbortMissionTimer(returnMaxTime - i);
        }

        Debug.Log("MissionAborted!");
        Destroy(this.gameObject);
    }

    //Controlla la vita, uccide il player se <= 0
    void checkLife()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void IncreaseMaxLife(int value)
    {
        maxLife = maxLife + value;
        UpdateLifeUI();
    }

    public int getLife()
    {
        return life;
    }

    public int getMaxLife()
    {
        return maxLife;
    }

    public int getPricePerHeal()
    {
        return cashPerHeal;
    }

    public void increaseLife(float value)
    {
        life = life + (int) value;
        UpdateLifeUI();
    }

    public void setPercentageLife(int percentage)
    {
        int newlife = (int)(maxLife * ((float)(percentage) / 100));
        life = newlife+1;
        if(life >= maxLife)
        {
            life = maxLife;
        }
        UpdateLifeUI();
    }
}
