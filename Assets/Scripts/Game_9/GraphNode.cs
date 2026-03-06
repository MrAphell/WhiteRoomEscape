using System.Collections.Generic;
using UnityEngine;

// A lézeres hálózat egyetlen elemét kezelõ osztály
public class GraphNode : MonoBehaviour, IInteractable
{
    // A kocka lehetséges típusai: forrástól a logikai kapukon át a végpontig
    public enum NodeType { Start, Relay, Splitter, Switch, OR, AND, XOR, End }

    // Az interfész hívja meg interakciókor
    public void Interact() => InteractAndRotate();

    [Header("Csomópont Beállítások")]
    public string nodeName = "Csomópont";
    public NodeType type = NodeType.Relay;
    public LayerMask interactableLayer; // Ezen a rétegen keressük a többi kockát sugárral

    [Header("Állapot (Ne állítsd át)")]
    public bool isPowered = false;     // Jelzi, ha kap energiát
    public bool isOverloaded = false;  // Jelzi, ha hiba történt a logikában
    public int activeInputs = 0;       // Hány lézer mutat jelenleg erre a kockára
    public bool isSwitchOn = true;     // Kapcsoló állapota

    [Header("Forgatás")]
    public float rotationSpeed = 10f;

    [HideInInspector] public List<GraphNode> outgoingNodes = new List<GraphNode>(); // Kockák, amik felé továbbítjuk a fényt

    private Quaternion _targetRotation;
    private List<LineRenderer> _lineRenderers = new List<LineRenderer>(); // A lézersugarak vizuális elemei

    void Start()
    {
        _targetRotation = transform.rotation;

        // Kiszámoljuk, hány kimenõ lézersugárra van szüksége a kockának a típusa alapján
        int neededLines = (type == NodeType.Splitter) ? 3 : (type == NodeType.End ? 0 : 1);

        for (int i = 0; i < neededLines; i++)
        {
            // Új objektum és LineRenderer létrehozása minden lézersugárhoz
            GameObject lineObj = new GameObject("LaserBeam_" + i);
            lineObj.transform.SetParent(this.transform);
            lineObj.transform.localPosition = Vector3.zero;

            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.useWorldSpace = true;
            lr.enabled = false;

            _lineRenderers.Add(lr);
        }
    }

    void Update()
    {
        // Sima, fokozatos elforgatás a célirányba
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * rotationSpeed);

