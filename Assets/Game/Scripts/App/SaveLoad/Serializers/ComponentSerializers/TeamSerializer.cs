using SampleGame.App.Data;
using SampleGame.Common;
using SampleGame.Gameplay;

namespace SampleGame.App.SaveLoad.Serializers
{
    public class TeamSerializer : ComponentSerializer<Team, TeamData>
    {
        protected override TeamData Serialize(Team service)
        {
            return new TeamData { TeamType = (int)service.Type };
        }

        protected override void Deserialize(Team service, TeamData data)
        {
            service.Type = (TeamType)data.TeamType;
        }
    }
}