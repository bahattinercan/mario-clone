# Mario Clone

A 2D side-scrolling platformer prototype built in Unity where the player navigates a linear level, jumps across platforms, collects coins, stomps or shoots hostile creatures (such as beetles, snails, frogs, and spiders), avoids hazards like water, and confronts an attacking boss. This repository is a personal prototype/demo project, not a maintained product, with the last substantial work committed in 2022.

## Gameplay

- **Player Movement & Physics**: Horizontal walking and running with velocity control and directional sprite flipping (`Assets/Scripts/Player Scripts/PlayerMovement.cs`).
- **Jumping & Ground Check**: Jump execution with dynamic velocity and ground detection using 2D box casting (`Assets/Scripts/Player Scripts/PlayerMovement.cs`).
- **Shooting Projectiles**: Spawning fire bullet projectiles with orientation-based horizontal trajectory when pressing the shoot key (`Assets/Scripts/Player Scripts/PlayerShoot.cs`).
- **Bullet Lifecycle & Collisions**: Fire bullet movement, timeout auto-destruction, and collision explosion upon contacting enemies or blocks (`Assets/Scripts/Player Scripts/FireBullet.cs`).
- **Health & Damage System**: Three-life counter, damage cooldown with temporary invulnerability, and automatic scene reload on death (`Assets/Scripts/Player Scripts/PlayerDamage.cs`).
- **Coin Collection & Scoring**: Trigger-based coin pickups that increment the score counter, update the UI display, and play audio (`Assets/Scripts/Player Scripts/Score.cs`).
- **Camera Tracking**: Smooth horizontal camera following using `Vector3.SmoothDamp` (`Assets/Scripts/Camera Scripts/CameraFollow.cs`).
- **Dynamic Background Resizing**: Automatic scaling of background sprites to fit the camera viewport and screen aspect ratio (`Assets/Scripts/Background Scripts/Background.cs`).
- **Interactive Bonus Blocks**: Raycast-detected head-bump interaction that bounces blocks upward when struck from beneath (`Assets/Scripts/Blocks Scripts/BonusBlock.cs`).
- **Water Hazards**: Hazard trigger volume that inflicts damage on player contact and teleports the character back to the respawn point (`Assets/Scripts/Blocks Scripts/Water.cs`).
- **Flying Bird Enemy**: Horizontal aerial patrol enemy that raycasts downward to detect the player and drop falling eggs (`Assets/Scripts/Enemy Scripts/Bird.cs`).
- **Bird Egg Hazards**: Falling projectiles dropped by birds that damage the player on impact (`Assets/Scripts/Enemy Scripts/Egg.cs`).
- **Jumping Frog Enemy**: Timed hop routines with directional facing and top-stomp hit registration that bounces the player and eliminates the frog (`Assets/Scripts/Enemy Scripts/Frog.cs`).
- **Snail & Beetle Enemies**: Ground patrol enemies reversing at platform edges; top-stomping stuns snails or defeats beetles, and stunned snails can be kicked as sliding shells (`Assets/Scripts/Enemy Scripts/Snail.cs`).
- **Spider Enemy**: Oscillating vertical movement enemy that damages the player on contact and can be stomped or shot (`Assets/Scripts/Enemy Scripts/Spider.cs`).
- **Boss Attack Pattern**: Boss entity executing timed projectile attacks throwing stones at randomized velocities (`Assets/Scripts/Enemy Scripts/Boss4.cs`).
- **Boss Health System**: Hit point tracking vulnerable to player bullets, triggering boss deactivation and death animations (`Assets/Scripts/Enemy Scripts/Boss4Health.cs`).
- **Boss Stone Projectile**: Thrown boulder projectile that damages the player on collision and deactivates over time (`Assets/Scripts/Enemy Scripts/Stone.cs`).
- **Target Frame Rate Initialization**: Enforces a 60 FPS target frame rate on startup (`Assets/Scripts/Helper Scripts/MySettings.cs`).
- **Collision & Tag Constants**: Static string constants mapping object tags across gameplay systems (`Assets/Scripts/Helper Scripts/MyTags.cs`).

