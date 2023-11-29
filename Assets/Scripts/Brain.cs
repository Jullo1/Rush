using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain
{
    Player _myPlayer;

    public Brain(Player myBrain)
    {
        _myPlayer = myBrain;
    }

    public void ListenerKeys()
    {
        _myPlayer.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftControl))
            _myPlayer.Punch();
        if (Input.GetKeyDown(KeyCode.Q))
            _myPlayer.Special();
        if (Input.GetKeyDown(KeyCode.E))
            _myPlayer.Blast();
    }
}
