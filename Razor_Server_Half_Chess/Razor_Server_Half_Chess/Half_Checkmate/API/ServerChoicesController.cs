using Microsoft.AspNetCore.Mvc;

namespace Razor_Server_Half_Chess.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerChoicesController : ControllerBase
    {
        private readonly Random _random;

        public ServerChoicesController()
        {
            _random = new Random();
        }

        public class ServerChoiceRequest
        {
            public string[][]? PiecePositions { get; set; }
            public List<int[]>[][]? MoveOptions { get; set; }
        }

        public class Pointers
        {
            public int FromX { get; set; }
            public int FromY { get; set; }
            public int ToX { get; set; }
            public int ToY { get; set; }
        }


        [HttpPost]
        public IActionResult GetServerChoice([FromBody] ServerChoiceRequest request)
        {
            if (request.PiecePositions == null || request.MoveOptions == null)
            {
                return BadRequest("Invalid data received: PiecePositions or MoveOptions is null.");
            }

            string[][] piecePositions = request.PiecePositions;
            List<int[]>[][] moveOptions = request.MoveOptions;

            var movablePieces = new List<(int row, int col)>(); // Movable Pieces List

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (piecePositions[row][col] != null && piecePositions[row][col].EndsWith("'"))
                    {
                        if (moveOptions[row][col] != null && moveOptions[row][col].Any())
                        {
                            movablePieces.Add((row, col)); // If the string with "'" add to movablePieces
                        }
                    }
                }
            }

            if (!movablePieces.Any())
            {
                return BadRequest("No valid moves available.");
            }

            // Selecte a random Piece
            var selectedPiece = movablePieces[_random.Next(movablePieces.Count)];
            int fromX = selectedPiece.row;
            int fromY = selectedPiece.col;

            // Selecte a random Move for the selectedPiece
            var possibleMoves = moveOptions[fromX][fromY];
            var selectedMove = possibleMoves[_random.Next(possibleMoves.Count)];

            int toX = selectedMove[0];
            int toY = selectedMove[1];

            return Ok(new Pointers
            {
                FromX = fromX,
                FromY = fromY,
                ToX = toX,
                ToY = toY
            });
        }

        // For promotion case
        [HttpGet("GetRandomString")]
        public IActionResult GetRandomString()
        {
            string[] options = { "Bishop", "Knight", "Rook" };
            string randomString = options[_random.Next(options.Length)];
            return Ok(randomString);
        }

    }
}
