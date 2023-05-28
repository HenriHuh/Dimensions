using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public EnvironmentController environmentController;
    public PlayerController playerController;
    public LayerMask growthLayer;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        environmentController.Init();
    }

    public void ToggleLimbo(Tools.BooleanType val = Tools.BooleanType.Toggle)
    {
        environmentController.ToggleLimbo(val);
    }


}
