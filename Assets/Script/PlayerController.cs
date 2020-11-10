using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Player;
    public float speed;

    public Vector2 movement;
    public GameObject sword;
    public Animator swordanim;
    private bool stabbing = false;
    public GameObject swordhitbox;

    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        Player = this.GetComponent<Rigidbody2D>();
        this.transform.parent = GameObject.FindGameObjectWithTag("Grid").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Player.velocity = movement;
        target = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, this.transform.position.z));
        float AngleRad = Mathf.Atan2(target.y - this.transform.position.y, target.x - this.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

    }

    private void OnMovement(InputValue input)
    {
        movement = input.Get<Vector2>() * speed;
    }

    void OnInteract()
    {
        Debug.Log("Interacted!");
        if (!stabbing)
            StartCoroutine(Stabby());
    }

    public IEnumerator Stabby()
    {
        stabbing = true;
        swordanim.SetBool("Isswinging", true);
        swordhitbox.SetActive(true);
        sword.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        sword.SetActive(false);
        swordanim.SetBool("Isswinging", false);
        swordhitbox.SetActive(false);
        stabbing = false;
    }

    void OnSpecial()
    {
        Debug.DrawLine(this.transform.position, target);
    }
    
}
