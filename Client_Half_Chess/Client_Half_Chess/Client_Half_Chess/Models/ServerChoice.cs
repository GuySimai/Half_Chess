using Client_Half_Chess.Models;
using Client_Half_Chess;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using System.Linq;

public class Pointers
{
    public int FromX { get; set; }
    public int FromY { get; set; }
    public int ToX { get; set; }
    public int ToY { get; set; }
}

public class ServerChoice
{
    // HttpClient
    private static HttpClient client;
    private const string PATH = "https://localhost:7054/";
    private const string TO_ServerChoice = "api/ServerChoices";

    // Variables for the server
    private string[][] PiecePositions { get; set; }
    private List<int[]>[][] MoveOptions { get; set; }

    public ServerChoice()
    {
        PiecePositions = new string[GameBoard.ROWS][];
        MoveOptions = new List<int[]>[GameBoard.ROWS][];

        for (int i = 0; i < GameBoard.ROWS; i++)
        {
            PiecePositions[i] = new string[GameBoard.COLUMNS];
            MoveOptions[i] = new List<int[]>[GameBoard.COLUMNS];
        }

        if (client == null)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(PATH)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

    // Fill the matrices
    public void FillPiecePositions(Player playerUser, Player playerServer, Button[,] boardButtons)
    {
        for (int row = 0; row < GameBoard.ROWS; row++)
        {
            for (int col = 0; col < GameBoard.COLUMNS; col++)
            {
                if (playerUser.pieces[row, col] != null)
                {
                    PiecePositions[row][col] = playerUser.pieces[row, col].GetType().Name.ToLower();
                }
                else if (playerServer.pieces[row, col] != null)
                {
                    PiecePositions[row][col] = playerServer.pieces[row, col].GetType().Name.ToUpper();
                    MoveOptions[row][col] = GetPossibleMoves(playerServer, playerUser, row, col, boardButtons);

                    if (MoveOptions[row][col].Any())
                    {
                        PiecePositions[row][col] += "'";
                    }
                }
                else
                {
                    PiecePositions[row][col] = null;
                }
            }
        }
    }

    // Fill the matrices helper
    private List<int[]> GetPossibleMoves(Player PiecesGroup, Player OtherGroup, int row, int col, Button[,] boardButtons)
    {
        List<int[]> possibleMoves = new List<int[]>();

        if (PiecesGroup.pieces[row, col] != null)
        {
            ChessPiece selectedPiece = PiecesGroup.pieces[row, col];
            List<Button> possibilities = PiecesGroup.ThePossibilities(OtherGroup, row, col, selectedPiece, boardButtons);

            foreach (Button btn in possibilities)
            {
                int possibilityRow = (btn.Top - GameBoard.MARGIN) / btn.Height;
                int possibilityCol = (btn.Left - GameBoard.MARGIN) / btn.Width;

                possibleMoves.Add(new int[] { possibilityRow, possibilityCol });
            }
        }

        return possibleMoves;
    }

    // Send to the server the data and get Pointers
    public async Task<Pointers> SendToServerAndGetPointsAsync()
    {
        var data = new
        {
            PiecePositions = PiecePositions,
            MoveOptions = MoveOptions
        };

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(TO_ServerChoice, content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var serverChoice = JsonConvert.DeserializeObject<Pointers>(result);

            Pointers pointers = new Pointers();

            pointers.FromX = serverChoice.FromX;
            pointers.FromY = serverChoice.FromY;
            pointers.ToX = serverChoice.ToX;
            pointers.ToY = serverChoice.ToY;
            return pointers;
        }
        return null;
    }

    // For promotion case
    public async Task<string> GetRandomStringAsync()
    {
        const string RANDOM_STRING_ENDPOINT = TO_ServerChoice + "/GetRandomString";
        HttpResponseMessage response = await client.GetAsync(RANDOM_STRING_ENDPOINT);

        if (response.IsSuccessStatusCode)
        {
            string jsonString = await response.Content.ReadAsStringAsync();
            string randomString = jsonString.Trim('"');
            return randomString;
        }
        return null;
    }

}
