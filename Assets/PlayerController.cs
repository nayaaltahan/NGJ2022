using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private float speed;
    [SerializeField] private float screenXPadding, screenYPadding = 100f;
    

    private float maxX, maxY, minX, minY;
    // Start is called before the first frame update
    void Start()
    {
        minX = Camera.main.ScreenToWorldPoint(new Vector3(screenXPadding, 0, 1)).x;
        maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - screenXPadding, 0, 1)).x;
        minY = Camera.main.ScreenToWorldPoint(new Vector3(screenYPadding, 0, 1)).y;
        maxY = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height - screenYPadding, 0, 1)).y;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        MoveSpaceship(direction * speed * Time.deltaTime);
    }


    public void MoveSpaceship(Vector3 direction)
    {
        // Move spaceship and limit position to screen max and min
        spaceShip.transform.position = new Vector3(
            Mathf.Clamp(spaceShip.transform.position.x + direction.x, minX, maxX),
            Mathf.Clamp(spaceShip.transform.position.y + direction.y, minY, maxY),
            0);
        
    }

    private void OnGUI()
    {
        // Set UI style and font size
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;
        
        // Set IMGUI style to style object
        GUI.skin.label = style;


        // Show a textbox with transform position
        GUI.Label(new Rect(10, 10, 300, 20), $"Position: {spaceShip.transform.position}");
        
        //show a textbox with minX, maxX, minY, maxY
        GUI.Label(new Rect(10, 30, 300, 20), $"minX: {minX}, maxX: {maxX}, minY: {minY}, maxY: {maxY}");
        
        // Show a textbox with screen width and height
        GUI.Label(new Rect(10, 50, 300, 20), $"Screen width: {Screen.width}, height: {Screen.height}");
        
        // Show a textbox with mouse position in world space
        GUI.Label(new Rect(10, 70, 300, 20), $"Mouse position: {Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
        
    }
}