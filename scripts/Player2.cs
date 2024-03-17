using Godot;

public partial class Player2 : CharacterBody2D
{
	// Movement speed
	public float Speed = 300; // Initial speed, adjust as needed
	public float MaxSpeed = 300; // Maximum speed, adjust as needed
	public float Acceleration = 50; // Acceleration rate, adjust as needed

	// Target ball
	private Ball _ball;

	// Offset variables
	private float _offsetY;

	// Timer for changing offset
	private Timer _offsetTimer;
	public override void _Ready()
	{
		// Find the ball in the scene
		_ball = GetNode<Ball>("../Ball");

		// Initialize the offset
		_offsetY = (float)GD.RandRange(-50, 50);

		// Create and connect the offset timer
		_offsetTimer = new Timer();
		AddChild(_offsetTimer);
		_offsetTimer.WaitTime = 2.0f; // Wait for 2 seconds
		_offsetTimer.OneShot = false; // Repeat timer
		_offsetTimer.Connect("timeout", new Callable(this, "UpdateOffset"));
		_offsetTimer.Start();
	}
	// Method to update the offset
	private void UpdateOffset()
	{
		// Determine the sign of the Y component of the direction
		float directionSign = Mathf.Sign(_ball.GlobalPosition.Y - GlobalPosition.Y);

		// Calculate the offset range based on the direction
		float offsetYMin = directionSign * 0;
		float offsetYMax = directionSign * 50;

		// Update the offset within the range
		_offsetY = -(float)GD.RandRange(offsetYMin, offsetYMax);

		// Restart the timer
		_offsetTimer.Start();
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Get the direction towards the ball
		Vector2 direction = (_ball.GlobalPosition - GlobalPosition);


		direction.Y += _offsetY;

		// Zero out the horizontal movement
		direction.X = 0;

		// Normalize the direction vector
		direction = direction.Normalized();

		// Calculate movement vector based on direction and speed
		Vector2 movement = direction * Speed * (float)delta;

		var collisionInfo = MoveAndCollide(movement);
		if (collisionInfo != null) this.Position += movement;
		GD.Print(_offsetY);
	}


}
