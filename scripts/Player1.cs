using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player1 : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Ensure that the Area2D node is added as a child of the character
		Area2D area = GetNode<Area2D>("Area2D");
		// Connect the area_entered signal of the Area2D node to the _on_Area2D_area_entered method
		area.Connect("area_entered", new Callable(this, nameof(_on_Area2D_area_entered)));
	}


	List<Key> pressedKeys = new List<Key>();
	bool collidedWithWall = false;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		movement(collidedWithWall);
	}

	// This method will be called when the area_entered signal is emitted
	private void _on_Area2D_area_entered(Area2D area)
	{
		if (area.IsInGroup("wall"))
		{
			collidedWithWall = true;
		}
		else
		{
			collidedWithWall = false;
		}
	}


	public void movement(bool stopAtWall)
	{
		if (!stopAtWall)
		{
			//Godot.Sprite2D child = this.GetNode<Godot.Sprite2D>("ChildNode");
			float amount = -4;

			// Calculate movement vector based on pressed keys
			Vector2 movement = new Vector2(0, 0);

			// Check for pressed keys and store them in the array if they are not already present
			if (Input.IsKeyPressed(Key.Z) && !pressedKeys.Contains(Key.Z))
			{
				pressedKeys.Add(Key.Z);
			}
			else if (Input.IsKeyPressed(Key.S) && !pressedKeys.Contains(Key.S))
			{
				pressedKeys.Add(Key.S);
			}

			// Remove keys from the list if they are not pressed in the current frame
			for (int i = pressedKeys.Count - 1; i >= 0; i--)
			{
				if (!Input.IsKeyPressed(pressedKeys[i]))
				{
					pressedKeys.RemoveAt(i);
				}
			}

			//GD.Print("Pressed keys: " + "[" + string.Join(",", pressedKeys.Select(k => k.ToString())) + "]");

			if (pressedKeys.Count > 0)
			{
				Key lastKey = pressedKeys[pressedKeys.Count - 1];
				if (lastKey == Key.S)
				{
					movement.Y -= amount; // Move upwards
				}
				else if (lastKey == Key.Z)
				{
					movement.Y += amount; // Move downwards
				}
			}
			this.Position += movement;
		}

	}
}
