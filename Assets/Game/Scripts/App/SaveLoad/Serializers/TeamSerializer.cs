using Game.Scripts.App.Data;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class TeamSerializer : GameSerializer<Team, TeamData>
    {
        public TeamSerializer(Team service) : base(service, nameof(TeamData)) { }

        protected override TeamData Serialize(Team service)
        {
            return new TeamData { TeamType = (int)service.Type };
        }

        protected override void Deserialize(Team service, TeamData data)
        {
            throw new System.NotImplementedException();
        }
    }
}