using Godot;
using System;

public partial class Player2 : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		movement();
	}

	public void movement()
	{
		Godot.Sprite2D child = this.GetNode<Godot.Sprite2D>("ChildNode");
		float amount = -4;
		if (Input.IsKeyPressed(Key.S))
		{
			this.Position += new Vector2(0, -amount);
		}
		if (Input.IsKeyPressed(Key.W))
		{
			this.Position += new Vector2(0, amount);
		}
	}
}
