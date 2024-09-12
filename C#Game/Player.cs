using System;
using System.Drawing;
using System.Windows.Forms;

public class Player
{
    // x position of player, always starts at center
    public float _xPosition = 0.5f * Window.width;
    public float _width = 0.07f * Window.width;
    public float _height = 0.05f * Window.width;
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
        if (_xPosition < 0) 
        {
            _xPosition = 0;
        } else if(_xPosition > Window.width - _width)
        {
            _xPosition = Window.width - _width;
        }
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(box, _xPosition, 0.85f * Window.height, _width, _height);
        _direction = 0;
    }
}