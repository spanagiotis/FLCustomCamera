FLCustomCamera
Code for the Freelancer style camera control in Unity3D

To get the script to work properly follow the instructions below.

Setting Up the Object Hierarchy:

1) Create a dummy/root object. This will hold the main camera and your spaceship model.

2) Create a main camera (or use the default one with Unity3D) and parent it to the dummy/root game object.

3) Create a spaceship game object to hold your 3d model objects that make up your ship and parent it to the dummy/root game object.

4) Attach the FLStyleCameraController.cs script to the dummy/root game object and drag the spaceship gameobject into the "spaceship" box.

5) Adjust your camera transform and/or spaceship game object transform to position them in a default spot.

6) Run!

FLStyleCameraController Variable Descriptions

SpaceShip - This is a reference to the spaceship game object.

Maximum Yaw Angle - Maximum angle which the ship will 'yaw'. This counts for both its left and right movement.

Maximum Roll Angle - Maximum angle which the ship will 'roll'. This counts for both its left and right movement.

Maximum Pitch Angle - Maximum angle which the ship will 'pitch'. This counts for bot its up and down movement.

Rotation Acceleration - Changing this value affects how fast the dummy/root object reaches its maximum rotation speed.

Sway Smooth Time - How fast the sway is smoothed.

Vertical Sway Extent - How far the camera will move off to the side of the ship, X coordinate.

Horizontal Sway Extent - How far the camera will move off to the side of the ship, Y coordinate.

Vertical Offset - Positions the camera at the default position +/- this offset.

Minimum Medial Distance - This is the distance that the ship will move up the screen.

Maximum Medial Distance - This is the distance that the ship will move down the screen.

Spin Acceleration - How fast the dummy/root reaches its maximum spin speed.

Maximum Spin Speed - How fast the dummy/root spins around the Z axis.

IsEnabled - This will enable/disable the camera rotation and sways. Good for checking the yaw/roll/pitch of the ship.