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
    // GENERALS VARIABLES //
    [SerializeField] int life = 10;             // Vita, numero di hit da subire.
    [SerializeField] int maxLife = 10;
    [SerializeField] float maxDistanceToBase;   // Massima distanza consentita dalla base.
    [SerializeField] GameObject PlayerBase;     // Istanza della main base

    // MOVEMENTS //
    [SerializeField] float moveSpeed;           // Fattore moltiplicativo velocità.

    // MAIN WEAPON //
    [SerializeField] GameObject mainWeaponProjectile;   // Istanza proiettile da usare della main weapon.
    [SerializeField] float mainWeaponFireRate;          // Rateo di fuoco della main weapon. in RPM (colpi al min).
    Coroutine MainWeaponFireCoroutine;                  // Referenza alla coroutine di shooting della main weapon.
    [SerializeField] GameObject shootingPoint;          // where to instantiate object.

    // UI REFERENCES //
    [SerializeField] Text LifeUI;
    [SerializeField] GameObject maxDistanceUI;
    [SerializeField] Text abortMissionUI;
    Coroutine abortMission = null;
    [SerializeField] int returnMaxTime = 10;            // abort mission timer

    [SerializeField] int cashPerHeal;

    // ALTRE VARIABILI //


    void Start()
    {
        UpdateLifeUI(); // Inizializza l'UI della vita
    }

    void Update()
    {
        Move();
        MainWeaponFireInput();
        checkMaxDistance();
    }

    // Funzione utilizzata in Update.
    // Move() permette di spostare il player nell'asse delle x e y.
    // Inoltre gestisce la rotazione
    private void Move()
    {
        // X & Y MOVEMENTS ----------------------------------------------------------------------
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        // Debug.Log(deltaX + " - " + deltaY);

        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);
        //------------------------------------------------------------------------------------------
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
        LifeUI.text = "LIFE: " + life+"/"+maxLife;
    }

    // Controlla la distanza del player rispetto alla base, nel caso ecceda, attiva il processo di "abortMission"
    void checkMaxDistance()
    {
        float distance = Mathf.Abs(Vector2.Distance(PlayerBase.transform.position, transform.position));

        if (distance > maxDistanceToBase)
        {
            maxDistanceUI.SetActive(true);
            if (abortMission == null)
            {
                abortMission = StartCoroutine(AbortMission());
            }
        }
        else
        {
            maxDistanceUI.SetActive(false);
            if(abortMission != null)
            {
                StopCoroutine(abortMission);
                abortMissionUI.text = "" + returnMaxTime;
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
            abortMissionUI.text = ""+ (returnMaxTime - i);
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
