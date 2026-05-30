# Understeer

A gameplay-focused Unity racing prototype built around vehicle handling, tuning systems, and replayability.

Understeer was developed as an exploration of how vehicle setup, surface interaction, and player choice influence driving behaviour. Instead of relying on a single optimal strategy, the project encourages experimentation through multiple vehicle types and real-time tuning options.

---

## Play the Game

🎮 WebGL Build:
https://mohdayaankhan.itch.io/understeer

📥 Windows Build:
https://mohdayaankhan.itch.io/understeer

📹 Gameplay Showcase:
[https://youtu.be/aR0I97N1b1M](url)

---

## Screenshots

<img width="1280" height="800" alt="Screenshot 2026-05-24 171359" src="https://github.com/user-attachments/assets/14e3cb50-8de0-40b1-b85a-5bf1c24cb08b" />
<img width="1280" height="800" alt="Screenshot 2026-05-24 171334" src="https://github.com/user-attachments/assets/1bc7aef9-fe0e-4fc1-bec8-27127b7af0a0" />
<img width="632" height="356" alt="Screenshot 2026-05-24 162015" src="https://github.com/user-attachments/assets/b44538a7-eb04-4e82-b334-f5bf1c4e77b1" />
<img width="1280" height="800" alt="Screenshot 2026-05-25 171550" src="https://github.com/user-attachments/assets/cdda51d3-7211-47d0-8cb9-666411dc20d4" />


---

## Core Features

- 8 drivable vehicles with distinct handling profiles
- Real-time vehicle tuning system
- 4 preset tuning configurations
- Runtime adjustment of 10+ handling parameters
- Checkpoint-validated lap system
- Best lap time tracking
- Surface-aware vehicle behaviour
- Vehicle selection menu with 3D previews
- Respawn system
- Responsive gameplay UI
- WebGL and Windows deployment

---

## Technical Systems

### Vehicle Controller

The driving system is built using Unity's PhysX framework and focuses on creating noticeably different handling characteristics across multiple vehicles.

Vehicle behaviour can be adjusted through parameters such as:

- Acceleration
- Braking Force
- Steering Sensitivity
- Suspension Settings
- Tire Grip
- Downforce

This allows players to experiment with different setups and discover driving styles that suit them.

---

### Real-Time Tuning System

A runtime tuning panel allows players to modify vehicle parameters while the game is running.

The goal was to create a system where gameplay feel could be tested and iterated on without rebuilding the project or modifying code.

Players can:

- Select predefined presets
- Modify tuning values directly
- Compare handling behaviour in real time

---

### Lap Validation System

Instead of counting laps through a single finish-line trigger, the system validates that required checkpoints have been crossed before a lap is recorded.

This prevents shortcut exploitation and ensures lap completion remains reliable.

The system tracks:

- Checkpoint progression
- Valid lap completion
- Current lap time
- Best lap time

---

### Surface Detection

Vehicle behaviour changes depending on the surface being driven on.

The system detects the current driving surface and adjusts handling-related values accordingly, creating different driving experiences across track sections.

Examples include:

- Reduced grip surfaces
- Higher traction areas
- Different vehicle responses based on terrain

---

### Respawn System

Players can reset their vehicle if they become stuck or leave the intended play area.

The respawn system restores the vehicle to a valid track position while preserving gameplay flow.

---

### UI Systems

The project includes multiple UI-driven gameplay systems:

- Main Menu
- Vehicle Selection Screen
- Runtime Tuning Interface
- Lap Timing Display
- Best Lap Tracking
- Pause Menu

UI elements update dynamically based on gameplay state and player actions.

---

## Development Goals

The primary goals behind Understeer were:

- Building gameplay systems from scratch
- Exploring vehicle handling and tuning mechanics
- Improving Unity architecture and scripting skills
- Creating replayability through player choice
- Learning how gameplay systems interact at a project level

---

## Challenges Solved

### Reliable Lap Tracking

Ensuring laps could not be completed through shortcuts required checkpoint validation logic rather than simple finish-line detection.

### Runtime Tuning

Allowing players to adjust gameplay parameters during runtime required careful synchronization between UI systems and vehicle controllers.

### Surface-Based Handling

Creating noticeable differences between surfaces while maintaining a predictable driving experience required balancing multiple handling variables.

---

## Tech Stack

- Unity 6.3 LTS
- C#
- Unity PhysX
- URP
- Git
- GitHub

---

## Future Improvements

Potential future additions include:

- AI Opponents
- Multiplayer Support
- Additional Tracks
- Ghost Replay System
- Expanded Vehicle Progression
- Advanced Telemetry and Analytics

---

## Developer

Mohd Ayaan Khan

GitHub:
https://github.com/MAK031205

itch.io:
https://mohdayaankhan.itch.io/understeer

ArtStation:
https://mak031205.artstation.com
