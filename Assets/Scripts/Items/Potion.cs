using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Potion : Item {

    public string stat;
    public float multiplier;

    [SyncVar]
    public float timer;

    private bool drank = false;


    [Server]
    public override void UseItem(Vector3 position, Vector3 direction)
    {
        base.UseItem(position, direction);
        drinkPotion();
    }

    [ServerCallback]
    private void Update()
    {
        if (drank)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                undrinkPotion();
                drank = false;
            }
        }
    }

    private void drinkPotion()
    {
        //play animation
        //increase by <multiplayer> for <stat> of <user> for <timer> seconds

        drank = true;
        this.user.GetComponent<PlayerMovement>().changeStat(stat, multiplier);
    }

    private void undrinkPotion()
    {
        //play animation
        //increase by <multiplayer> for <stat> of <user> for <timer> seconds

        this.user.GetComponent<PlayerMovement>().changeStat(stat, 1/multiplier);
    }
}
