using System;
using System.Drawing;
using System.Windows.Forms;

public class Ball 
{
    // y position of player, always starts at top
    public float _xPosition = 0.5f * Window.width;
    public float _yPosition = 0;
    public float _size = 0.05f * Window.width;
    public bool _show = false;
    public bool _done = false;
    Image candy = Image.FromFile("images/single_candy.png"); // load once during initialization
    public Ball() {
        
    }

    public Ball (Random rnd) {
        _xPosition = (float) rnd.NextDouble() * (float)(Window.width - 0.5f * _size);
    }

    public void HideBall()
    {
        _show = false;
        _done = true;
    }

	public void Update(float dt)
    {
        if (_show)
        {
            _yPosition += (float) 200 * dt;
        }
    }

    public void Draw(Graphics g)
    {
        if (_show) {
            // centered on xPosition
            g.DrawImage(candy, _xPosition - 0.5f * _size, _yPosition, _size, _size);
        }
    }
}