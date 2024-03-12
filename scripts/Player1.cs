using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player1 : CharacterBody2D
{
	List<Key> pressedKeys = new List<Key>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area2D area = GetNode<Area2D>("Area2D");
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		movement();
	}

	public void movement()
	{
		float velocity = -4;

		// Calculate movement vector based on pressed keys
		Vector2 movement = new Vector2(0, 0);

		// Check for pressed keys and store them in the array if they are not already present
		if (Input.IsKeyPressed(Key.Z) && !pressedKeys.Contains(Key.Z)) pressedKeys.Add(Key.Z);
		else if (Input.IsKeyPressed(Key.S) && !pressedKeys.Contains(Key.S)) pressedKeys.Add(Key.S);

		// Remove keys from the list if they are not pressed in the current frame
		for (int i = pressedKeys.Count - 1; i >= 0; i--)
		{
			if (!Input.IsKeyPressed(pressedKeys[i])) pressedKeys.RemoveAt(i);
		}
		//GD.Print("Pressed keys: " + "[" + string.Join(",", pressedKeys.Select(k => k.ToString())) + "]");

		if (pressedKeys.Count > 0) //check if anny key is pressed
		{
			Key lastKey = pressedKeys[pressedKeys.Count - 1]; // check wich key is last pressed

			if (lastKey == Key.S ) movement.Y -= velocity;
			else if (lastKey == Key.Z ) movement.Y += velocity;
		}

		
		// Check for collisions and adjust movement if necessary
		var collisionInfo = MoveAndCollide(movement);
		if (collisionInfo != null) this.Position += movement; // var collisionInfo = MoveAndCollide(movement); the if is not nessery to stop movement its not needed only added out of readability
	}
}
