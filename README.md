
# White Room Escape

  

White Room Escape is a modular Unity-based puzzle game developed as a BSc thesis project.

The game consists of multiple puzzle rooms connected through a central hub system.

Each room introduces a different gameplay mechanic and logical challenge while sharing a common interaction, progression, and profile framework.

  

The project focuses on game system architecture, algorithmic puzzle design, and modular gameplay mechanics implemented in C#.

  

---

  

## Overview

  

The player starts from a central hub and progresses through a sequence of puzzle rooms.

Each room contains a distinct mechanic that must be understood and solved in order to complete the level and unlock further progression.

  

Player progression is managed through a profile-based system, while completion times are stored and displayed through a scoreboard subsystem.

  

Core features include:

  

- Main menu and settings system

- Profile management

- Central hub with level progression

- 9 different puzzle rooms

- Shared interaction system

- Scoreboard and time tracking

- Modular game architecture

- Automated EditMode and PlayMode tests

  

---

  

## Gameplay Structure

  

The game consists of **9 puzzle rooms**, each implementing a different logical or gameplay mechanic.

  

| Level | Description |

|------|-------------|

| Game 1 | Introductory puzzle with object interaction and keypad usage |

| Game 2 | Logic puzzle based on switch states and combination handling |

| Game 3 | Exploration-oriented puzzle with light and environmental discovery |

| Game 4 | Grid-based navigation puzzle with rule-based movement |

| Game 5 | Memory and pattern-recognition challenge |

| Game 6 | Procedurally generated maze using a recursive backtracking algorithm |

| Game 7 | Stealth-oriented puzzle with detection and enemy behaviour |

| Game 8 | Multi-step logic puzzle built around interconnected activation states |

| Game 9 | Graph-based logic system using interconnected nodes |

  

Each puzzle room works as an independent gameplay space, while remaining integrated into the global game systems.

  

---

  

## Core Systems

  

### Main Menu System

  

Handles navigation between:

  

- Start Game

- Settings

- Profiles

- Scoreboard

  

Implemented in:

- `Assets/Scripts/Main/MainMenuController.cs`

  

---

  

### Hub System

  

The hub acts as the central progression space where players can access unlocked puzzle rooms.

  

Features:

  

- Level unlocking

- Progression feedback

- Visual distinction between locked, available, and completed levels

  

Implemented in:

- `Assets/Scripts/Main/HubManager.cs`

  

---

  

### Interaction System

  

A generic interaction system allows the player to interact with puzzle elements using raycasting.

  

Key components:

  

- `InteractionController`

- `IInteractable`

  

This system allows puzzles to define their own behavior while sharing a common interaction framework.

  

Implemented in:

- `Assets/Scripts/Interaction/`

  

---

  

### Game Manager

  

The `GameManager` coordinates global game state, including:

  

- active profile handling

- unlocked level progression

- persistent player-related state values

- scene-related progression logic

  

Implemented in:

- `Assets/Scripts/GameManager.cs`

  

---

  

### Scoreboard System

  

The scoreboard subsystem stores and displays completion times for the individual puzzle rooms.

  

Implemented in:

- `Assets/Scripts/ScoreBoard/ScoreManager.cs`

- `Assets/Scripts/ScoreBoard/ScoreboardUI.cs`

  

---

  

## Algorithms Used

  

Several puzzles rely on algorithmic or structured logical solutions.

  

### Maze Generation (Game 6)

  

Uses a **recursive backtracking algorithm** to generate a procedural maze.

  

Key concepts:

  

- grid generation

- depth-first traversal

- wall removal between cells

  

Files:

- `Assets/Scripts/Game_6/MazeGenerator.cs`

- `Assets/Scripts/Game_6/MazeCell.cs`

  

---

  

### Graph Logic System (Game 9)

  

Implements a node-based logical system in which different node types process and propagate signals.

  

Files:

- `Assets/Scripts/Game_9/GraphManager.cs`

- `Assets/Scripts/Game_9/GraphNode.cs`

  

---

  

## Testing

  

The project includes automated tests using Unity Test Framework.

  

Two test levels are present:

  

- **EditMode tests** for central logic and state handling

- **PlayMode tests** for runtime interaction and gameplay behaviour

  

Main tested areas include:

  

- `GameManager`

- `HubManager`

- `InteractionController`

- `KeypadSystem`

- `MainMenuController`

- `PauseManager`

- `ScoreManager`

- selected gameplay-related runtime behaviours

  

Test folders:

- `Assets/Tests/EditModeTests/`

- `Assets/Tests/PlayModeTests/`

  

---

  

## Project Structure

  

```text

Assets

├── Scenes

│ ├── MainMenu

│ ├── MainHub

│ ├── Game_1 ... Game_9

│

├── Scripts

│ ├── Main

│ ├── Interaction

│ ├── ScoreBoard

│ ├── Game_1

│ ├── Game_2

│ ├── Game_3

│ ├── Game_4

│ ├── Game_5

│ ├── Game_6

│ ├── Game_7

│ ├── Game_8

│ └── Game_9

│

└── Tests

├── EditModeTests

└── PlayModeTests

  

```

## Technologies Used

- Unity Engine

- C#

- Unity Test Framework

  

Unity provides the engine environment, scene management, UI framework, and runtime systems, while gameplay logic and system architecture are implemented in C#.

  

## Running the Project

### Requirements

  

Use the Unity version specified by the project configuration in:

ProjectSettings/ProjectVersion.txt

  

### Steps

1. Clone the repository:

```bash

git clone https://github.com/MrAphell/WhiteRoomEscape.git

```

2. Open the project in Unity Hub

3. Open the scene:

Assets/Scenes/MainMenu

4. Press Play

  

## Future Improvements

  

Possible future improvements include:

  

further refinement of scoreboard presentation

improved UI feedback

expanded puzzle balancing and polish

fuller gameplay integration of some partially prepared systems

additional automated tests

Author

  

***Polonkai Olivér***

  

*BSc Thesis Project*

  

# License

  

This project was developed for educational and research purposes.

  

---

  

# Projekt címe

  

Rövid leírás arról, hogy mit csinál ez a projekt, és kiknek készült.

  

# White Room Escape

  

A White Room Escape egy moduláris felépítésű, Unity-alapú puzzle játék, amely BSc szakdolgozati projektként készült.

A játék több feladványszobából áll, amelyeket egy központi hub rendszer köt össze.

Minden szoba eltérő játékmeneti mechanikát és logikai kihívást kínál, miközben közös interakciós, progressziós és profilkezelési keretrendszert használ.

  

A projekt középpontjában a játékrendszerek architektúrája, az algoritmikus puzzle-tervezés, valamint a C# nyelven megvalósított moduláris játékmeneti mechanikák állnak.

  

---

  

## Áttekintés

  

A játékos egy központi hubból indul, majd egymás után halad végig a különböző feladványszobákon.

Minden szoba egy sajátos mechanikát tartalmaz, amelyet meg kell érteni és meg kell oldani a pálya teljesítéséhez és a további haladás feloldásához.

  

