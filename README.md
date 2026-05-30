# Understeer

A gameplay-focused Unity racing prototype built around vehicle handling, tuning systems, and replayability.

Understeer was developed as an exploration of how vehicle setup, surface interaction, and player choice influence driving behaviour. Instead of relying on a single optimal strategy, the project encourages experimentation through multiple vehicle types and real-time tuning options.

## Project Status

**Current Status:** Playable Prototype

The project is currently focused on gameplay systems, vehicle handling, and player experimentation. Future iterations may expand AI systems, progression mechanics, additional tracks, and multiplayer functionality.

---

## Play the Game

<img width="1023" height="640" alt="Untitleddesign-ezgif com-optimize" src="https://github.com/user-attachments/assets/db214ac6-760d-4817-8415-7d9dc0f39be7" />

🎮 WebGL Build:
https://mohdayaankhan.itch.io/understeer

📥 Windows Build:
https://mohdayaankhan.itch.io/understeer

📹 **Gameplay Showcase**
https://youtu.be/aR0I97N1b1M

---
## My Contribution

This project was developed independently, including:

- Gameplay programming
- Vehicle handling systems
- Runtime tuning systems
- UI implementation
- Lap validation logic
- Surface detection systems
- Vehicle selection workflow
- Testing, balancing, and deployment

---

## Screenshots

<img width="1280" height="800" alt="Screenshot 2026-05-24 171359" src="https://github.com/user-attachments/assets/14e3cb50-8de0-40b1-b85a-5bf1c24cb08b" />
<img width="1023" height="640" alt="Untitleddesign4-ezgif com-optimize" src="https://github.com/user-attachments/assets/3dcb8542-99de-43bd-8adb-f04d474f830b" />
<img width="1024" height="640" alt="Untitled design (5)" src="https://github.com/user-attachments/assets/8bd298fa-9bd7-445a-a640-7270ea4a0cbe" />

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
## Key Takeaways

Through this project I gained experience with:

- Gameplay systems architecture
- Physics-based vehicle handling
- Runtime parameter tuning
- UI-to-gameplay communication
- Checkpoint validation logic
- Iterative gameplay balancing
- WebGL deployment workflows

---

## Tech Stack

- Unity 6.3 LTS
- C#
- Unity PhysX Vehicle Systems
- Universal Render Pipeline (URP)
- TextMeshPro
- Git
- GitHub

---

## Future Improvements

Planned future improvements:

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



