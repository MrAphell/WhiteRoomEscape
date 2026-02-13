using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public enum AIState { Idle, Patrol, Chase }

    [Header("AI Állapot")]
    public AIState currentState = AIState.Patrol;

    [Header("Beállítások")]
    public float detectionRange = 7f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4.5f;
    public float stopWaitTime = 2f;

    [Header("Vizuális Visszajelzés")]
    public GameObject alertIcon;

    [Header("Útvonal")]
    public List<Transform> waypoints;

    private NavMeshAgent _agent;
    private Transform _player;
    private Transform _mainCamera;
    private int _currentWaypointIndex = 0;
    private float _waitTimer;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) _player = playerObj.transform;

        if (Camera.main != null) _mainCamera = Camera.main.transform;

        // Kényszerített Y=1 magasság
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

        if (waypoints.Count > 0)
        {
            _agent.SetDestination(waypoints[_currentWaypointIndex].position);
        }

        if (alertIcon != null) alertIcon.SetActive(false);
    }

    void Update()
    {
        if (_player == null) return;

        // Magasság korrekció
        if (transform.position.y != 1f)
        {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        // --- Állapot váltás
        if (distanceToPlayer <= detectionRange)
        {
            currentState = AIState.Chase;
        }
        else if (currentState == AIState.Chase && distanceToPlayer > detectionRange + 2f)
        {
            currentState = AIState.Patrol;
            SetNextWaypoint();
        }

        // --- Ikon
        if (alertIcon != null)
        {
            bool shouldShowAlert = (currentState == AIState.Chase);
            if (alertIcon.activeSelf != shouldShowAlert) alertIcon.SetActive(shouldShowAlert);

            if (alertIcon.activeSelf && _mainCamera != null)
            {
                alertIcon.transform.LookAt(alertIcon.transform.position + _mainCamera.rotation * Vector3.forward,
                                           _mainCamera.rotation * Vector3.up);
            }
        }

        // --- Mozgás
        switch (currentState)
        {
            case AIState.Patrol: PatrolBehavior(); break;
            case AIState.Chase: ChaseBehavior(); break;
        }
    }

    private void PatrolBehavior()
    {
        if (!_agent.isOnNavMesh) return;
        _agent.speed = patrolSpeed;

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

    private void SetNextWaypoint()
    {
        if (waypoints.Count == 0) return;
        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Count;
        _agent.SetDestination(waypoints[_currentWaypointIndex].position);
    }

    private void ChaseBehavior()
    {
        _agent.speed = chaseSpeed;
        _agent.SetDestination(_player.position);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ELKAPVA!");
            RestartGame();
        }
    }

    private void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 gizmoPos = new Vector3(transform.position.x, 1f, transform.position.z);
        Gizmos.DrawWireSphere(gizmoPos, detectionRange);
    }
}