A játékos haladását profilalapú rendszer kezeli, míg a teljesítési idők tárolását és megjelenítését egy ranglistarendszer végzi.

  

Főbb funkciók:

  

- Főmenü és beállítási rendszer

- Profilkezelés

- Központi hub pályahaladással

- 9 különböző feladványszoba

- Közös interakciós rendszer

- Ranglista és időmérés

- Moduláris játékarchitektúra

- Automatizált EditMode és PlayMode tesztek

  

---

  

## Játékmenet felépítése

  

A játék **9 feladványszobából** áll, amelyek mindegyike eltérő logikai vagy játékmeneti mechanikát valósít meg.

  

| Pálya | Leírás |

|------|--------|

| Game 1 | Bevezető puzzle objektuminterakcióval és kódpad használattal |

| Game 2 | Logikai puzzle kapcsolóállapotok és kombinációk kezelésével |

| Game 3 | Felfedezésre épülő puzzle fénnyel és környezeti elemekkel |

| Game 4 | Rácsalapú navigációs puzzle szabályalapú mozgással |

| Game 5 | Memória- és mintafelismerési kihívás |

| Game 6 | Procedurálisan generált labirintus rekurzív backtracking algoritmussal |

| Game 7 | Lopakodásalapú puzzle észleléssel és ellenfélviselkedéssel |

| Game 8 | Többlépcsős logikai puzzle egymásra épülő aktiválási állapotokkal |

| Game 9 | Gráfalapú logikai rendszer összekapcsolt csomópontokkal |

  

Minden feladványszoba önálló játéktérként működik, miközben integrálódik a globális játékrendszerbe.

  

---

  

## Fő rendszerek

  

### Főmenü rendszer

  

A navigációt az alábbi elemek között kezeli:

  

- Játék indítása

- Beállítások

- Profilok

- Ranglista

  

Megvalósítás helye:

- `Assets/Scripts/Main/MainMenuController.cs`

  

---

  

### Hub rendszer

  

A hub a központi haladási tér, ahol a játékos hozzáférhet a feloldott feladványszobákhoz.

  

Főbb funkciók:

  

- Pályafeloldás

- Progressziós visszajelzés

- A zárolt, elérhető és teljesített pályák vizuális megkülönböztetése

  

Megvalósítás helye:

- `Assets/Scripts/Main/HubManager.cs`

  

---

  

### Interakciós rendszer

  

Az általános interakciós rendszer lehetővé teszi, hogy a játékos raycasting segítségével kapcsolatba lépjen a puzzle-elemekkel.

  

Fő komponensek:

  

- `InteractionController`

- `IInteractable`

  

Ez a rendszer lehetővé teszi, hogy az egyes puzzle-elemek saját viselkedést definiáljanak, miközben közös interakciós keretrendszert használnak.

  

Megvalósítás helye:

- `Assets/Scripts/Interaction/`

  

---

  

### Game Manager

  

A `GameManager` a globális játékállapot koordinálását végzi, beleértve az alábbiakat:

  

- aktív profil kezelése

- feloldott pályák progressziója

- tartós, játékoshoz kapcsolódó állapotértékek

- jelenetekhez kapcsolódó progressziós logika

  

Megvalósítás helye:

- `Assets/Scripts/GameManager.cs`

  

---

  

### Ranglistarendszer

  

A ranglistarendszer az egyes feladványszobák teljesítési idejét tárolja és jeleníti meg.

  

Megvalósítás helye:

- `Assets/Scripts/ScoreBoard/ScoreManager.cs`

- `Assets/Scripts/ScoreBoard/ScoreboardUI.cs`

  

---

  

## Használt algoritmusok

  

Több puzzle algoritmikus vagy strukturált logikai megoldásokra épül.

  

### Labirintusgenerálás (Game 6)

  

**Rekurzív backtracking algoritmust** használ procedurális labirintus generálására.

  

Főbb fogalmak:

  

- rácsgenerálás

- mélységi bejárás

- falak eltávolítása a cellák között

  

Fájlok:

- `Assets/Scripts/Game_6/MazeGenerator.cs`

- `Assets/Scripts/Game_6/MazeCell.cs`

  

---

  

### Gráfalapú logikai rendszer (Game 9)

  

Csomópontalapú logikai rendszert valósít meg, amelyben különböző csomóponttípusok dolgozzák fel és továbbítják a jeleket.

  

Fájlok:

- `Assets/Scripts/Game_9/GraphManager.cs`

- `Assets/Scripts/Game_9/GraphNode.cs`

  

---

  

## Tesztelés

  

A projekt automatizált teszteket is tartalmaz a Unity Test Framework segítségével.

  

Két tesztszint található benne:

  

- **EditMode tesztek** a központi logika és állapotkezelés vizsgálatára

- **PlayMode tesztek** a futás közbeni interakciók és játékmeneti viselkedések vizsgálatára

  

A főbb tesztelt területek:

  

- `GameManager`

- `HubManager`

- `InteractionController`

- `KeypadSystem`

- `MainMenuController`

- `PauseManager`

- `ScoreManager`

- egyes futás közbeni játékmeneti viselkedések

  

Tesztek mappái:

- `Assets/Tests/EditModeTests/`

- `Assets/Tests/PlayModeTests/`

  

---

  

## Projektstruktúra

  

```text

Assets

├── Scenes

│ ├── MainMenu

│ ├── MainHub

│ ├── Game_1 ... Game_9

│

├── Scripts

│ ├── Main

│ ├── Interaction

│ ├── ScoreBoard

│ ├── Game_1

│ ├── Game_2

│ ├── Game_3

│ ├── Game_4

│ ├── Game_5

│ ├── Game_6

│ ├── Game_7

│ ├── Game_8

│ └── Game_9

│

└── Tests

├── EditModeTests

└── PlayModeTests

```

## Felhasznált technológiák

  

- **Unity Engine**

- **C#**

- **Unity Test Framework**

  

A Unity biztosítja a motor környezetét, a jelenetkezelést, a felhasználói felület keretrendszerét és a futásidejű rendszereket, míg a játékmeneti logika és a rendszerarchitektúra C# nyelven készült.

  

## A projekt futtatása

  

### Követelmények

  

Használd a projekt konfigurációjában megadott Unity-verziót itt:

  

`ProjectSettings/ProjectVersion.txt`

  

### Lépések

  

1. Klónozd a repository-t:

  

```bash

git clone https://github.com/MrAphell/WhiteRoomEscape.git

```

Nyisd meg a projektet Unity Hubban.

  

2. Nyisd meg a következő jelenetet:

  

3. Assets/Scenes/MainMenu

  

4. Nyomd meg a Play gombot.

  

## Jövőbeli fejlesztések

  

Lehetséges jövőbeli fejlesztések:

  

- a ranglista megjelenítésének további finomítása

- jobb felhasználói visszajelzések

- a puzzle-ök egyensúlyának és kidolgozottságának bővítése

- egyes előkészített rendszerek teljesebb játékmeneti integrációja

- további automatizált tesztek

  

---

  

## Szerző

  

**Polonkai Olivér**

  

*BSc szakdolgozati projekt*

  

---

  

## Licenc

  

