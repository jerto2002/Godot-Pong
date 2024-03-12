using Godot;
using System;

public partial class Ball : StaticBody2D
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
        // Move the ball
        KinematicCollision2D collision = MoveAndCollide(_velocity * (float)delta);

        if (collision != null)
        {
            // Get the collider from the collision
            Object collider = collision.GetCollider();

            // Bounce off the paddle
            _velocity = _velocity.Bounce(collision.GetNormal());

            Node collidedNode = (Node)collision.GetCollider();

            if (collidedNode is CharacterBody2D staticBody)
            {
                // Get the position of the ball
                Vector2 ballPosition = Position;

                // Subtract the y positions of the ball and the StaticBody2D
                float yDifference = staticBody.Position.Y - ballPosition.Y;
				GD.Print(yDifference);
                // Calculate the angle based on the yDifference
                float angle;
                if (yDifference > 0)
                {
                    angle = Mathf.Lerp(0, -Mathf.Pi / 6, yDifference / 100.0f); // Adjust the 100.0f as needed
                }
                else
                {
                    angle = Mathf.Lerp(0, Mathf.Pi / 6, -yDifference / 100.0f); // Adjust the 100.0f as needed
                }

                // Set the Y-component of the ball's velocity based on the angle
                _velocity.Y = Mathf.Sin(angle) * _velocity.Length();

                // Adjust the X-component of the ball's velocity to maintain constant speed
                _velocity.X = Mathf.Cos(angle) * _velocity.Length() * Mathf.Sign(_velocity.X); // Maintain the original direction
            }
        }

        // Normalize the velocity to ensure consistent speed
        _velocity = _velocity.Normalized() * 250; // Adjust 250 as needed for the desired speed
    }
}
