# White Room Escape

White Room Escape is a modular Unity-based puzzle game developed as a thesis project.  
The game consists of multiple independent puzzle rooms connected through a central hub system.  
Each room introduces a different gameplay mechanic and logical challenge while sharing a common interaction and progression framework.

The project focuses on game system architecture, algorithmic puzzle design, and modular gameplay mechanics implemented in C#.

---

## Overview

The player starts in a central hub and must solve a series of puzzle rooms.  
Each room contains a unique mechanic that must be understood and solved in order to unlock the exit.

Progression is tracked through a profile system and a scoreboard records player performance.

Core features include:

- Main menu and settings system
- Profile management
- Central hub with level progression
- 9 unique puzzle rooms
- Interaction system
- Scoreboard and time tracking
- Modular game architecture

---

## Gameplay Structure

The game consists of **9 puzzle rooms**, each implementing a different mechanic.

| Level | Description |
|------|-------------|
| Game 1 | Introductory puzzle with simple interaction mechanics |
| Game 2 | Logic puzzle using switches or triggers |
| Game 3 | Exploration-based puzzle with environmental interaction |
| Game 4 | Grid-based floor puzzle with safe and trap tiles |
| Game 5 | Pattern or logic-based challenge |
| Game 6 | Procedural maze generation using a recursive backtracking algorithm |
| Game 7 | Intermediate puzzle combining multiple interaction elements |
| Game 8 | Advanced puzzle requiring multi-step problem solving |
| Game 9 | Graph-based logic system using interconnected nodes |

Each puzzle room operates independently but integrates with the global game systems.

---

## Core Systems

### Main Menu System

Handles navigation between:

- Start Game
- Settings
- Profiles
- Scoreboard

Implemented in:
Assets/Scripts/Main/MainMenuController.cs

---

### Hub System

The hub acts as the central progression system where players can access unlocked puzzle rooms.

Features:

- Level unlocking
- Completion tracking
- Visual feedback for completed puzzles

Implemented in:
Assets/Scripts/Main/HubManager.cs

---

### Interaction System

A generic interaction system allows the player to interact with puzzle elements using raycasting.

Key components:

- `InteractionController`
- `IInteractable` interface

This system allows puzzles to define their own behavior while sharing a common interaction framework.

Assets/Scripts/Interaction

---

### Game Manager

The `GameManager` controls global game state including:

- level completion
- player lives
- score tracking
- profile data

Assets/Scripts/GameManager.cs

---

### Scoreboard System

Tracks player performance and total completion time across profiles.

Implemented in:
Assets/Scripts/Main/ScoreboardController.cs

---

## Algorithms Used

Several puzzles implement algorithmic solutions:

### Maze Generation (Game 6)

Uses a **Recursive Backtracking algorithm** to generate a procedural maze.

Key concepts:

- grid generation
- stack-based DFS traversal
- wall removal between cells

Files:
MazeGenerator.cs
MazeCell.cs

---

### Graph Logic System (Game 9)

Implements a node-based logical system where different node types process signals.

Node types include:

- AND
- OR
- XOR
- Splitter
- Switch
- End node

The system evaluates signal propagation through the graph until the final node condition is satisfied.

Files:
GraphManager.cs
GraphNode.cs

---

## Project Structure
Assets
├── Scenes
│ ├── MainMenu
│ ├── MainHub
│ ├── Game_1 ... Game_9
│
├── Scripts
│ ├── Main
│ ├── Interaction
│ ├── Game_1
│ ├── Game_2
│ ├── Game_3
│ ├── Game_4
│ ├── Game_5
│ ├── Game_6
│ ├── Game_7
│ ├── Game_8
│ └── Game_9


---

## Technologies Used

- **Unity Engine**
- **C#**
- **ShaderLab**
- **HLSL**

Unity handles the rendering pipeline while gameplay logic and system architecture are implemented in C#.

---

## Running the Project

### Requirements

- Unity **6.3 LTS** (or compatible version)

### Steps

1. Clone the repository
git clone https://github.com/MrAphell/WhiteRoomEscape.git

2. Open the project in Unity Hub

3. Open the scene:
Assets/Scenes/MainMenu

4. Press **Play**

---

## Future Improvements

Planned improvements include:

- improved scoreboard logic
- additional puzzle mechanics
- enhanced UI feedback
- improved persistence system for profiles and scores

---

## Author

Polonkai Olivér  
Software Engineering BSc Thesis Project

---

## License

This project was developed for educational and research purposes.

---

# White Room Escape

A **White Room Escape** egy moduláris felépítésű, Unity játékmotorral fejlesztett puzzle játék, amely szakdolgozati projektként készült.  
A játék több egymástól független feladványszobából áll, amelyeket egy központi hub rendszer köt össze. Minden szoba egyedi játékmeneti mechanikát és logikai kihívást tartalmaz, miközben közös interakciós és progressziós keretrendszert használ.

A projekt fő fókusza a játékrendszerek architektúrája, az algoritmikus puzzle-tervezés, valamint a moduláris játékmeneti mechanikák megvalósítása C# nyelven.

---

## Áttekintés

A játékos egy központi hubból indul, ahonnan különböző feladványszobákba léphet be.  
Minden szoba egy egyedi mechanikát tartalmaz, amelyet a játékosnak meg kell értenie és meg kell oldania ahhoz, hogy feloldja a kijáratot.