A projekt oktatási és kutatási célból készült.# White Room Escape

  

White Room Escape is a modular Unity-based puzzle game developed as a BSc thesis project.

The game consists of multiple puzzle rooms connected through a central hub system.

Each room introduces a different gameplay mechanic and logical challenge while sharing a common interaction, progression, and profile framework.

  

The project focuses on game system architecture, algorithmic puzzle design, and modular gameplay mechanics implemented in C#.

  

---

  

## Overview

  

The player starts from a central hub and progresses through a sequence of puzzle rooms.

Each room contains a distinct mechanic that must be understood and solved in order to complete the level and unlock further progression.

  

Player progression is managed through a profile-based system, while completion times are stored and displayed through a scoreboard subsystem.

  

Core features include:

  

- Main menu and settings system

- Profile management

- Central hub with level progression

- 9 different puzzle rooms

- Shared interaction system

- Scoreboard and time tracking

- Modular game architecture

- Automated EditMode and PlayMode tests

  

---

  

## Gameplay Structure

  

The game consists of **9 puzzle rooms**, each implementing a different logical or gameplay mechanic.

  

| Level | Description |

|------|-------------|

| Game 1 | Introductory puzzle with object interaction and keypad usage |

| Game 2 | Logic puzzle based on switch states and combination handling |

| Game 3 | Exploration-oriented puzzle with light and environmental discovery |

| Game 4 | Grid-based navigation puzzle with rule-based movement |

| Game 5 | Memory and pattern-recognition challenge |

| Game 6 | Procedurally generated maze using a recursive backtracking algorithm |

| Game 7 | Stealth-oriented puzzle with detection and enemy behaviour |

| Game 8 | Multi-step logic puzzle built around interconnected activation states |

| Game 9 | Graph-based logic system using interconnected nodes |

  

Each puzzle room works as an independent gameplay space, while remaining integrated into the global game systems.

  

---

  

## Core Systems

  

### Main Menu System

  

Handles navigation between:

  

- Start Game

- Settings

- Profiles

- Scoreboard

  

Implemented in:

- `Assets/Scripts/Main/MainMenuController.cs`

  

---

  

### Hub System

  

The hub acts as the central progression space where players can access unlocked puzzle rooms.

  

Features:

  

- Level unlocking

- Progression feedback

- Visual distinction between locked, available, and completed levels

  

Implemented in:

- `Assets/Scripts/Main/HubManager.cs`

  

---

  

### Interaction System

  

A generic interaction system allows the player to interact with puzzle elements using raycasting.

  

Key components:

  

- `InteractionController`

- `IInteractable`

  

This system allows puzzles to define their own behavior while sharing a common interaction framework.

  

Implemented in:

- `Assets/Scripts/Interaction/`

  

---

  

### Game Manager

  

The `GameManager` coordinates global game state, including:

  

- active profile handling

- unlocked level progression

- persistent player-related state values

- scene-related progression logic

  

Implemented in:

- `Assets/Scripts/GameManager.cs`

  

---

  

### Scoreboard System

  

The scoreboard subsystem stores and displays completion times for the individual puzzle rooms.

  

Implemented in:

- `Assets/Scripts/ScoreBoard/ScoreManager.cs`

- `Assets/Scripts/ScoreBoard/ScoreboardUI.cs`

  

---

  

## Algorithms Used

  

Several puzzles rely on algorithmic or structured logical solutions.

  

### Maze Generation (Game 6)

  

Uses a **recursive backtracking algorithm** to generate a procedural maze.

  

Key concepts:

  

- grid generation

- depth-first traversal

- wall removal between cells

  

Files:

- `Assets/Scripts/Game_6/MazeGenerator.cs`

- `Assets/Scripts/Game_6/MazeCell.cs`

  

---

  

### Graph Logic System (Game 9)

  

Implements a node-based logical system in which different node types process and propagate signals.

  

Files:

- `Assets/Scripts/Game_9/GraphManager.cs`

- `Assets/Scripts/Game_9/GraphNode.cs`

  

---

  

## Testing

  

The project includes automated tests using Unity Test Framework.

  

Two test levels are present:

  

- **EditMode tests** for central logic and state handling

- **PlayMode tests** for runtime interaction and gameplay behaviour

  

Main tested areas include:

  

- `GameManager`

- `HubManager`

- `InteractionController`

- `KeypadSystem`

- `MainMenuController`

- `PauseManager`

- `ScoreManager`

- selected gameplay-related runtime behaviours

  

Test folders:

- `Assets/Tests/EditModeTests/`

- `Assets/Tests/PlayModeTests/`

  

---

  

## Project Structure

  

```text

Assets

├── Scenes

│ ├── MainMenu

│ ├── MainHub

│ ├── Game_1 ... Game_9

│

├── Scripts

│ ├── Main

│ ├── Interaction

│ ├── ScoreBoard

│ ├── Game_1

│ ├── Game_2

│ ├── Game_3

│ ├── Game_4

│ ├── Game_5

│ ├── Game_6

│ ├── Game_7

│ ├── Game_8

│ └── Game_9

│

└── Tests

├── EditModeTests

└── PlayModeTests

  

```

## Technologies Used

- Unity Engine

- C#

- Unity Test Framework

  

Unity provides the engine environment, scene management, UI framework, and runtime systems, while gameplay logic and system architecture are implemented in C#.

  

## Running the Project

### Requirements

  

Use the Unity version specified by the project configuration in:

ProjectSettings/ProjectVersion.txt

  

### Steps

1. Clone the repository:

```bash

git clone https://github.com/MrAphell/WhiteRoomEscape.git

```

2. Open the project in Unity Hub

3. Open the scene:

Assets/Scenes/MainMenu

4. Press Play

  

## Future Improvements

  

Possible future improvements include:

  

further refinement of scoreboard presentation

improved UI feedback

expanded puzzle balancing and polish

fuller gameplay integration of some partially prepared systems

additional automated tests

Author

  

***Polonkai Olivér***

  

*BSc Thesis Project*

  

# License

  

This project was developed for educational and research purposes.

  

---

  

# Projekt címe

  

Rövid leírás arról, hogy mit csinál ez a projekt, és kiknek készült.

  

# White Room Escape

  

A White Room Escape egy moduláris felépítésű, Unity-alapú puzzle játék, amely BSc szakdolgozati projektként készült.

A játék több feladványszobából áll, amelyeket egy központi hub rendszer köt össze.

Minden szoba eltérő játékmeneti mechanikát és logikai kihívást kínál, miközben közös interakciós, progressziós és profilkezelési keretrendszert használ.

  

A projekt középpontjában a játékrendszerek architektúrája, az algoritmikus puzzle-tervezés, valamint a C# nyelven megvalósított moduláris játékmeneti mechanikák állnak.

  

---

  

## Áttekintés

  

A játékos egy központi hubból indul, majd egymás után halad végig a különböző feladványszobákon.

Minden szoba egy sajátos mechanikát tartalmaz, amelyet meg kell érteni és meg kell oldani a pálya teljesítéséhez és a további haladás feloldásához.

  

