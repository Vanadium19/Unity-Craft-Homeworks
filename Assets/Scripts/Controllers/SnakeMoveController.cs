using Modules;
using Zenject;

namespace Controllers
{
    public class SnakeMoveController : ITickable
    {
        private readonly ISnake _snake;
        private readonly IMoveInput _moveInput;

        public SnakeMoveController(ISnake snake, IMoveInput moveInput)
        {
            _snake = snake;
            _moveInput = moveInput;
        }

        public void Tick()
        {
            var direction = _moveInput.GetDirection();
            _snake.Turn(direction);
        }
    }
}