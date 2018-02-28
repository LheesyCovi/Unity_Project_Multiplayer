using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    public PlayerWeapon Equipped;
    public List<PlayerWeapon> weapon = new List<PlayerWeapon>();
    //public PlayerWeapon weapon;
    public Camera cam;
    public LayerMask mask;


    private void Start()
    {

        Equipped = weapon[0];

        weapon.Add(
                new PlayerWeapon()
           );

        if (cam == null)
        {
            Debug.LogError("No camera referenced!");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, Equipped.range, mask))
        {
            //Debug.Log("vi ramte " + _hit.collider.name);
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, Equipped.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string _id, int _damage)
    {
        Debug.Log(_id + " has been shot");

        Player _player = GameManager.GetPlayer(_id);
        _player.TakeDamage(Equipped.damage);
    }
}