A játékos haladását profilalapú rendszer kezeli, míg a teljesítési idők tárolását és megjelenítését egy ranglistarendszer végzi.

  

Főbb funkciók:

  

- Főmenü és beállítási rendszer

- Profilkezelés

- Központi hub pályahaladással

- 9 különböző feladványszoba

- Közös interakciós rendszer

- Ranglista és időmérés

- Moduláris játékarchitektúra

- Automatizált EditMode és PlayMode tesztek

  

---

  

## Játékmenet felépítése

  

A játék **9 feladványszobából** áll, amelyek mindegyike eltérő logikai vagy játékmeneti mechanikát valósít meg.

  

| Pálya | Leírás |

|------|--------|

| Game 1 | Bevezető puzzle objektuminterakcióval és kódpad használattal |

| Game 2 | Logikai puzzle kapcsolóállapotok és kombinációk kezelésével |

| Game 3 | Felfedezésre épülő puzzle fénnyel és környezeti elemekkel |

| Game 4 | Rácsalapú navigációs puzzle szabályalapú mozgással |

| Game 5 | Memória- és mintafelismerési kihívás |

| Game 6 | Procedurálisan generált labirintus rekurzív backtracking algoritmussal |

| Game 7 | Lopakodásalapú puzzle észleléssel és ellenfélviselkedéssel |

| Game 8 | Többlépcsős logikai puzzle egymásra épülő aktiválási állapotokkal |

| Game 9 | Gráfalapú logikai rendszer összekapcsolt csomópontokkal |

  

Minden feladványszoba önálló játéktérként működik, miközben integrálódik a globális játékrendszerbe.

  

---

  

## Fő rendszerek

  

### Főmenü rendszer

  

A navigációt az alábbi elemek között kezeli:

  

- Játék indítása

- Beállítások

- Profilok

- Ranglista

  

Megvalósítás helye:

- `Assets/Scripts/Main/MainMenuController.cs`

  

---

  

### Hub rendszer

  

A hub a központi haladási tér, ahol a játékos hozzáférhet a feloldott feladványszobákhoz.

  

Főbb funkciók:

  

- Pályafeloldás

- Progressziós visszajelzés

- A zárolt, elérhető és teljesített pályák vizuális megkülönböztetése

  

Megvalósítás helye:

- `Assets/Scripts/Main/HubManager.cs`

  

---

  

### Interakciós rendszer

  

Az általános interakciós rendszer lehetővé teszi, hogy a játékos raycasting segítségével kapcsolatba lépjen a puzzle-elemekkel.

  

Fő komponensek:

  

- `InteractionController`

- `IInteractable`

  

Ez a rendszer lehetővé teszi, hogy az egyes puzzle-elemek saját viselkedést definiáljanak, miközben közös interakciós keretrendszert használnak.

  

Megvalósítás helye:

- `Assets/Scripts/Interaction/`

  

---

  

### Game Manager

  

A `GameManager` a globális játékállapot koordinálását végzi, beleértve az alábbiakat:

  

- aktív profil kezelése

- feloldott pályák progressziója

- tartós, játékoshoz kapcsolódó állapotértékek

- jelenetekhez kapcsolódó progressziós logika

  

Megvalósítás helye:

- `Assets/Scripts/GameManager.cs`

  

---

  

### Ranglistarendszer

  

A ranglistarendszer az egyes feladványszobák teljesítési idejét tárolja és jeleníti meg.

  

Megvalósítás helye:

- `Assets/Scripts/ScoreBoard/ScoreManager.cs`

- `Assets/Scripts/ScoreBoard/ScoreboardUI.cs`

  

---

  

## Használt algoritmusok

  

Több puzzle algoritmikus vagy strukturált logikai megoldásokra épül.

  

### Labirintusgenerálás (Game 6)

  

**Rekurzív backtracking algoritmust** használ procedurális labirintus generálására.

  

Főbb fogalmak:

  

- rácsgenerálás

- mélységi bejárás

- falak eltávolítása a cellák között

  

Fájlok:

- `Assets/Scripts/Game_6/MazeGenerator.cs`

- `Assets/Scripts/Game_6/MazeCell.cs`

  

---

  

### Gráfalapú logikai rendszer (Game 9)

  

Csomópontalapú logikai rendszert valósít meg, amelyben különböző csomóponttípusok dolgozzák fel és továbbítják a jeleket.

  

Fájlok:

- `Assets/Scripts/Game_9/GraphManager.cs`

- `Assets/Scripts/Game_9/GraphNode.cs`

  

---

  

## Tesztelés

  

A projekt automatizált teszteket is tartalmaz a Unity Test Framework segítségével.

  

Két tesztszint található benne:

  

- **EditMode tesztek** a központi logika és állapotkezelés vizsgálatára

- **PlayMode tesztek** a futás közbeni interakciók és játékmeneti viselkedések vizsgálatára

  

A főbb tesztelt területek:

  

- `GameManager`

- `HubManager`

- `InteractionController`

- `KeypadSystem`

- `MainMenuController`

- `PauseManager`

- `ScoreManager`

- egyes futás közbeni játékmeneti viselkedések

  

Tesztek mappái:

- `Assets/Tests/EditModeTests/`

- `Assets/Tests/PlayModeTests/`

  

---

  

## Projektstruktúra

  

```text

Assets

├── Scenes

│ ├── MainMenu

│ ├── MainHub

│ ├── Game_1 ... Game_9

│

├── Scripts

│ ├── Main

│ ├── Interaction

│ ├── ScoreBoard

│ ├── Game_1

│ ├── Game_2

│ ├── Game_3

│ ├── Game_4

│ ├── Game_5

│ ├── Game_6

│ ├── Game_7

│ ├── Game_8

│ └── Game_9

│

└── Tests

├── EditModeTests

└── PlayModeTests

```

## Felhasznált technológiák

  

- **Unity Engine**

- **C#**

- **Unity Test Framework**

  

A Unity biztosítja a motor környezetét, a jelenetkezelést, a felhasználói felület keretrendszerét és a futásidejű rendszereket, míg a játékmeneti logika és a rendszerarchitektúra C# nyelven készült.

  

## A projekt futtatása

  

### Követelmények

  

Használd a projekt konfigurációjában megadott Unity-verziót itt:

  

`ProjectSettings/ProjectVersion.txt`

  

### Lépések

  

1. Klónozd a repository-t:

  

```bash

git clone https://github.com/MrAphell/WhiteRoomEscape.git

```

Nyisd meg a projektet Unity Hubban.

  

2. Nyisd meg a következő jelenetet:

  

3. Assets/Scenes/MainMenu

  

4. Nyomd meg a Play gombot.

  

## Jövőbeli fejlesztések

  

Lehetséges jövőbeli fejlesztések:

  

- a ranglista megjelenítésének további finomítása

- jobb felhasználói visszajelzések

- a puzzle-ök egyensúlyának és kidolgozottságának bővítése

- egyes előkészített rendszerek teljesebb játékmeneti integrációja

- további automatizált tesztek

  

---

  

## Szerző

  

**Polonkai Olivér**

  

*BSc szakdolgozati projekt*

  

---

  

## Licenc

  

