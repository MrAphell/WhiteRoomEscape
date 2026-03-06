using System.Collections.Generic;
using UnityEngine;

// A lézerhálózat logikai kiértékeléséért felelõs központi vezérlõ
public class GraphManager : MonoBehaviour
{
    public static GraphManager Instance;
    public GameObject exitToHubObject; // A kijárati kapu/objektum, ami siker esetén aktiválódik

    private GraphNode[] _allNodes; // A jelenetben található összes hálózati elem

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        // Alaphelyzetben a kijárat zárva van
        if (exitToHubObject != null) exitToHubObject.SetActive(false);

        // Összegyûjtjük a hálózat összes elemét a jelenetbõl
        _allNodes = FindObjectsOfType<GraphNode>();
        ScheduleEvaluation();
    }

    // Késleltetett kiértékelés, hogy a fizika és a forgások stabilizálódjanak
    public void ScheduleEvaluation()
    {
        CancelInvoke("EvaluateGraph");
        Invoke("EvaluateGraph", 0.1f);
    }

    // A hálózat logikai állapotának kiszámítása
    private void EvaluateGraph()
    {
        // 1.Fázis: Radar (Mindenki feltérképezi a szomszédait)
        foreach (GraphNode node in _allNodes)
        {
            node.FindNeighbors();
        }

        // 2.Fázis: Iteratív Relaxáció (Állapottér-keresés)
        // Addig ismételjük a kalkulációt, amíg a jelek "be nem állnak" véglegesre.
        // Ez küszöböli ki a versenyhelyzetet (Race Condition) a különbözõ úthosszoknál.
        bool changed = true;
        int maxIterations = 20; // Védõháló, hogy egy paradoxon/ciklus ne fagyassza le a játékot

        // Kezdeti állapot: csak a Start források adnak áramot
        foreach (GraphNode node in _allNodes)
        {
            node.isPowered = (node.type == GraphNode.NodeType.Start);
            node.isOverloaded = false;
        }

        // Stabil állapot keresése
        while (changed && maxIterations > 0)
        {
            changed = false;
            maxIterations--;

            // A) Bemenetek nullázása a friss számoláshoz
            foreach (GraphNode node in _allNodes) node.activeInputs = 0;

            // B) Áramfolyam szimulálása: ki küld jelet a szomszédjának
            foreach (GraphNode node in _allNodes)
            {
                if (node.isPowered && !node.isOverloaded)
                {
                    foreach (GraphNode target in node.outgoingNodes)
                    {
                        if (target != null) target.activeInputs++;
                    }
                }
            }

            // C) Logikai kapuk kiértékelése a beérkezõ jelek száma alapján
            foreach (GraphNode node in _allNodes)
            {
                if (node.type == GraphNode.NodeType.Start) continue;

                bool wasPowered = node.isPowered;
                bool wasOverloaded = node.isOverloaded;

                node.isPowered = false;
                node.isOverloaded = false;

                switch (node.type)
                {
                    case GraphNode.NodeType.Relay:
                    case GraphNode.NodeType.Splitter:
                    case GraphNode.NodeType.Switch:
                    case GraphNode.NodeType.OR:
                    case GraphNode.NodeType.End:
                        // Megengedõ logika: legalább 1 bemenet kell
                        if (node.activeInputs >= 1) node.isPowered = true;
                        break;

                    case GraphNode.NodeType.AND:
                        // Szigorú logika: legalább 2 bemenet kell
                        if (node.activeInputs >= 2) node.isPowered = true;
                        break;

                    case GraphNode.NodeType.XOR:
                        // Kizáró logika: pontosan 1 bemenet kapcsolja be
                        if (node.activeInputs == 1) node.isPowered = true;
                        else if (node.activeInputs > 1)
                        {
                            node.isPowered = false;
                            node.isOverloaded = true; // Több bemenet esetén zárlat
                        }
                        break;
                }

                // Ha változott egy elem állapota, jelezzük, hogy szükség van újabb körre
                if (wasPowered != node.isPowered || wasOverloaded != node.isOverloaded)
                {
                    changed = true;
                }
            }
        }

        // 3.Fázis: Végsõ ellenõrzés a stabil állapot elérése után
        bool allEndsPowered = true;
        bool hasAtLeastOneEnd = false;

        foreach (GraphNode node in _allNodes)
        {
            if (node.type == GraphNode.NodeType.End)
            {
                hasAtLeastOneEnd = true;
                if (!node.isPowered) allEndsPowered = false;
            }
        }

        // Ha minden létezõ End kocka energiát kap, nyitjuk a kijáratot
        if (hasAtLeastOneEnd && allEndsPowered)
        {
            if (exitToHubObject != null && !exitToHubObject.activeSelf) exitToHubObject.SetActive(true);
        }
        else
        {
            if (exitToHubObject != null) exitToHubObject.SetActive(false);
        }
    }
}