## Tech Stack

- **Game Engine**: Unity 2020.3.20f1 (declared in `ProjectSettings/ProjectVersion.txt`)
- **Language**: C# (.NET / Mono runtime)
- **Key Packages** (from `Packages/manifest.json`):
  - Unity 2D Animation (`com.unity.2d.animation`: 5.0.7)
  - Unity 2D Pixel Perfect (`com.unity.2d.pixel-perfect`: 4.0.1)
  - Unity 2D Sprite (`com.unity.2d.sprite`: 1.0.0)
  - Unity 2D SpriteShape (`com.unity.2d.spriteshape`: 5.1.4)
  - Unity 2D Tilemap (`com.unity.2d.tilemap`: 1.0.0)
  - TextMesh Pro (`com.unity.textmeshpro`: 3.0.6)
  - Unity UI (`com.unity.ugui`: 1.0.0)

## Project Structure

| Path | Contents | Classification |
| --- | --- | --- |
| `Assets/Scripts/` | C# scripts for player, enemies, camera, blocks, and gameplay helpers | Own code |
| `Assets/Scenes/` | Scene files (`Gameplay.unity` and `MainMenu.unity`) | Data / Scenes |
| `Assets/Prefabs/` | Prefab assets for player, enemies, items, and obstacles | Data / Prefabs |
| `Assets/Animations/` | Animator controllers and animation clips for entities and interactive blocks | Third-party / Asset data |
| `Assets/Sprites/` | 2D sprite sheets for characters, environment tilesets, UI, and particles | Third-party / Asset data |
| `Assets/Sounds/` | Audio files for jump, coin pickup, game over, and level clear effects | Third-party / Asset data |
| `Assets/Physics/` | 2D Physics materials | Data / Assets |
| `Assets/Tile Palettes/` | Tilemap palette data for 2D level design | Data / Assets |
| `Packages/` | Unity Package Manager manifest and package lock configuration | Configuration |
| `ProjectSettings/` | Unity project configuration (input, physics, tags, build settings) | Configuration |

## Running / Building

### Requirements
- Unity Hub
- Unity Editor version `2020.3.20f1` (or compatible Unity 2020.3 LTS release) with 2D modules installed

### Opening and Running in Editor
1. Open **Unity Hub**.
2. Click **Add** -> **Add project from disk** and select the repository root directory (`mario-clone`).
3. Select Unity Editor version `2020.3.20f1`.
4. Open the project in the editor.
5. In the Project window, navigate to `Assets/Scenes/` and open `Gameplay.unity` (the active entry scene specified in `ProjectSettings/EditorBuildSettings.asset`).
6. Press the **Play** button in the Unity Editor toolbar.

> *Note: Runtime execution and editor build were not executed or verified in this environment, as Unity Editor runtime tools are not available in this CLI environment.*

### Controls
- **Walk Left / Right**: `A` / `D` or Left / Right Arrow keys
- **Jump**: `Space`
- **Shoot Fire Bullet**: `J`

## Repository Notes

- **Tests**: None included in the repository (the Unity Test Framework package is registered in `Packages/manifest.json`, but there are no automated test scripts under `Assets/`).
- **CI/CD**: No continuous integration or automated build workflows are configured.
- **Commit History**: A minimal two-commit repository history created in September 2022 consisting of the initial repository setup and the project asset import.
- **Project Purpose**: This repository represents an educational practice prototype created to explore 2D platformer physics, tilemaps, and character controller scripting in Unity.

## Third-Party Assets

| Folder Path | Description | Licence File Present |
| --- | --- | --- |
| `Assets/Sprites/` | 2D platformer character sprites, tilesets, environment objects, and UI textures | No licence file present in repository |
| `Assets/Sounds/` | Sound effects (.mp3 and .ogg) for jump, coin collection, game over, and level clear | No licence file present in repository |
| `Assets/Animations/` | Pre-configured 2D animation clips and controllers for characters and blocks | No licence file present in repository |
