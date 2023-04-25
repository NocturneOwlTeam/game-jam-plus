using Nocturne.Enums;
using UnityEngine;

public class TeamComponent : MonoBehaviour
{
    // Diferencia entre los enemigos, objetos y aliados (tambien los que no pueden ser dañados).
    [SerializeField]
    private TeamIndex teamIndex = TeamIndex.None;

    public TeamIndex currentTeamIndex
    {
        set
        {
            if (teamIndex == value)
            {
                return;
            }

            teamIndex = value;
        }
        get
        {
            return teamIndex;
        }
    }
}