A projekt oktatási és kutatási célból készült.# White Room Escape

  

White Room Escape is a modular Unity-based puzzle game developed as a BSc thesis project.

The game consists of multiple puzzle rooms connected through a central hub system.

Each room introduces a different gameplay mechanic and logical challenge while sharing a common interaction, progression, and profile framework.

  

The project focuses on game system architecture, algorithmic puzzle design, and modular gameplay mechanics implemented in C#.

  

---

  

## Overview

  

The player starts from a central hub and progresses through a sequence of puzzle rooms.

Each room contains a distinct mechanic that must be understood and solved in order to complete the level and unlock further progression.

  

Player progression is managed through a profile-based system, while completion times are stored and displayed through a scoreboard subsystem.

  

Core features include:

  

- Main menu and settings system

- Profile management

- Central hub with level progression

- 9 different puzzle rooms

- Shared interaction system

- Scoreboard and time tracking

- Modular game architecture

- Automated EditMode and PlayMode tests

  

---

  

## Gameplay Structure

  

The game consists of **9 puzzle rooms**, each implementing a different logical or gameplay mechanic.

  

| Level | Description |

|------|-------------|

| Game 1 | Introductory puzzle with object interaction and keypad usage |

| Game 2 | Logic puzzle based on switch states and combination handling |

| Game 3 | Exploration-oriented puzzle with light and environmental discovery |

| Game 4 | Grid-based navigation puzzle with rule-based movement |

| Game 5 | Memory and pattern-recognition challenge |

| Game 6 | Procedurally generated maze using a recursive backtracking algorithm |

| Game 7 | Stealth-oriented puzzle with detection and enemy behaviour |

| Game 8 | Multi-step logic puzzle built around interconnected activation states |

| Game 9 | Graph-based logic system using interconnected nodes |

  

Each puzzle room works as an independent gameplay space, while remaining integrated into the global game systems.

  

---

  

## Core Systems

  

### Main Menu System

  

Handles navigation between:

  

- Start Game

- Settings

- Profiles

- Scoreboard

  

Implemented in:

- `Assets/Scripts/Main/MainMenuController.cs`

  

---

  

### Hub System

  

The hub acts as the central progression space where players can access unlocked puzzle rooms.

  

Features:

  

- Level unlocking

- Progression feedback

- Visual distinction between locked, available, and completed levels

  

Implemented in:

- `Assets/Scripts/Main/HubManager.cs`

  

---

  

### Interaction System

  

A generic interaction system allows the player to interact with puzzle elements using raycasting.

  

Key components:

  

- `InteractionController`

- `IInteractable`

  

This system allows puzzles to define their own behavior while sharing a common interaction framework.

  

Implemented in:

- `Assets/Scripts/Interaction/`

  

---

  

### Game Manager

  

The `GameManager` coordinates global game state, including:

  

- active profile handling

- unlocked level progression

- persistent player-related state values

- scene-related progression logic

  

Implemented in:

- `Assets/Scripts/GameManager.cs`

  

---

  

### Scoreboard System

  

The scoreboard subsystem stores and displays completion times for the individual puzzle rooms.

  

Implemented in:

- `Assets/Scripts/ScoreBoard/ScoreManager.cs`

- `Assets/Scripts/ScoreBoard/ScoreboardUI.cs`

  

---

  

## Algorithms Used

  

Several puzzles rely on algorithmic or structured logical solutions.

  

### Maze Generation (Game 6)

  

Uses a **recursive backtracking algorithm** to generate a procedural maze.

  

Key concepts:

  

- grid generation

- depth-first traversal

- wall removal between cells

  

Files:

- `Assets/Scripts/Game_6/MazeGenerator.cs`

- `Assets/Scripts/Game_6/MazeCell.cs`

  

---

  

### Graph Logic System (Game 9)

  

Implements a node-based logical system in which different node types process and propagate signals.

  

Files:

- `Assets/Scripts/Game_9/GraphManager.cs`

- `Assets/Scripts/Game_9/GraphNode.cs`

  

---

  

## Testing

  

The project includes automated tests using Unity Test Framework.

  

Two test levels are present:

  

- **EditMode tests** for central logic and state handling

- **PlayMode tests** for runtime interaction and gameplay behaviour

  

Main tested areas include:

  

- `GameManager`

- `HubManager`

- `InteractionController`

- `KeypadSystem`

- `MainMenuController`

- `PauseManager`

- `ScoreManager`

- selected gameplay-related runtime behaviours

  

Test folders:

- `Assets/Tests/EditModeTests/`

- `Assets/Tests/PlayModeTests/`

  

---

  

## Project Structure

  

```text

Assets

├── Scenes

│ ├── MainMenu

│ ├── MainHub

│ ├── Game_1 ... Game_9

│

├── Scripts

│ ├── Main

│ ├── Interaction

│ ├── ScoreBoard

│ ├── Game_1

│ ├── Game_2

│ ├── Game_3

│ ├── Game_4

│ ├── Game_5

│ ├── Game_6

│ ├── Game_7

│ ├── Game_8

│ └── Game_9

│

└── Tests

├── EditModeTests

└── PlayModeTests

  

```

## Technologies Used

- Unity Engine

- C#

- Unity Test Framework

  

Unity provides the engine environment, scene management, UI framework, and runtime systems, while gameplay logic and system architecture are implemented in C#.

  

## Running the Project

### Requirements

  

Use the Unity version specified by the project configuration in:

ProjectSettings/ProjectVersion.txt

  

### Steps

1. Clone the repository:

```bash

git clone https://github.com/MrAphell/WhiteRoomEscape.git

```

2. Open the project in Unity Hub

3. Open the scene:

Assets/Scenes/MainMenu

4. Press Play

  

## Future Improvements

  

Possible future improvements include:

  

further refinement of scoreboard presentation

improved UI feedback

expanded puzzle balancing and polish

fuller gameplay integration of some partially prepared systems

additional automated tests

Author

  

***Polonkai Olivér***

  

*BSc Thesis Project*

  

# License

  

This project was developed for educational and research purposes.

  

---

  

# Projekt címe

  

Rövid leírás arról, hogy mit csinál ez a projekt, és kiknek készült.

  

# White Room Escape

  

A White Room Escape egy moduláris felépítésű, Unity-alapú puzzle játék, amely BSc szakdolgozati projektként készült.

A játék több feladványszobából áll, amelyeket egy központi hub rendszer köt össze.

Minden szoba eltérő játékmeneti mechanikát és logikai kihívást kínál, miközben közös interakciós, progressziós és profilkezelési keretrendszert használ.

  

A projekt középpontjában a játékrendszerek architektúrája, az algoritmikus puzzle-tervezés, valamint a C# nyelven megvalósított moduláris játékmeneti mechanikák állnak.

  

---

  

## Áttekintés

  

A játékos egy központi hubból indul, majd egymás után halad végig a különböző feladványszobákon.

Minden szoba egy sajátos mechanikát tartalmaz, amelyet meg kell érteni és meg kell oldani a pálya teljesítéséhez és a további haladás feloldásához.

  