        // A lézersugarak hosszának és színének folyamatos frissítése
        UpdateVisualsRealtime();
    }

    // Szöveges visszajelzés a játékosnak, amikor ránéz a kockára
    public string GetPrompt()
    {
        if (type == NodeType.Start) return "Energy Source (Active)";
        if (type == NodeType.End) return "Energy Receiver";
        return $"{nodeName} ({type})\n[E] Rotate";
    }

    // Interakció: a kapcsolót átváltja, a többi típust pedig 90 fokkal elforgatja
    public void InteractAndRotate()
    {
        if (type == NodeType.Start || type == NodeType.End) return;
        if (type == NodeType.Switch) isSwitchOn = !isSwitchOn;
        else _targetRotation *= Quaternion.Euler(0, 90f, 0);

        // Szólunk a központi rendszernek, hogy számolja újra a hálózatot
        if (GraphManager.Instance != null) GraphManager.Instance.ScheduleEvaluation();
    }

    // Raycast segítségével megkeresi, melyik kockát találja el a lézer
    public void FindNeighbors()
    {
        outgoingNodes.Clear();
        if (type == NodeType.End) return;

        // Ha a kapcsoló ki van kapcsolva, nem keresünk szomszédot
        if (type == NodeType.Switch && !isSwitchOn)
        {
            outgoingNodes.Add(null);
            return;
        }

        // Irányok meghatározása
        List<Vector3> logicalDirections = new List<Vector3>();
        if (type == NodeType.Splitter)
        {
            logicalDirections.Add(_targetRotation * Vector3.forward);
            logicalDirections.Add(_targetRotation * Vector3.right);
            logicalDirections.Add(_targetRotation * Vector3.left);
        }
        else
        {
            logicalDirections.Add(_targetRotation * Vector3.forward);
        }

        foreach (var dir in logicalDirections)
        {
            Ray ray = new Ray(transform.position, dir);
            if (Physics.Raycast(ray, out RaycastHit hit, 15f, interactableLayer))
            {
                GraphNode target = hit.collider.GetComponent<GraphNode>();
                if (target != null && target != this) outgoingNodes.Add(target);
                else outgoingNodes.Add(null);
            }
            else outgoingNodes.Add(null);
        }
    }

    // A lézersugarak színének és hosszának kezelése
    private void UpdateVisualsRealtime()
    {
        if (type == NodeType.End) return;

        // Szín meghatározása: zöld ha van áram, piros ha nincs, narancs ha zárlat van
        Color beamColor = isPowered ? Color.green : Color.red;
        if (isOverloaded) beamColor = new Color(1f, 0.5f, 0f);

        Color solidRed = Color.red;
        Color fadingRed = new Color(1f, 0f, 0f, 0f);

        if (type == NodeType.Switch && !isSwitchOn)
        {
            foreach (var lr in _lineRenderers) lr.enabled = false;
            return;
        }

        List<Vector3> currentDirections = new List<Vector3>();
        if (type == NodeType.Splitter)
        {
            currentDirections.Add(transform.forward);
            currentDirections.Add(transform.right);
            currentDirections.Add(-transform.right);
        }
        else
        {
            currentDirections.Add(transform.forward);
        }

        for (int i = 0; i < _lineRenderers.Count; i++)
        {
            _lineRenderers[i].enabled = true;
            _lineRenderers[i].SetPosition(0, transform.position);

            Ray ray = new Ray(transform.position, currentDirections[i]);
            if (Physics.Raycast(ray, out RaycastHit hit, 15f, interactableLayer))
            {
                Vector3 perfectHit = hit.point;
                perfectHit.y = transform.position.y;
                _lineRenderers[i].SetPosition(1, perfectHit);

                GraphNode target = hit.collider.GetComponent<GraphNode>();
                if (target != null && target != this)
                {
                    _lineRenderers[i].startColor = beamColor;
                    _lineRenderers[i].endColor = beamColor;
                }
                else
                {
                    // Ha falat vagy mást ér, elhalványul a vége (piros)
                    _lineRenderers[i].startColor = solidRed;
                    _lineRenderers[i].endColor = fadingRed;
                }
            }
            else
            {
                // Ha a semmibe lõ, fix távolság után elhalványul
                _lineRenderers[i].SetPosition(1, transform.position + currentDirections[i] * 5f);
                _lineRenderers[i].startColor = solidRed;
                _lineRenderers[i].endColor = fadingRed;
            }
        }
    }

    // Részletes szöveg kiírása
    // Információs szöveg összeállítása a célkereszthez (UI-hoz) angol nyelven
    public string GetNodeInfo()
    {
        // Forrás esetén fix aktív állapotot mutatunk
        if (type == NodeType.Start) return "Energy Source\n<color=green>ACTIVE</color>";

        // Alap állapotok: van áram, nincs áram, vagy hiba (rövidzárlat)
        string status = isPowered ? "<color=green>POWERED</color>" : "<color=red>NO POWER</color>";
        if (isOverloaded) status = "<color=orange>SHORT CIRCUIT!</color>";

        // Kapcsoló típus esetén extra sor az állapotról
        if (type == NodeType.Switch) status += isSwitchOn ? "\n(CONNECTED)" : "\n(DISCONNECTED)";

        // Az összesített információk összefûzése
        string info = nodeName + " (" + type.ToString() + ")\nStatus: " + status + "\n";
        info += "Inputs: " + activeInputs + "\n";

        // Az interakciós segédlet megjelenítése a típus alapján
        if (type == NodeType.Switch) info += "[E] Toggle On/Off";
        else if (type != NodeType.End) info += "[E] Rotate (90°)";

        return info;
    }
}