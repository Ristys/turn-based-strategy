using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurnOrder : MonoBehaviour
{
    List<PlayerStats> players;
    GameObject[] playerObjects;
    //NavMeshAgent agent;

    private int currentPhase;

    void Start()
    {

        // Will hold a List of all playable characters in the scene.
        players = new List<PlayerStats>();

        // If the players haven't been gathered, do so. Put them in the List
        if (players.Count == 0) {
            playerObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in playerObjects) {
                players.Add(player.GetComponent(typeof (PlayerStats)) as PlayerStats);
                UnityEngine.Debug.Log(player.GetComponent(typeof (PlayerStats)) as PlayerStats + " was found and loaded into: " + players);
            }       
        }

        // Player 1 goes first.
        //agent = players[0].GetComponent<NavMeshAgent>();
        players[0].isTurn = true;
        players[0].canMove = true;
        currentPhase = 4;
    }

    void Update()
    {
        if (!players[WhosTurnIsIt()].hasMoved && (currentPhase == 1 || currentPhase == 4))
        {
            UnityEngine.Debug.Log("We're waiting for " + players[WhosTurnIsIt()] + " to move.");
            if (Input.GetMouseButtonDown(1)) {
                AdvancePhase(currentPhase);
            }

        } 
        else if (!players[WhosTurnIsIt()].hasPlayedCard && currentPhase == 2) {
            UnityEngine.Debug.Log("We're waiting for " + players[WhosTurnIsIt()] + " to play a card.");
            //TODO: Display cards in hand and handle picking/playing one.
            if (Input.GetKeyDown(KeyCode.E)) {
                AdvancePhase(currentPhase);
            }

        }
        else if (!players[WhosTurnIsIt()].hasEndedTurn && currentPhase == 3) {
            UnityEngine.Debug.Log("We're waiting for " + players[WhosTurnIsIt()] + " to end their turn.");
            if (Input.GetKeyDown(KeyCode.Space)) {
                AdvancePhase(currentPhase);
            }
        }
        else 
        {
            UnityEngine.Debug.Log("We somehow left the game.");
            //AdvancePhase(currentPhase);
        }
    }

    public List<PlayerStats> GetAllPlayers()
    {
        return players;
    }

    // Returns the index of the player who's turn it is.
    public int WhosTurnIsIt()
    {
        int numberOfClaims = 0;
        int loopIndex = 0;
        int turnIndex = 0;

        foreach (PlayerStats player in players) {
            loopIndex++;
            if (player.isTurn) {
                numberOfClaims++;
                turnIndex = loopIndex-1;
            }
        }

        if (numberOfClaims > 1) {
            UnityEngine.Debug.Log("There was a problem. " + numberOfClaims + "Players claim it's their turn.");
        }

        return turnIndex;
    }

    //1 = MOVE PHASE; 2 = CARD PHASE; 3 = END TURN;
    public void AdvancePhase(int prvPhase)
    {
        int currentPlayerIndex = WhosTurnIsIt();
        switch (prvPhase) {
            // Moving from move phase to card phase.
            case 1:
                currentPhase = 2;
                players[currentPlayerIndex].canMove = false;
                players[currentPlayerIndex].hasMoved = true;
                // Do stuff with cards. Prepare cards for picking.
                break;

            // Moving from card phase to end turn phase.
            case 2:
                currentPhase = 3;
                players[currentPlayerIndex].hasPlayedCard = true;
                players[currentPlayerIndex].canMove = false;
                break;

            // Next player's turn, and moving to move phase.
            case 3:
                currentPhase = 1;
                players[currentPlayerIndex].hasEndedTurn = false;
                players[currentPlayerIndex].canMove = false;
                players[currentPlayerIndex].isTurn = false;
                players[currentPlayerIndex].hasPlayedCard = false;
                players[currentPlayerIndex].hasMoved = false;

                if (currentPlayerIndex < players.Count - 1) {
                    players[currentPlayerIndex + 1].isTurn = true;
                    players[currentPlayerIndex + 1].canMove = true;
                    break;
                }
                else if (currentPlayerIndex >= players.Count - 1) {
                    players[0].isTurn = true;
                    players[0].canMove = true;
                    break;
                }
                break;
            
            // The game just started, player one goes first.
            case 4:
                currentPhase = 1;
                players[0].canMove = true;
                players[0].hasMoved = false;
                players[0].hasPlayedCard = false;
                players[0].hasEndedTurn = false;
                //players[0].moveToTarget();

                break;

            default:
                UnityEngine.Debug.Log("We somehow are trying a 4th phase?");
                break;
        }
    }


    // CURRENTLY THIS IS FOR DEBUGGING ONLY
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 200, 200));
        GUILayout.Label("This Player's turn: " + players[WhosTurnIsIt()]);
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(20, 100, 200, 200));
        GUILayout.Label("Player 1: " + players[0]);
        GUILayout.Label("can move: " + players[0].canMove);
        GUILayout.Label("has moved: " + players[0].hasMoved);
        GUILayout.Label("has played card: " + players[0].hasPlayedCard);
        GUILayout.Label("has ended turn: " + players[0].hasEndedTurn);
        GUILayout.Label("is turn: " + players[0].isTurn);
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(20, 300, 200, 200));
        GUILayout.Label("Player 2: " + players[1]);
        GUILayout.Label("can move: " + players[1].canMove);
        GUILayout.Label("has moved: " + players[1].hasMoved);
        GUILayout.Label("has played card: " + players[1].hasPlayedCard);
        GUILayout.Label("has ended turn: " + players[1].hasEndedTurn);
        GUILayout.Label("is turn: " + players[1].isTurn);
        GUILayout.EndArea();

    }
}
