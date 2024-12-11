using Modules;

namespace Controllers
{
    public interface IMoveInput
    {
        public SnakeDirection GetDirection();
    }
}