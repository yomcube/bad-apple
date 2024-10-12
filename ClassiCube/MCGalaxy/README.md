# ClassiCube + MCGalaxy

## Prerequisites
- PNG sequence of Bad Apple **starting at frame 1**
  - `f_0001.png`, `f_0002.png`, `f_0003.png`, `f_0004.png`, etc.
- [MCGalaxy](https://www.classicube.net/mcg/download/)
- [ClassiCube](https://www.classicube.net/download/)

## Setup
 1. Open MCGalaxy.
 2. Copy [CmdBadApple.cs](ClassiCube/MCGalaxy/CmdBadApple.cs) to `extra/commands/source/`.
 3. Run the following commands in the MCGalaxy console:
    ```
    /resizelvl main 60 45 1 confirm
    /compile BadApple
    /cmdload BadApple
    ```
 4. Open ClassiCube. If prompted, choose enhanced mode.
 5. Click the Direct Connect button.
 6. Enter your username in the username box. In the IP box, enter `localhost:25565`.
    - 25565 is MCGalaxy's default port, so if you change
      it, adjust the port in the launcher accordingly.
 7. Connect to the server.
 8. Press X to enable the NoClip hack. This will enable you to fly out of the world boundaries, letting you see the entire world.
 9. Press T to open chat. Run `/badapple`. The world will now display Bad Apple.
    - To stop, run `/badapple stop`.
