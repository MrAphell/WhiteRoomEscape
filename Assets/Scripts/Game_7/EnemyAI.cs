using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// NavMesh alapú ellenséges MI, amely õrjárat és üldözés funkciókkal rendelkezik
public class EnemyAI : MonoBehaviour
{
    public enum AIState { Idle, Patrol, Chase }

    [Header("AI Állapot")]
    public AIState currentState = AIState.Patrol; // Az ellenség aktuális viselkedése

    [Header("Beállítások")]
    public float detectionRange = 7f; // Érzékelési távolság, amin belül üldözõbe veszi a játékost
    public float patrolSpeed = 2f;    // Sebesség õrjárat közben
    public float chaseSpeed = 4.5f;   // Sebesség üldözés közben
    public float stopWaitTime = 2f;   // Várakozási idõ az útpontoknál

    [Header("Vizuális Visszajelzés")]
    public GameObject alertIcon; // Üldözéskor megjelenõ ikon (pl. felkiáltójel)

    [Header("Útvonal")]
    public List<Transform> waypoints; // Az útvonalat kijelölõ pontok listája

    private NavMeshAgent _agent;      // A Unity NavMesh komponense a mozgáshoz
    private Transform _player;        // Referencia a játékoshoz
    private Transform _mainCamera;    // Referencia a kamerához (az ikon forgatásához)
    private int _currentWaypointIndex = 0; // Aktuális útpont sorszáma
    private float _waitTimer;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        // Játékos megkeresése tag alapján
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) _player = playerObj.transform;

        if (Camera.main != null) _mainCamera = Camera.main.transform;

        // Fix magasság beállítása (lebegés vagy süllyedés elkerülése)
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

        // Elsõ útpont kijelölése, ha van a listában
        if (waypoints.Count > 0)
        {
            _agent.SetDestination(waypoints[_currentWaypointIndex].position);
        }

        if (alertIcon != null) alertIcon.SetActive(false);
    }

    void Update()
    {
        if (_player == null) return;

        // Magasság folyamatos korrekciója a pályán
        if (transform.position.y != 1f)
        {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        // Állapot váltás logika
        if (distanceToPlayer <= detectionRange)
        {
            currentState = AIState.Chase; // Ha közel van a játékos, üldözzük
        }
        else if (currentState == AIState.Chase && distanceToPlayer > detectionRange + 2f)
        {
            currentState = AIState.Patrol; // Ha messzire ment, visszatérünk az õrjárathoz
            SetNextWaypoint();
        }

        // Figyelmeztetõ ikon kezelése
        if (alertIcon != null)
        {
            bool shouldShowAlert = (currentState == AIState.Chase);
            if (alertIcon.activeSelf != shouldShowAlert) alertIcon.SetActive(shouldShowAlert);

            // Billboard effekt: az ikon mindig a játékos kamerája felé nézzen
            if (alertIcon.activeSelf && _mainCamera != null)
            {
                alertIcon.transform.LookAt(alertIcon.transform.position + _mainCamera.rotation * Vector3.forward,
                                           _mainCamera.rotation * Vector3.up);
            }
        }

        // Mozgás végrehajtása állapot szerint
        switch (currentState)
        {
            case AIState.Patrol: PatrolBehavior(); break;
            case AIState.Chase: ChaseBehavior(); break;
        }
    }

    // Õrjárat viselkedés: pontról pontra halad, közben várakozik
    private void PatrolBehavior()
    {
        if (!_agent.isOnNavMesh) return;
        _agent.speed = patrolSpeed;

        // Ha elértük az útpontot (vagy közel vagyunk hozzá)
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= stopWaitTime)
            {
                SetNextWaypoint();
                _waitTimer = 0;
            }
        }
    }

    // Következõ útpont kijelölése ciklikusan a listából
    private void SetNextWaypoint()
    {
        if (waypoints.Count == 0) return;
        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Count;
        _agent.SetDestination(waypoints[_currentWaypointIndex].position);
    }

    // Üldözés viselkedés: folyamatosan a játékos pozíciója felé navigál
    private void ChaseBehavior()
    {
        _agent.speed = chaseSpeed;
        _agent.SetDestination(_player.position);
    }

    // Ha az ellenség fizikailag hozzáér a játékoshoz
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ELKAPVA!");
            RestartGame();
        }
    }

    // A pálya újraindítása elkapás esetén
    private void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Fejlesztõi segítség: az érzékelési kör vizuális megjelenítése a Scene ablakban
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 gizmoPos = new Vector3(transform.position.x, 1f, transform.position.z);
        Gizmos.DrawWireSphere(gizmoPos, detectionRange);
    }
}