using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    private int totalPlayerPoints = 0;

    public int PotalPlayerPoints { get { return totalPlayerPoints; } }

    public void AddPoints(int points)
    {
        totalPlayerPoints += points;

        if (totalPlayerPoints < 0) { totalPlayerPoints = 0; }
    }
}
