using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Text> Board = new List<Text>();
    [SerializeField] Text VictoryMessage;

    bool playerTurn = true;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (gameOver) {
            return;
        }
        if (playerTurn) {
            for (int i = 0; i < Board.Count; i++) {
                HandleKeyPress(i + 1);
            }

        } else {
            FillFreeSpace();
        }
    }

    void HandleKeyPress(int key) {
        if (Input.GetKeyDown(key.ToString()) &&
            string.IsNullOrEmpty(Board[key - 1].text)) {
            Board[key - 1].text = "X";
            Board[key - 1].color = Color.blue;
            if (IsWinning()) {
                VictoryMessage.text = "The player is the winner!";
                gameOver = true;
            } else if (IsFullBoard()) {
                VictoryMessage.text = "Game over, it's a tie...";
                gameOver = true;
            }
            playerTurn = false;
        }
    }

    void FillFreeSpace() {

        for (int i = 0; i < Board.Count; i++) {
            if (string.IsNullOrEmpty(Board[i].text)) {
                Board[i].color = Color.red;
                Board[i].text = "O";

                if (IsWinning()) {
                    VictoryMessage.text = "The AI is the winner!";
                    gameOver = true;
                    return;
                } else {
                    Board[i].text = "X";
                    if (IsWinning()) {
                        Board[i].text = "O";
                        playerTurn = true;
                        return;
                    } else {
                        Board[i].text = "";
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(Board[4].text)) {
            Board[4].text = "O";
            playerTurn = true;
        } else if (Board[4].text == "O") {
            if (string.IsNullOrEmpty(Board[0].text)) {
                Board[4].text = "O";
                playerTurn = true;
            } else if (string.IsNullOrEmpty(Board[2].text)) {
                Board[4].text = "O";
                playerTurn = true;
            } else if (string.IsNullOrEmpty(Board[6].text)) {
                Board[4].text = "O";
                playerTurn = true;
            } else if (string.IsNullOrEmpty(Board[8].text)) {
                Board[4].text = "O";
                playerTurn = true;
            }
        } else {
            PlaceRandom();
        }
    }

    private void PlaceRandom() {
        int attempts = 0;
        bool done = false;
        while (!done && attempts < 9) {
            attempts++;
            int randomSpace = UnityEngine.Random.Range(0, Board.Count);
            if (string.IsNullOrEmpty(Board[randomSpace].text)) {
                Board[randomSpace].text = "O";
                Board[randomSpace].color = Color.red;
                done = true;
                playerTurn = true;
            }
        }
    }

    bool IsWinning() {
        //row
        for (int i = 0; i < Board.Count; i += 3) {
            if (Board[i].text == Board[i + 1].text && Board[i + 1].text == Board[i + 2].text && string.IsNullOrEmpty(Board[i].text) == false) {
                return true;
            }
        }
        //column
        for (int i = 0; i < 3; i++) {
            if (Board[i].text == Board[i + 3].text && Board[i + 3].text == Board[i + 6].text && string.IsNullOrEmpty(Board[i].text) == false) {
                return true;
            }
        }

        if (Board[0].text == Board[4].text && Board[4].text == Board[8].text && string.IsNullOrEmpty(Board[0].text) == false) {
            return true;
        }

        if (Board[2].text == Board[4].text && Board[4].text == Board[6].text && string.IsNullOrEmpty(Board[2].text) == false) {
            return true;
        }
        return false;
    }

    bool IsFullBoard() {
        for (int i = 0; i < Board.Count; i++) {
            if (string.IsNullOrEmpty(Board[i].text)) {
                return false;
            }
        }
        return true;
    }
}
