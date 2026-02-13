using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DetectionRing : MonoBehaviour
{
    private EnemyAI _enemyAI;
    private LineRenderer _line;
    public int segments = 50;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _enemyAI = GetComponent<EnemyAI>();

        _line.useWorldSpace = false;
        _line.positionCount = segments + 1;
        _line.startWidth = 0.1f;
        _line.endWidth = 0.1f;
        _line.loop = true;

        if (_line.material == null)
        {
            _line.material = new Material(Shader.Find("Sprites/Default"));
        }
    }

    void Update()
    {
        DrawCircle();
    }

    void DrawCircle()
    {
        float radius = _enemyAI.detectionRange;
        float angle = 0f;

        Color ringColor = (_enemyAI.currentState == EnemyAI.AIState.Chase) ? Color.red : Color.yellow;
        _line.startColor = ringColor;
        _line.endColor = ringColor;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            _line.SetPosition(i, new Vector3(x, -0.98f, z));

            angle += (360f / segments);
        }
    }
}