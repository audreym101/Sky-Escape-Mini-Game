# Sky Escape Mini Game

A 3D mini game built in Unity where the player races against enemies to collect all coins before they do.

## Gameplay

- Move the player using **W A S D** keys
- **Collect all coins** before the enemies do to win
- Avoid enemies — they chase and attack you, taking away hearts
- If you lose all hearts, it's **Game Over**

## Win & Lose Conditions

| Condition | Result |
|---|---|
| Player collects all coins first | **You Win** |
| Enemy collects all coins first | **Enemy Wins** |
| Player loses all hearts | **Game Over** |

## HUD

- **Score** — increases by 1 for each coin collected
- **Health** — shows remaining hearts `[HP]`
- **♥ -1 Heart!** — notification popup when the player takes damage

## Controls

| Key | Action |
|---|---|
| W / A / S / D | Move |
| Space | Jump |

## Scenes

- `MenuScene` — main menu with Play and Quit buttons
- `SampleScene` — main game scene

## Project Structure

```
Assets/
├── Scripts/
│   ├── Core/         # Character base class, Camera follow
│   ├── Enemy/        # Enemy AI and factory
│   ├── Managers/     # GameManager, ScoreManager, UIManager, WinManager, GameOverManager
│   ├── Player/       # Player controller, PlayerHealth, Coin
│   ├── States/       # Enemy state machine (Idle, Chase, Attack)
│   └── UI/           # Leaderboard
├── Scenes/
├── Prefabs/
└── Assets/           # Third party assets (coins, skeleton, trees, prison pack)
```

## Built With

- Unity (URP)
- C#
- TextMeshPro
- Starter Assets — Third Person Controller
- PolyKebap Stylized Coin Pack
- asoliddev Low Poly Dynamic Skeleton
- 7th Side Modular Prison Asset Pack