A haladást egy profilrendszer követi, míg a játékos teljesítményét egy ranglista (scoreboard) rögzíti.

A játék főbb funkciói:

- Főmenü és beállítási rendszer  
- Profilkezelés  
- Központi hub a pályák közötti haladással  
- 9 egyedi feladványszoba  
- Interakciós rendszer  
- Ranglista és időmérés  
- Moduláris játékarchitektúra  

---

## Játékmenet felépítése

A játék **9 különböző feladványszobából** áll, amelyek mindegyike más mechanikát valósít meg.

| Pálya | Leírás |
|------|-------------|
| Game 1 | Bevezető puzzle egyszerű interakciós mechanikával |
| Game 2 | Logikai puzzle kapcsolókkal vagy triggerek segítségével |
| Game 3 | Felfedezés alapú puzzle környezeti interakciókkal |
| Game 4 | Rácsalapú padló puzzle biztonságos és csapda csempékkel |
| Game 5 | Mintafelismerésen vagy logikai sorrenden alapuló kihívás |
| Game 6 | Procedurálisan generált labirintus rekurzív backtracking algoritmussal |
| Game 7 | Közepes nehézségű puzzle több interakciós elem kombinációjával |
| Game 8 | Összetettebb puzzle több lépésből álló megoldással |
| Game 9 | Gráf alapú logikai rendszer összekapcsolt csomópontokkal |

Minden feladványszoba önállóan működik, de integrálódik a globális játékrendszerbe.

---

## Fő rendszerek

### Főmenü rendszer

A főmenü biztosítja a navigációt a következő elemek között:

- Játék indítása  
- Beállítások  
- Profilok  
- Ranglista  

Megvalósítás:
Assets/Scripts/Main/MainMenuController.cs

---

### Hub rendszer

A hub a játék központi haladási rendszere, ahol a játékos hozzáférhet a feloldott feladványszobákhoz.

Funkciók:

- Pályák feloldása  
- Teljesítések nyomon követése  
- Vizuális visszajelzés a teljesített pályákról  

Megvalósítás:
Assets/Scripts/Main/HubManager.cs

---

### Interakciós rendszer

Egy általános interakciós rendszer teszi lehetővé, hogy a játékos raycasting segítségével kapcsolatba lépjen a puzzle elemekkel.

Fő komponensek:

- `InteractionController`  
- `IInteractable` interfész  

Ez a rendszer lehetővé teszi, hogy az egyes puzzle elemek saját viselkedést definiáljanak, miközben közös interakciós keretrendszert használnak.

---

### Game Manager

A `GameManager` kezeli a globális játékállapotot, például:

- pálya teljesítések  
- játékos életek  
- pontszám követés  
- profil adatok  

---

### Ranglista rendszer

A ranglista a játékos teljesítményét és az összesített teljesítési időt követi a profilok között.

Megvalósítás:
Assets/Scripts/Main/ScoreboardController.cs

---

## Használt algoritmusok

Számos puzzle algoritmikus megoldásokat alkalmaz.

### Labirintus generálás (Game 6)

A pálya **Recursive Backtracking algoritmust** használ egy procedurálisan generált labirintus létrehozásához.

Fő koncepciók:

- rács generálás  
- verem alapú DFS bejárás  
- falak eltávolítása cellák között  

Fájlok:
MazeGenerator.cs
MazeCell.cs

---

### Gráf alapú logikai rendszer (Game 9)

A rendszer egy csomópont alapú logikai hálózatot valósít meg, ahol különböző node típusok dolgozzák fel a jeleket.

Node típusok:

- AND  
- OR  
- XOR  
- Splitter  
- Switch  
- End node  

A rendszer a gráfon keresztül propagálja a jeleket, amíg a végső csomópont feltétele teljesül.

Fájlok:
GraphManager.cs
GraphNode.cs

---

## Projekt struktúra
Assets
├── Scenes
│ ├── MainMenu
│ ├── MainHub
│ ├── Game_1 ... Game_9
│
├── Scripts
│ ├── Main
│ ├── Interaction
│ ├── Game_1
│ ├── Game_2
│ ├── Game_3
│ ├── Game_4
│ ├── Game_5
│ ├── Game_6
│ ├── Game_7
│ ├── Game_8
│ └── Game_9

---

## Felhasznált technológiák

- **Unity Engine**
- **C#**
- **ShaderLab**
- **HLSL**

A Unity kezeli a grafikai renderelési folyamatot, míg a játékmenet logikája és a rendszerarchitektúra C# nyelven került megvalósításra.

---

## A projekt futtatása

### Követelmények

- Unity **6.3 LTS** (vagy kompatibilis verzió)

### Lépések

1. A repository klónozása
git clone https://github.com/MrAphell/WhiteRoomEscape.git

2. A projekt megnyitása Unity Hub segítségével

3. A következő jelenet megnyitása:
Assets/Scenes/MainMenu

4. A **Play** gomb megnyomása

---

## Jövőbeli fejlesztések

Tervezett fejlesztések:

- a ranglista logikájának továbbfejlesztése  
- további puzzle mechanikák  
- jobb felhasználói visszajelzések a felületen  
- fejlettebb adatmentési rendszer profilok és pontszámok számára  

---

## Szerző

Polonkai Olivér  
Programtervező informatikus BSc szakdolgozati projekt

---

## Licenc

A projekt oktatási és kutatási célból készült.
