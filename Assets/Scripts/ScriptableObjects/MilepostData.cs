using UnityEngine;

[CreateAssetMenu(fileName = "MilepostData", menuName = "Digital Rails/MilepostData", order = 1)]
public sealed class MilepostData : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private int constructionCost;

    public Sprite Sprite
    {
        get => this.sprite;
        set => this.sprite = value;
    }

    public int ConstructionCost
    {
        get => this.constructionCost;
        set => this.constructionCost = value;
    }
}
