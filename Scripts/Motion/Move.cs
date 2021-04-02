using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private float height;
    private float width;

    public MapDimensions md;
    public Joystick joystick;


    private void Awake()
    {
        height = md.height;
        width = md.width;
    }

    void FixedUpdate()
    {
        int speed = PlayerPrefs.GetInt("moveSpeed", 12);
        if (transform.position.x > width / -2 && transform.position.x < width / 2 && transform.position.y > height / -2 && transform.position.y < height / 2)
        {
            var move = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
            transform.position += move * speed * Time.deltaTime;
        }
        else
        {
            
            if (transform.position.x <= width / -2 && transform.position.y <= height / -2)
            {
                float joystickHorizontal = 0;
                if (joystick.Horizontal > 0)
                {
                    joystickHorizontal = joystick.Horizontal;
                }
                float joystickVertical = 0;
                if (joystick.Vertical > 0)
                {
                    joystickVertical = joystick.Vertical;
                }
                var move = new Vector3(joystickHorizontal, joystickVertical, 0);
                transform.position += move * speed * Time.deltaTime;
            }
            else if (transform.position.y <= height / -2&& transform.position.x >= width / 2)
            {
                float joystickVertical = 0;
                if (joystick.Vertical > 0)
                {
                    joystickVertical = joystick.Vertical;
                }
                float joystickHorizontal = 0;
                if (joystick.Horizontal < 0)
                {
                    joystickHorizontal = joystick.Horizontal;
                }
                var move = new Vector3(joystickHorizontal, joystickVertical, 0);
                transform.position += move * speed * Time.deltaTime;
            }
            else if (transform.position.y >= height / 2 && transform.position.x >= width / 2)
            {
                float joystickVertical = 0;
                if (joystick.Vertical < 0)
                {
                    joystickVertical = joystick.Vertical;
                }
                float joystickHorizontal = 0;
                if (joystick.Horizontal < 0)
                {
                    joystickHorizontal = joystick.Horizontal;
                }
                var move = new Vector3(joystickHorizontal, joystickVertical, 0);
                transform.position += move * speed * Time.deltaTime;
            }
            else if (transform.position.x <= width / -2 && transform.position.y >= height / 2)
            {
                float joystickHorizontal = 0;
                if (joystick.Horizontal > 0)
                {
                    joystickHorizontal = joystick.Horizontal;
                }
                float joystickVertical = 0;
                if (joystick.Vertical < 0)
                {
                    joystickVertical = joystick.Vertical;
                }
                var move = new Vector3(joystickHorizontal, joystickVertical, 0);
                transform.position += move * speed * Time.deltaTime;
            }


            else
            {
                if (transform.position.x <= width / -2)
                {
                    float joystickHorizontal = 0;
                    if (joystick.Horizontal > 0)
                    {
                        joystickHorizontal = joystick.Horizontal;
                    }
                    var move = new Vector3(joystickHorizontal, joystick.Vertical, 0);
                    transform.position += move * speed * Time.deltaTime;
                }
                if (transform.position.x >= width / 2)
                {
                    float joystickHorizontal = 0;
                    if (joystick.Horizontal < 0)
                    {
                        joystickHorizontal = joystick.Horizontal;
                    }
                    var move = new Vector3(joystickHorizontal, joystick.Vertical, 0);
                    transform.position += move * speed * Time.deltaTime;
                }
                if (transform.position.y >= height / 2)
                {
                    float joystickVertical = 0;
                    if (joystick.Vertical < 0)
                    {
                        joystickVertical = joystick.Vertical;
                    }
                    var move = new Vector3(joystick.Horizontal, joystickVertical, 0);
                    transform.position += move * speed * Time.deltaTime;
                }
                if (transform.position.y <= height / -2)
                {
                    float joystickVertical = 0;
                    if (joystick.Vertical > 0)
                    {
                        joystickVertical = joystick.Vertical;
                    }
                    var move = new Vector3(joystick.Horizontal, joystickVertical, 0);
                    transform.position += move * speed * Time.deltaTime;
                }
            }
        }

        /*
        if (transform.position.x > width / -2 && transform.position.x < width / 2 && transform.position.y > height / -2 && transform.position.y < height / 2)
        {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
        }
        else
        {
            if ((Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > width / -2)
            {
                var move = new Vector3(Input.GetAxis("a"), 0, 0);
                transform.position += move * speed * Time.deltaTime;
            }
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < width / 2)
            {
                var move = new Vector3(Input.GetAxis("d"), 0, 0);
                transform.position += move * speed * Time.deltaTime;
            }
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position.y < height / 2)
            {
                var move = new Vector3(0, Input.GetAxis("w"), 0);
                transform.position += move * speed * Time.deltaTime;
            }
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y > height / -2)
            {
                var move = new Vector3(0, Input.GetAxis("s"), 0);
                transform.position += move * speed * Time.deltaTime;
            }
        }*/

    }
}
