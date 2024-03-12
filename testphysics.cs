using Godot;
using System;

public partial class testphysics : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

		private Vector2 _velocity = new Vector2(250, 250);

    public override void _PhysicsProcess(double delta)
    {
        var collisionInfo = MoveAndCollide(_velocity * (float)delta);
        if (collisionInfo != null)
            _velocity = _velocity.Bounce(collisionInfo.GetNormal());
    }
}
