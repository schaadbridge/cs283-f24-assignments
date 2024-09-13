using System;
using System.Drawing;
using System.Windows.Forms;

public class Player
{
    // x position of player, always starts at center
    public float _xPosition = 0.5f * Window.width;
    public float _width = 0.1f * Window.width;
    public float _height = 0.07f * Window.width;
    public int _direction = 0;
    Image box = Image.FromFile("images/brown_box.png"); // load once during initialization
    public void MoveLeft() 
    {
        _direction = -250;
    }
    public void MoveRight() 
    {
        _direction = 250; 
    }
	public void Update(float dt)
    {
        _xPosition += _direction * dt;
        if (_xPosition < 0.5f * _width) 
        {
            _xPosition = 0.5f * _width;
        } else if(_xPosition > Window.width - 0.5f * _width)
        {
            _xPosition = Window.width - 0.5f * _width;
        }
    }
    

    public void Draw(Graphics g)
    {
        // centered on xPosition
        g.DrawImage(box, _xPosition - 0.5f * _width, 0.85f * Window.height, _width, _height);
        _direction = 0;
    }
}