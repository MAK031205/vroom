# Understeer
### A physics-based arcade racing prototype

Ten cars. One track. A drift system that mostly works.

---

## About

Understeer is a Unity racing prototype built as a portfolio project. The focus is on tunable vehicle physics, every parameter that affects how the car handles is exposed and adjustable in real time while you drive.

The name is accurate. We're working on it.

---

## How to Play

### Starting a Race
1. Select a car from the car selection screen
2. Press START
3. Drive through the first checkpoint to begin the race
4. Complete all laps as fast as possible

### Controls

| Key | Action |
|-----|--------|
| W | Drive Forward |
| S | Drive Backward |
| A | Steer Left |
| D | Steer Right |
| R | Respawn at last checkpoint |
| P | Open / Close tuning panel |

---

## Tuning Panel

Press **P** mid-race to open the physics tuning panel. All changes apply in real time.

| Parameter | What it does |
|-----------|-------------|
| torque | Base acceleration force |
| maxTorque | Upper torque limit |
| steerAngle | Maximum steering lock |
| downPull | Downforce at speed |
| rearDriftStiffness | Rear grip during drift |
| normalRearStiffness | Rear grip during normal driving |
| steerAssist | Auto-correction during cornering |
| driftSteerAssist | Steering assist during drift |
| driftSlipThreshold | How much slip triggers drift mode |
| yawDamping | Rotation stability (normal) |
| driftYawDamping | Rotation stability (drift) |

Four presets (A/B/C/D) are available for quick handling profiles.

---

## Features

- 10 drivable vehicles with distinct physics profiles
- Real-time physics tuning panel with full parameter access
- Drift system with slip angle detection and yaw stability
- Checkpoint-validated lap system with best lap tracking
- Surface detection — grass slows you down
- Respawn system — press R to return to last checkpoint

---

## Known Issues

- Understeer (yes, really)
- WebGL build has reduced shadow quality compared to Windows build
- Camera can occasionally be obstructed by trackside trees
- No AI opponents in current build — time trial only

---

## Build Info

- Engine: Unity 6.3 LTS
- Render Pipeline: URP
- Input System: Unity Input System Package
- Platform: Windows / WebGL

---

## Play / Download

Available on [itch.io](https://mohdayaankhan.itch.io/understeer) — Windows build recommended for best visual quality.

---

## Credits

Built by Mohd Ayaan Khan — BTech Information Technology, Semester 4.

*Portfolio prototype. Not a finished game.*
