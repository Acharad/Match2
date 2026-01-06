# Match2

A performance-oriented Match-2 puzzle game framework built in Unity. This project demonstrates a service-based architecture, efficient memory management via custom pooling, and robust gameplay algorithms.

## ⚙️ Technical Features

* **Service-Oriented Architecture:** Game logic involves isolated services (Input, Board, Audio) managed by a central Service Locator pattern for modularity.
* **Advanced Grid Logic:** * **Recursive Match Detection:** Implements efficient flood-fill algorithms to identify matching groups.
  * **Deadlock Detection:** Automatically detects unplayable states and reshuffles the board via `DeadlockDetector`.
  * **Gravity System:** Custom `GravityHandler` manages smooth item falling and grid replenishment.
* **Optimized Performance:** Uses a custom `ItemPool` system to minimize Garbage Collection (GC) overhead during high-frequency item spawning.
* **Data-Driven Design:** Level configurations and item properties are managed via ScriptableObjects (`LevelData`, `ItemUIData`).
* **Input Abstraction:** Supports both Mouse and Touch inputs through a unified `IInput` interface.
* **Visual Feedback:** Integrated DOTween for smooth UI and gameplay animations.

## 🛠 Tech Stack

* **Engine:** Unity 2021.3+
* **Language:** C#
* **Architecture:** MVC / Service Locator
* **Dependencies:** DOTween

## 🚀 Getting Started

1.  Clone the repository.
2.  Open the project in Unity.
3.  Navigate to `Assets/Scenes/SampleScene` (or MainScene).
4.  Press **Play**.
