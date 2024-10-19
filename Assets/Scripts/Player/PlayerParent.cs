using CosmicHorrorJam.Util;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    [SerializeField]
    public GameObject dummyTopScale;

    [SerializeField]
    public GameObject dummyBottomScale;

    [SerializeField]
    public bool reflection;

    [SerializeField]
    public float speedModifier = 1f;

    [SerializeField] public DirectionFaced startDirectionFaced = DirectionFaced.Right;
}