A játékos haladását profilalapú rendszer kezeli, míg a teljesítési idők tárolását és megjelenítését egy ranglistarendszer végzi.

  

Főbb funkciók:

  

- Főmenü és beállítási rendszer

- Profilkezelés

- Központi hub pályahaladással

- 9 különböző feladványszoba

- Közös interakciós rendszer

- Ranglista és időmérés

- Moduláris játékarchitektúra

- Automatizált EditMode és PlayMode tesztek

  

---

  

## Játékmenet felépítése

  

A játék **9 feladványszobából** áll, amelyek mindegyike eltérő logikai vagy játékmeneti mechanikát valósít meg.

  

| Pálya | Leírás |

|------|--------|

| Game 1 | Bevezető puzzle objektuminterakcióval és kódpad használattal |

| Game 2 | Logikai puzzle kapcsolóállapotok és kombinációk kezelésével |

| Game 3 | Felfedezésre épülő puzzle fénnyel és környezeti elemekkel |

| Game 4 | Rácsalapú navigációs puzzle szabályalapú mozgással |

| Game 5 | Memória- és mintafelismerési kihívás |

| Game 6 | Procedurálisan generált labirintus rekurzív backtracking algoritmussal |

| Game 7 | Lopakodásalapú puzzle észleléssel és ellenfélviselkedéssel |

| Game 8 | Többlépcsős logikai puzzle egymásra épülő aktiválási állapotokkal |

| Game 9 | Gráfalapú logikai rendszer összekapcsolt csomópontokkal |

  

Minden feladványszoba önálló játéktérként működik, miközben integrálódik a globális játékrendszerbe.

  

---

  

## Fő rendszerek

  

### Főmenü rendszer

  

A navigációt az alábbi elemek között kezeli:

  

- Játék indítása

- Beállítások

- Profilok

- Ranglista

  

Megvalósítás helye:

- `Assets/Scripts/Main/MainMenuController.cs`

  

---

  

### Hub rendszer

  

A hub a központi haladási tér, ahol a játékos hozzáférhet a feloldott feladványszobákhoz.

  

Főbb funkciók:

  

- Pályafeloldás

- Progressziós visszajelzés

- A zárolt, elérhető és teljesített pályák vizuális megkülönböztetése

  

Megvalósítás helye:

- `Assets/Scripts/Main/HubManager.cs`

  

---

  

### Interakciós rendszer

  

Az általános interakciós rendszer lehetővé teszi, hogy a játékos raycasting segítségével kapcsolatba lépjen a puzzle-elemekkel.

  

Fő komponensek:

  

- `InteractionController`

- `IInteractable`

  

Ez a rendszer lehetővé teszi, hogy az egyes puzzle-elemek saját viselkedést definiáljanak, miközben közös interakciós keretrendszert használnak.

  

Megvalósítás helye:

- `Assets/Scripts/Interaction/`

  

---

  

### Game Manager

  

A `GameManager` a globális játékállapot koordinálását végzi, beleértve az alábbiakat:

  

- aktív profil kezelése

- feloldott pályák progressziója

- tartós, játékoshoz kapcsolódó állapotértékek

- jelenetekhez kapcsolódó progressziós logika

  

Megvalósítás helye:

- `Assets/Scripts/GameManager.cs`

  

---

  

### Ranglistarendszer

  

A ranglistarendszer az egyes feladványszobák teljesítési idejét tárolja és jeleníti meg.

  

Megvalósítás helye:

- `Assets/Scripts/ScoreBoard/ScoreManager.cs`

- `Assets/Scripts/ScoreBoard/ScoreboardUI.cs`

  

---

  

## Használt algoritmusok

  

Több puzzle algoritmikus vagy strukturált logikai megoldásokra épül.

  

### Labirintusgenerálás (Game 6)

  

**Rekurzív backtracking algoritmust** használ procedurális labirintus generálására.

  

Főbb fogalmak:

  

- rácsgenerálás

- mélységi bejárás

- falak eltávolítása a cellák között

  

Fájlok:

- `Assets/Scripts/Game_6/MazeGenerator.cs`

- `Assets/Scripts/Game_6/MazeCell.cs`

  

---

  

### Gráfalapú logikai rendszer (Game 9)

  

Csomópontalapú logikai rendszert valósít meg, amelyben különböző csomóponttípusok dolgozzák fel és továbbítják a jeleket.

  

Fájlok:

- `Assets/Scripts/Game_9/GraphManager.cs`

- `Assets/Scripts/Game_9/GraphNode.cs`

  

---

  

## Tesztelés

  

A projekt automatizált teszteket is tartalmaz a Unity Test Framework segítségével.

  

Két tesztszint található benne:

  

- **EditMode tesztek** a központi logika és állapotkezelés vizsgálatára

- **PlayMode tesztek** a futás közbeni interakciók és játékmeneti viselkedések vizsgálatára

  

A főbb tesztelt területek:

  

- `GameManager`

- `HubManager`

- `InteractionController`

- `KeypadSystem`

- `MainMenuController`

- `PauseManager`

- `ScoreManager`

- egyes futás közbeni játékmeneti viselkedések

  

Tesztek mappái:

- `Assets/Tests/EditModeTests/`

- `Assets/Tests/PlayModeTests/`

  

---

  

## Projektstruktúra

  

```text

Assets

├── Scenes

│ ├── MainMenu

│ ├── MainHub

│ ├── Game_1 ... Game_9

│

├── Scripts

│ ├── Main

│ ├── Interaction

│ ├── ScoreBoard

│ ├── Game_1

│ ├── Game_2

│ ├── Game_3

│ ├── Game_4

│ ├── Game_5

│ ├── Game_6

│ ├── Game_7

│ ├── Game_8

│ └── Game_9

│

└── Tests

├── EditModeTests

└── PlayModeTests

```

## Felhasznált technológiák

  

- **Unity Engine**

- **C#**

- **Unity Test Framework**

  

A Unity biztosítja a motor környezetét, a jelenetkezelést, a felhasználói felület keretrendszerét és a futásidejű rendszereket, míg a játékmeneti logika és a rendszerarchitektúra C# nyelven készült.

  

## A projekt futtatása

  

### Követelmények

  

Használd a projekt konfigurációjában megadott Unity-verziót itt:

  

`ProjectSettings/ProjectVersion.txt`

  

### Lépések

  

1. Klónozd a repository-t:

  

```bash

git clone https://github.com/MrAphell/WhiteRoomEscape.git

```

Nyisd meg a projektet Unity Hubban.

  

2. Nyisd meg a következő jelenetet:

  

3. Assets/Scenes/MainMenu

  

4. Nyomd meg a Play gombot.

  

## Jövőbeli fejlesztések

  

Lehetséges jövőbeli fejlesztések:

  

- a ranglista megjelenítésének további finomítása

- jobb felhasználói visszajelzések

- a puzzle-ök egyensúlyának és kidolgozottságának bővítése

- egyes előkészített rendszerek teljesebb játékmeneti integrációja

- további automatizált tesztek

  

---

  

## Szerző

  

**Polonkai Olivér**

  

*BSc szakdolgozati projekt*

  

---

  

