using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player1 : CharacterBody2D
{
	List<Key> pressedKeys = new List<Key>();
	bool collidedWithTopWall = false;
	bool collidedWithBottenWall = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Ensure that the Area2D node is added as a child of the character
		Area2D area = GetNode<Area2D>("Area2D");
		// Connect the area_entered signal of the Area2D node to the _on_Area2D_area_entered method
		area.Connect("area_entered", new Callable(this, nameof(_on_Area2D_area_entered)));
		area.Connect("area_exited", new Callable(this, nameof(_on_Area2D_area_exited)));
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		movement(collidedWithTopWall, collidedWithBottenWall);
	}

	// This method will be called when the area_entered signal is emitted
	private void _on_Area2D_area_entered(Area2D area)
	{
		if (area.IsInGroup("TopWall")) collidedWithTopWall = true;

		if (area.IsInGroup("BottomWall")) collidedWithBottenWall = true;

	}
	private void _on_Area2D_area_exited(Area2D area)
	{
		if (area.IsInGroup("TopWall")) collidedWithTopWall = false;

		if (area.IsInGroup("BottomWall")) collidedWithBottenWall = false;
	}

	public void movement(bool stopAtTopWall, bool stopAtBottomWall)
	{
		float amount = -4;

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

			if (lastKey == Key.S && !stopAtBottomWall) movement.Y -= amount;
			else if (lastKey == Key.Z && !stopAtTopWall) movement.Y += amount;
		}

		this.Position += movement;
	}
}
