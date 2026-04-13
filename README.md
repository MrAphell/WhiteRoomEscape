
# White Room Escape

  

*Choose language / Válassz nyelvet: [English](#english-version) | [Magyar](#magyar-verzió)*

  

---

  

## <a id="english-version"></a>English Version

  


***White Room Escape*** is a modular, Unity-based puzzle game developed as a BSc thesis project. The game consists of multiple puzzle rooms connected by a central hub system. Each room introduces a different gameplay mechanic and logical challenge, while utilizing a shared interaction, progression, and profile management framework.

The project focuses on game systems architecture, algorithmic puzzle design, and modular gameplay mechanics implemented in C#.

### Overview
The player starts from a central hub and progresses sequentially through the various puzzle rooms. Each room contains a unique mechanic that must be understood and solved to complete the level and unlock further progress. Player progression is handled by a profile-based system, while completion times are stored and displayed by a scoreboard system.

**Core features:**
* Main menu and settings system
* Profile management
* Central hub with level progression
* 9 different puzzle rooms
* Shared interaction system
* Scoreboard and time tracking
* Modular game architecture
* Automated EditMode and PlayMode tests

### Gameplay Structure
The game consists of **9 puzzle rooms**, each implementing a distinct logical or gameplay mechanic.

| Level | Description |
|---|---|
| **Game 1** | Introductory puzzle with object interaction and keypad usage |
| **Game 2** | Logic puzzle handling switch states and combinations |
| **Game 3** | Exploration-based puzzle with light and environmental elements |
| **Game 4** | Grid-based navigation puzzle with rule-based movement |
| **Game 5** | Memory and pattern recognition challenge |
| **Game 6** | Procedurally generated maze using a recursive backtracking algorithm |
| **Game 7** | Stealth-based puzzle with detection and enemy behavior |
| **Game 8** | Multi-step logic puzzle with interconnected activation states |
| **Game 9** | Graph-based logic system with interconnected nodes |

### Core Systems
* **Main Menu System:** Navigation between Start Game, Settings, Profiles, and Scoreboard. (`Assets/Scripts/Main/MainMenuController.cs`)
* **Hub System:** The central progression space where the player can access unlocked puzzle rooms. (`Assets/Scripts/Main/HubManager.cs`)
* **Interaction System:** Raycasting-based general interaction system (`InteractionController`, `IInteractable`). (`Assets/Scripts/Interaction/`)
* **Game Manager:** Manages global game state, active profile, and progression. (`Assets/Scripts/GameManager.cs`)
* **Scoreboard System:** Stores and displays completion times. (`Assets/Scripts/ScoreBoard/`)

### Algorithms Used
* **Maze Generation (Game 6):** Uses a **recursive backtracking algorithm** to generate procedural mazes.
* **Graph-based Logic System (Game 9):** Implements a node-based logic system where different node types process and forward signals.

### Testing
The project includes automated tests using the Unity Test Framework:
* **EditMode tests:** To examine core logic and state management.
* **PlayMode tests:** To examine runtime interactions and gameplay behaviors.

### Technologies Used
* **Unity Engine**
* **C#**
* **Unity Test Framework**

### Running the Project
**Requirements:** Use the Unity version specified in the project configuration: `ProjectSettings/ProjectVersion.txt`.

### Running the Project
**Requirements:** se the Unity version specified by the project configuration in `ProjectSettings/ProjectVersion.txt`.

1. Clone the repository:
   ```bash
	   git clone https://github.com/MrAphell/WhiteRoomEscape.git
	```
2.  Open the project in Unity Hub.
    
3.  Open the scene: `Assets/Scenes/MainMenu`
    
4.  Press Play!

### Future Improvements
* Further refinement of the scoreboard display
* Better user feedback
* Expanding puzzle balance and polish
* More complete gameplay integration of certain prepared systems
* Additional automated tests

## Project Structure
```text
Assets
├── Scenes
│   ├── MainMenu
│   ├── MainHub
│   └── Game_1 ... Game_9
├── Scripts
│   ├── Main
│   ├── Interaction
│   ├── ScoreBoard
│   └── Game_1 ... Game_9
└── Tests
    ├── EditModeTests
    └── PlayModeTests
   ```

## Author

**Polonkai Olivér** _BSc Thesis Project_

## License
This project was developed for educational and research purposes.

---

## <a id="magyar-verzió"></a>Magyar Verzió

A ***White Room Escape*** egy moduláris felépítésű, Unity-alapú puzzle játék, amely BSc szakdolgozati projektként készült. A játék több feladványszobából áll, amelyeket egy központi hub rendszer köt össze. Minden szoba eltérő játékmeneti mechanikát és logikai kihívást kínál, miközben közös interakciós, progressziós és profilkezelési keretrendszert használ.

A projekt középpontjában a játékrendszerek architektúrája, az algoritmikus puzzle-tervezés, valamint a C# nyelven megvalósított moduláris játékmeneti mechanikák állnak.

### Áttekintés
A játékos egy központi hubból indul, majd egymás után halad végig a különböző feladványszobákon. Minden szoba egy sajátos mechanikát tartalmaz, amelyet meg kell érteni és meg kell oldani a pálya teljesítéséhez és a további haladás feloldásához. A játékos haladását profilalapú rendszer kezeli, míg a teljesítési idők tárolását és megjelenítését egy ranglistarendszer végzi.

**Főbb funkciók:**
* Főmenü és beállítási rendszer
* Profilkezelés
* Központi hub pályahaladással
* 9 különböző feladványszoba
* Közös interakciós rendszer
* Ranglista és időmérés
* Moduláris játékarchitektúra
* Automatizált EditMode és PlayMode tesztek

### Játékmenet felépítése
A játék **9 feladványszobából** áll, amelyek mindegyike eltérő logikai vagy játékmeneti mechanikát valósít meg.

| Pálya | Leírás |
|---|---|
| **Game 1** | Bevezető puzzle objektuminterakcióval és kódpad használattal |
| **Game 2** | Logikai puzzle kapcsolóállapotok és kombinációk kezelésével |
| **Game 3** | Felfedezésre épülő puzzle fénnyel és környezeti elemekkel |
| **Game 4** | Rácsalapú navigációs puzzle szabályalapú mozgással |
| **Game 5** | Memória- és mintafelismerési kihívás |
| **Game 6** | Procedurálisan generált labirintus rekurzív backtracking algoritmussal |
| **Game 7** | Lopakodásalapú puzzle észleléssel és ellenfélviselkedéssel |
| **Game 8** | Többlépcsős logikai puzzle egymásra épülő aktiválási állapotokkal |
| **Game 9** | Gráfalapú logikai rendszer összekapcsolt csomópontokkal |

### Fő rendszerek
* **Főmenü rendszer:** Navigáció a Játék indítása, Beállítások, Profilok és Ranglista között. (`Assets/Scripts/Main/MainMenuController.cs`)
* **Hub rendszer:** A központi haladási tér, ahol a játékos hozzáférhet a feloldott feladványszobákhoz. (`Assets/Scripts/Main/HubManager.cs`)
* **Interakciós rendszer:** Raycasting alapú általános interakciós rendszer (`InteractionController`, `IInteractable`). (`Assets/Scripts/Interaction/`)
* **Game Manager:** Globális játékállapot, aktív profil és progresszió kezelése. (`Assets/Scripts/GameManager.cs`)
* **Ranglistarendszer:** A teljesítési idők tárolása és megjelenítése. (`Assets/Scripts/ScoreBoard/`)

### Használt algoritmusok
* **Labirintusgenerálás (Game 6):** **Rekurzív backtracking algoritmust** használ procedurális labirintus generálására.
* **Gráfalapú logikai rendszer (Game 9):** Csomópontalapú logikai rendszert valósít meg, amelyben különböző csomóponttípusok dolgozzák fel és továbbítják a jeleket.

### Tesztelés
A projekt automatizált teszteket tartalmaz a Unity Test Framework segítségével:
* **EditMode tesztek:** A központi logika és állapotkezelés vizsgálatára.
* **PlayMode tesztek:** A futás közbeni interakciók és játékmeneti viselkedések vizsgálatára.

### Felhasznált technológiák
* **Unity Engine**
* **C#**
* **Unity Test Framework**

### A projekt futtatása
**Követelmények:** Használd a projekt konfigurációjában megadott Unity-verziót: `ProjectSettings/ProjectVersion.txt`.

1. Klónozd a repository-t:
   ```bash
	   git clone https://github.com/MrAphell/WhiteRoomEscape.git
	```
2.  Nyisd meg a projektet Unity Hubban.
    
3.  Nyisd meg a következő jelenetet: `Assets/Scenes/MainMenu`
    
4.  Nyomd meg a Play gombot.
    

### Jövőbeli fejlesztések

-   A ranglista megjelenítésének további finomítása
    
-   Jobb felhasználói visszajelzések
    
-   A puzzle-ök egyensúlyának és kidolgozottságának bővítése
    
-   Egyes előkészített rendszerek teljesebb játékmeneti integrációja
    
-   További automatizált tesztek

## Project Structure / Projektstruktúra

```text
Assets
├── Scenes
│   ├── MainMenu
│   ├── MainHub
│   └── Game_1 ... Game_9
├── Scripts
│   ├── Main
│   ├── Interaction
│   ├── ScoreBoard
│   └── Game_1 ... Game_9
└── Tests
    ├── EditModeTests
    └── PlayModeTests
   ```
    
## Author / Szerző

**Polonkai Olivér** _BSc Szakdolgozati projekt_

## License / Licenc

A projekt oktatási és kutatási célból készült.