## Licenc

  

A projekt oktatási és kutatási célból készült.# White Room Escape

  

White Room Escape is a modular Unity-based puzzle game developed as a BSc thesis project.

The game consists of multiple puzzle rooms connected through a central hub system.

Each room introduces a different gameplay mechanic and logical challenge while sharing a common interaction, progression, and profile framework.

  

The project focuses on game system architecture, algorithmic puzzle design, and modular gameplay mechanics implemented in C#.

  

---

  

## Overview

  

The player starts from a central hub and progresses through a sequence of puzzle rooms.

Each room contains a distinct mechanic that must be understood and solved in order to complete the level and unlock further progression.

  

Player progression is managed through a profile-based system, while completion times are stored and displayed through a scoreboard subsystem.

  

Core features include:

  

- Main menu and settings system

- Profile management

- Central hub with level progression

- 9 different puzzle rooms

- Shared interaction system

- Scoreboard and time tracking

- Modular game architecture

- Automated EditMode and PlayMode tests

  

---

  

## Gameplay Structure

  

The game consists of **9 puzzle rooms**, each implementing a different logical or gameplay mechanic.

  

| Level | Description |

|------|-------------|

| Game 1 | Introductory puzzle with object interaction and keypad usage |

| Game 2 | Logic puzzle based on switch states and combination handling |

| Game 3 | Exploration-oriented puzzle with light and environmental discovery |

| Game 4 | Grid-based navigation puzzle with rule-based movement |

| Game 5 | Memory and pattern-recognition challenge |

| Game 6 | Procedurally generated maze using a recursive backtracking algorithm |

| Game 7 | Stealth-oriented puzzle with detection and enemy behaviour |

| Game 8 | Multi-step logic puzzle built around interconnected activation states |

| Game 9 | Graph-based logic system using interconnected nodes |

  

Each puzzle room works as an independent gameplay space, while remaining integrated into the global game systems.

  

---

  

## Core Systems

  

### Main Menu System

  

Handles navigation between:

  

- Start Game

- Settings

- Profiles

- Scoreboard

  

Implemented in:

- `Assets/Scripts/Main/MainMenuController.cs`

  

---

  

### Hub System

  

The hub acts as the central progression space where players can access unlocked puzzle rooms.

  

Features:

  

- Level unlocking

- Progression feedback

- Visual distinction between locked, available, and completed levels

  

Implemented in:

- `Assets/Scripts/Main/HubManager.cs`

  

---

  

### Interaction System

  

A generic interaction system allows the player to interact with puzzle elements using raycasting.

  

Key components:

  

- `InteractionController`

- `IInteractable`

  

This system allows puzzles to define their own behavior while sharing a common interaction framework.

  

Implemented in:

- `Assets/Scripts/Interaction/`

  

---

  

### Game Manager

  

The `GameManager` coordinates global game state, including:

  

- active profile handling

- unlocked level progression

- persistent player-related state values

- scene-related progression logic

  

Implemented in:

- `Assets/Scripts/GameManager.cs`

  

---

  

### Scoreboard System

  

The scoreboard subsystem stores and displays completion times for the individual puzzle rooms.

  

Implemented in:

- `Assets/Scripts/ScoreBoard/ScoreManager.cs`

- `Assets/Scripts/ScoreBoard/ScoreboardUI.cs`

  

---

  

## Algorithms Used

  

Several puzzles rely on algorithmic or structured logical solutions.

  

### Maze Generation (Game 6)

  

Uses a **recursive backtracking algorithm** to generate a procedural maze.

  

Key concepts:

  

- grid generation

- depth-first traversal

- wall removal between cells

  

Files:

- `Assets/Scripts/Game_6/MazeGenerator.cs`

- `Assets/Scripts/Game_6/MazeCell.cs`

  

---

  

### Graph Logic System (Game 9)

  

Implements a node-based logical system in which different node types process and propagate signals.

  

Files:

- `Assets/Scripts/Game_9/GraphManager.cs`

- `Assets/Scripts/Game_9/GraphNode.cs`

  

---

  

## Testing

  

The project includes automated tests using Unity Test Framework.

  

Two test levels are present:

  

- **EditMode tests** for central logic and state handling

- **PlayMode tests** for runtime interaction and gameplay behaviour

  

Main tested areas include:

  

- `GameManager`

- `HubManager`

- `InteractionController`

- `KeypadSystem`

- `MainMenuController`

- `PauseManager`

- `ScoreManager`

- selected gameplay-related runtime behaviours

  

Test folders:

- `Assets/Tests/EditModeTests/`

- `Assets/Tests/PlayModeTests/`

  

---

  

## Project Structure

  

```text

Assets

├── Scenes

│ ├── MainMenu

│ ├── MainHub

│ ├── Game_1 ... Game_9

│

├── Scripts

│ ├── Main

│ ├── Interaction

│ ├── ScoreBoard

│ ├── Game_1

│ ├── Game_2

│ ├── Game_3

│ ├── Game_4

│ ├── Game_5

│ ├── Game_6

│ ├── Game_7

│ ├── Game_8

│ └── Game_9

│

└── Tests

├── EditModeTests

└── PlayModeTests

  

```

## Technologies Used

- Unity Engine

- C#

- Unity Test Framework

  

Unity provides the engine environment, scene management, UI framework, and runtime systems, while gameplay logic and system architecture are implemented in C#.

  

## Running the Project

### Requirements

  

Use the Unity version specified by the project configuration in:

ProjectSettings/ProjectVersion.txt

  

### Steps

1. Clone the repository:

	```bash

	git clone https://github.com/MrAphell/WhiteRoomEscape.git

	```

2. Open the project in Unity Hub

3. Open the scene:

	Assets/Scenes/MainMenu

4. Press Play

  

## Future Improvements

  

Possible future improvements include:

  

- further refinement of scoreboard presentation

- improved UI feedback

- expanded puzzle balancing and polish

- fuller gameplay integration of some partially prepared systems

- additional automated tests

---

Author:
  

***Polonkai Olivér***

  

*BSc Thesis Project*

  

# License

  

This project was developed for educational and research purposes.

  

---

  

# White Room Escape

  

A White Room Escape egy moduláris felépítésű, Unity-alapú puzzle játék, amely BSc szakdolgozati projektként készült.

A játék több feladványszobából áll, amelyeket egy központi hub rendszer köt össze.

Minden szoba eltérő játékmeneti mechanikát és logikai kihívást kínál, miközben közös interakciós, progressziós és profilkezelési keretrendszert használ.

  

A projekt középpontjában a játékrendszerek architektúrája, az algoritmikus puzzle-tervezés, valamint a C# nyelven megvalósított moduláris játékmeneti mechanikák állnak.

  

---

  

## Áttekintés

  

A játékos egy központi hubból indul, majd egymás után halad végig a különböző feladványszobákon.

Minden szoba egy sajátos mechanikát tartalmaz, amelyet meg kell érteni és meg kell oldani a pálya teljesítéséhez és a további haladás feloldásához.

  

A játékos haladását profilalapú rendszer kezeli, míg a teljesítési idők tárolását és megjelenítését egy ranglistarendszer végzi.

  

