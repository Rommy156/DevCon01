# 1. **Introduction**
 **3D stealth-action** game where you play a ninja infiltrating heavily guarded environment to find a treasure. Success depends on stealth and strategy rather than brute force. The player must blend into shadows, avoid detection, and silently eliminate targets to achieve objectives.

## 1.1 Desired Game Mechanic or Feature
### _The prototype focuses on stealth visibility and noise detection systems in a 3D ninja stealth game._
* **Movement:** walk and run 
* **Cover:** Objects blocks enemy line of sight
* **Sound:** Enemy can hear footsteps
* **Camera:** Player can control the camera
> Possible additions may include shadow mechanic, power ups and more levels

# 2. **Objective statement**
How the use of sound, light, and an intelligent AI system is used make a simple, immersive and casual game. Unlike many stealth games that rely heavily on large and multiple UI indicators.

# 3. **Gameplay**
## 3.1 Movement
* Forward movement
* Silent vs Noisy movement
## 3.2 Stealth
* Enemy vision cones
* Sound radius for footstep, running and object interaction
## 3.3 Core Loop:
* (observe → plan → act → adapt)
## 3.4 AI Behaviors
* Semi-random routes
* Investigate noise
* Alert allies, Increased awareness
* Combat player if player is seen
## 3.5 Win/Fail conditions
* Win: If player reaches exit undetected
* Fail: If player is captured or overwhelmed
## 3.6 Combat (Optional, Risky)
* Silent takedowns (from behind).
* Throwing shuriken (creates noise/distraction or eliminates target).
* Smoke bombs to escape.
## 3.7 Progression
* Missions increase in complexity:
* Level 1: Infiltrate small courtyard.
* Level 2: Navigate multiple patrols indoors.
* Level 3: Assassinate a guarded target.

# 4. **Design rationale**
* Make players feel clever by outsmarting enemy while feeling tense
* Use of sound to distract and lure enemies
* Minimal feedback system (simple visibility indicator and noise meter)

# 5. **Front End**
* Main Menu: Start Game, Continue, Options, Exit.
* Noise indicator.

# 6. **Audio Design**
* Enemy chatter & alert states.
* Footstep variation depending on surface (wood, grass, stone).
* Tense music that escalates when spotted.

# 7. **References**
* Mixamo 3d character and animation library
* YouTube tutorial [iIHeartGameDev, Sebastian Lauge ]
* Unity Asset Store: RPG Poly Pack https://assetstore.unity.com/packages/3d/environments/landscapes/rpg-poly-pack-lite-148410
* Bensound: Crime Scene by FoePound - License code: 2CE2FHTCJAKTEYJC, Evolution by Benjamin Tissot - License code: QX5RDLDFMZSXWKFV
* Pixabay: RUNNING ON GRASS Sound Effect by freesound_communnity

# 8. **Team**
* Allen Adepoju: Producer, Designer, Programmer
* Haig, Trinity: Sound designer
* Chouhan, Karndeep
