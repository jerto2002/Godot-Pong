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

    private Vector2 _velocity = new Vector2(300, 0);

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
            CheckForGoal(collidedNode);

            OffPlayerBounceLogic(collidedNode);
        }
    }

    private void OffPlayerBounceLogic(Node collidedNode)
    {
        if (collidedNode is CharacterBody2D characterBody)//Bounce of player logic
        {
            // Get the position of the ball
            Vector2 ballPosition = Position;

            // Subtract the y positions of the ball and the StaticBody2D
            float yDifference = characterBody.Position.Y - ballPosition.Y;

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

            // Increase the velocity by a factor (e.g., 1.1 for a 10% increase)
            float velocityIncreaseFactor = 1.1f; // Adjust as needed
            _velocity *= velocityIncreaseFactor;

            // Set the Y-component of the ball's velocity based on the angle
            _velocity.Y = Mathf.Sin(angle) * _velocity.Length();

            // Adjust the X-component of the ball's velocity to maintain constant speed
            _velocity.X = Mathf.Cos(angle) * _velocity.Length() * Mathf.Sign(_velocity.X); // Maintain the original direction
        }
    }
    private void CheckForGoal(Node collidedNode)
    {
        if (collidedNode is StaticBody2D staticBody2D)
        {
            var collidedGroups = staticBody2D.GetGroups();

            if (collidedGroups.Contains("Goal1"))
            {
                GD.Print("Goal1");
                Label label = GetNode<Label>("../ScorePlayer2");
                // Parse the current text and increment by 1
                int score = int.Parse(label.Text) + 1;
                // Update the text of the label with the incremented value
                label.Text = score.ToString();

                // Reset ball position
                ResetBallPosition();
            }
            else if (collidedGroups.Contains("Goal2"))
            {
                GD.Print("Goal2");
                Label label = GetNode<Label>("../ScorePlayer1");
                // Parse the current text and increment by 1
                int score = int.Parse(label.Text) + 1;
                // Update the text of the label with the incremented value
                label.Text = score.ToString();

                // Reset ball position
                ResetBallPosition();
            }
        }
    }

    private void ResetBallPosition()
    {
        // Set the ball position to a predefined starting position
        Position = new Vector2(0, 0); // Adjust the position as needed
    }


}