Főbb funkciók:

  

- Főmenü és beállítási rendszer

- Profilkezelés

- Központi hub pályahaladással

- 9 különböző feladványszoba

- Közös interakciós rendszer

- Ranglista és időmérés

- Moduláris játékarchitektúra

- Automatizált EditMode és PlayMode tesztek

  

---

  

## Játékmenet felépítése

  

A játék **9 feladványszobából** áll, amelyek mindegyike eltérő logikai vagy játékmeneti mechanikát valósít meg.

  

| Pálya | Leírás |

|------|--------|

| Game 1 | Bevezető puzzle objektuminterakcióval és kódpad használattal |

| Game 2 | Logikai puzzle kapcsolóállapotok és kombinációk kezelésével |

| Game 3 | Felfedezésre épülő puzzle fénnyel és környezeti elemekkel |

| Game 4 | Rácsalapú navigációs puzzle szabályalapú mozgással |

| Game 5 | Memória- és mintafelismerési kihívás |

| Game 6 | Procedurálisan generált labirintus rekurzív backtracking algoritmussal |

| Game 7 | Lopakodásalapú puzzle észleléssel és ellenfélviselkedéssel |

| Game 8 | Többlépcsős logikai puzzle egymásra épülő aktiválási állapotokkal |

| Game 9 | Gráfalapú logikai rendszer összekapcsolt csomópontokkal |

  

Minden feladványszoba önálló játéktérként működik, miközben integrálódik a globális játékrendszerbe.

  

---

  

## Fő rendszerek

  

### Főmenü rendszer

  

A navigációt az alábbi elemek között kezeli:

  

- Játék indítása

- Beállítások

- Profilok

- Ranglista

  

Megvalósítás helye:

- `Assets/Scripts/Main/MainMenuController.cs`

  

---

  

### Hub rendszer

  

A hub a központi haladási tér, ahol a játékos hozzáférhet a feloldott feladványszobákhoz.

  

Főbb funkciók:

  

- Pályafeloldás

- Progressziós visszajelzés

- A zárolt, elérhető és teljesített pályák vizuális megkülönböztetése

  

Megvalósítás helye:

- `Assets/Scripts/Main/HubManager.cs`

  

---

  

### Interakciós rendszer

  

Az általános interakciós rendszer lehetővé teszi, hogy a játékos raycasting segítségével kapcsolatba lépjen a puzzle-elemekkel.

  

Fő komponensek:

  

- `InteractionController`

- `IInteractable`

  

Ez a rendszer lehetővé teszi, hogy az egyes puzzle-elemek saját viselkedést definiáljanak, miközben közös interakciós keretrendszert használnak.

  

Megvalósítás helye:

- `Assets/Scripts/Interaction/`

  

---

  

### Game Manager

  

A `GameManager` a globális játékállapot koordinálását végzi, beleértve az alábbiakat:

  

- aktív profil kezelése

- feloldott pályák progressziója

- tartós, játékoshoz kapcsolódó állapotértékek

- jelenetekhez kapcsolódó progressziós logika

  

Megvalósítás helye:

- `Assets/Scripts/GameManager.cs`

  

---

  

### Ranglistarendszer

  

A ranglistarendszer az egyes feladványszobák teljesítési idejét tárolja és jeleníti meg.

  

Megvalósítás helye:

- `Assets/Scripts/ScoreBoard/ScoreManager.cs`

- `Assets/Scripts/ScoreBoard/ScoreboardUI.cs`

  

---

  

## Használt algoritmusok

  

Több puzzle algoritmikus vagy strukturált logikai megoldásokra épül.

  

### Labirintusgenerálás (Game 6)

  

**Rekurzív backtracking algoritmust** használ procedurális labirintus generálására.

  

Főbb fogalmak:

  

- rácsgenerálás

- mélységi bejárás

- falak eltávolítása a cellák között

  

Fájlok:

- `Assets/Scripts/Game_6/MazeGenerator.cs`

- `Assets/Scripts/Game_6/MazeCell.cs`

  

---

  

### Gráfalapú logikai rendszer (Game 9)

  

Csomópontalapú logikai rendszert valósít meg, amelyben különböző csomóponttípusok dolgozzák fel és továbbítják a jeleket.

  

Fájlok:

- `Assets/Scripts/Game_9/GraphManager.cs`

- `Assets/Scripts/Game_9/GraphNode.cs`

  

---

  

## Tesztelés

  

A projekt automatizált teszteket is tartalmaz a Unity Test Framework segítségével.

  

Két tesztszint található benne:

  

- **EditMode tesztek** a központi logika és állapotkezelés vizsgálatára

- **PlayMode tesztek** a futás közbeni interakciók és játékmeneti viselkedések vizsgálatára

  

A főbb tesztelt területek:

  

- `GameManager`

- `HubManager`

- `InteractionController`

- `KeypadSystem`

- `MainMenuController`

- `PauseManager`

- `ScoreManager`

- egyes futás közbeni játékmeneti viselkedések

  

Tesztek mappái:

- `Assets/Tests/EditModeTests/`

- `Assets/Tests/PlayModeTests/`

  

---

  

## Projektstruktúra

  

```text

Assets

├── Scenes

│ ├── MainMenu

│ ├── MainHub

│ ├── Game_1 ... Game_9

│

├── Scripts

│ ├── Main

│ ├── Interaction

│ ├── ScoreBoard

│ ├── Game_1

│ ├── Game_2

│ ├── Game_3

│ ├── Game_4

│ ├── Game_5

│ ├── Game_6

│ ├── Game_7

│ ├── Game_8

│ └── Game_9

│

└── Tests

├── EditModeTests

└── PlayModeTests

```

## Felhasznált technológiák

  

- **Unity Engine**

- **C#**

- **Unity Test Framework**

  

A Unity biztosítja a motor környezetét, a jelenetkezelést, a felhasználói felület keretrendszerét és a futásidejű rendszereket, míg a játékmeneti logika és a rendszerarchitektúra C# nyelven készült.

  

## A projekt futtatása

  

### Követelmények

  

Használd a projekt konfigurációjában megadott Unity-verziót itt:

  

`ProjectSettings/ProjectVersion.txt`

  

### Lépések

  

1. Klónozd a repository-t:

  

	```bash

	git clone https://github.com/MrAphell/WhiteRoomEscape.git

	```

	Nyisd meg a projektet Unity Hubban.

  

2. Nyisd meg a következő jelenetet:

  

3. Assets/Scenes/MainMenu

  

4. Nyomd meg a Play gombot.

  

## Jövőbeli fejlesztések

  

Lehetséges jövőbeli fejlesztések:

  

- a ranglista megjelenítésének további finomítása

- jobb felhasználói visszajelzések

- a puzzle-ök egyensúlyának és kidolgozottságának bővítése

- egyes előkészített rendszerek teljesebb játékmeneti integrációja

- további automatizált tesztek

  

---


## Szerző

  

***Polonkai Olivér***

  

*BSc szakdolgozati projekt*

  

---

  

## Licenc

  

A projekt oktatási és kutatási célból készült
