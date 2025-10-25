
# Balloon Platformer

A vertical 2D endless platformer made with Unity. Control a balloon, hop between rising platforms, avoid obstacles, and score points based on height.

## Controls

* Move: `A` / `D` or `Left` / `Right`
* Jump: `Space`
* (Optional) Pause/Quit: `Esc`

## Features

* Object pooling for platforms and obstacles (smooth performance)
* Upward-following camera with smooth interpolation
* Score by maximum height; resets on respawn
* Obstacles avoid platforms using a LayerMask
* Moving platforms (left–right) with optional “sticky” behavior
* Kill Zone below the screen triggers clean respawn
* Optional power-ups (e.g., Shield)

## Unity Version / Assets

* Tested on Unity 2021.3+ (should work on newer versions).
* Uses free art assets (e.g., Bayat Games – Free Platform Game Assets).

  * Respect the license terms of the asset packs you import.

## Project Structure

```
Assets/
  Prefabs/
    Balloon.prefab
    Platform.prefab
    Obstacle.prefab
    (Optional) ShieldPickup.prefab
  Scripts/
    BalloonController.cs
    BalloonGameManager.cs
    CameraFollow.cs
    PlatformPool.cs
    ObstaclePool.cs
    RecycleWhenBelow.cs
    KillZone.cs
    ScoreByHeight.cs
    MovingPlatform.cs
    StickyPlatform.cs
    (Optional) Shield.cs, ShieldPickup.cs
  Art/Audio/... (as needed)
```

## How to Run

1. Open the project in Unity.
2. Load the main scene (e.g., `Main.unity`).
3. Inspector wiring:

   * `GameManager` (with `BalloonGameManager`):

     * **Balloon Prefab** → `Balloon.prefab`
     * **Camera Follow** → `Main Camera` (with `CameraFollow`)
     * **Platforms / Obstacles** → the objects holding `PlatformPool` / `ObstaclePool`
     * **Score** → `ScoreByHeight` (on the Canvas)
   * `PlatformPool`: set `platformPrefab`, `poolSize` ≈ 12, `levelHalfWidth` ≈ 3–4, `minGapY` ≈ 1.2, `maxGapY` ≈ 2.2
   * `ObstaclePool`: set `obstaclePrefab`, `poolSize` ≈ 4–6, `levelHalfWidth` ≈ 3–4, `minGapY` ≈ 3, `maxGapY` ≈ 6,
     `spawnChance` ≈ 0.35, `platformLayer` = **Ground**, `avoidRadius` ≈ 0.6–1
   * `Main Camera` has Tag = **MainCamera**
   * `KillZone` (child of camera): `BoxCollider2D` with **Is Trigger = ON**, positioned slightly below the bottom of the view
4. Press **Play**.

## Tags and Layers

* **Balloon**: `Rigidbody2D (Dynamic)` + `CircleCollider2D`, Tag: (optional) `Player`
* **Platform.prefab**: `BoxCollider2D` (Is Trigger = OFF), Tag: `Platform`, Layer: `Ground`
* **Obstacle.prefab**: `Collider2D` (Polygon/Box) (Is Trigger = OFF), Tag: `Obstacle`
* **ShieldPickup.prefab** (optional): `CircleCollider2D` (Is Trigger = ON)

## Script Overview

* `BalloonController` – movement, jumping, death on obstacle
* `BalloonGameManager` – spawn/respawn balloon, attach camera/pools/score
* `CameraFollow` – vertical follow with `SnapTo` on respawn
* `PlatformPool` / `ObstaclePool` – spawn/recycle stream, X spread and Y gap
* `RecycleWhenBelow` – recycles objects after they pass below the camera frame
* `KillZone` – trigger below the screen that signals respawn
* `ScoreByHeight` – score by highest Y
* `MovingPlatform` – horizontal oscillation
* `StickyPlatform` – temporarily parents the player to moving platforms
* (Optional) `Shield`, `ShieldPickup` – one-hit shield power-up

## Tuning (Difficulty/Spacing)

* Increase vertical spacing: `PlatformPool → Min/Max Gap Y`
* Wider horizontal spread: `PlatformPool → Level Half Width`
* More/fewer obstacles: `ObstaclePool → spawnChance` or `poolSize`
* Prevent obstacle–platform overlap: set platforms to Layer `Ground`, assign `ObstaclePool.platformLayer = Ground`, adjust `avoidRadius`

## Build

1. **File → Build Settings…**
2. Add the main scene to “Scenes In Build”
3. Choose target platform (Windows/macOS/WebGL/Android/iOS) and click **Build**
4. For mobile, set portrait orientation and relevant player settings

## Troubleshooting

* Platforms “disappear”:

  * Ensure only one `RecycleWhenBelow` per object, added by the pool (not duplicated on the prefab)
  * `RecycleWhenBelow` should recycle when the object is below the camera’s bottom (not too early)
* Obstacles sit on platforms:

  * Set `platformLayer = Ground` in `ObstaclePool`, increase `avoidRadius`
* No respawn:

  * `BalloonController` must call `BalloonGameManager.HandleBalloonDeath()` on obstacle hit
  * Ensure `KillZone` exists and is set as a trigger below the view
* UI scaling:

  * On the Canvas, use `Canvas Scaler → Scale With Screen Size` (for portrait e.g., 1080×1920)

## License

* Code: MIT (or your preferred license)
* Art assets: according to their creators’ licenses (e.g., Bayat Games, Kenney, etc.)

## Credits

* Bayat Games – Free Platform Game Assets
* (Optional) Kenney assets for rings/glow used in Shield VFX

---

If you want, I can tailor this to your exact scene/prefab names and include screenshots or GIFs.
