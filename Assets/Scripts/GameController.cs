using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Sprite[] puzzles;

    public Sprite blank;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Sprite> solutionPuzzle = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    public List<Button> solution = new List<Button>();

	//Change to true when complete
    public bool complete;

	//create boolean variables for locations where white can move to
    public bool topRight, topLeft, bottomLeft, bottomRight, top, bottom, left, right, middle;

	//Set the color white to square 15
    public int white = 15;

	//Load the sprite colors for the sqaures
    public void Awake()
	{
        puzzles = Resources.LoadAll<Sprite>("Sprites/Squares");
        blank = Resources.Load<Sprite>("Sprites/Blank");

    }
    public void Start()
    {
        GetButtons();
        ChangeColor();
        SolutionColor();
        AddListeners();
    }

	//create a physical version of the square
    public void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        GameObject[] objects1 = GameObject.FindGameObjectsWithTag("SolutionButton"); //Solution

        for (int counter = 0; counter < objects.Length; counter++)
        {
            btns.Add(objects[counter].GetComponent<Button>());

            solution.Add(objects1[counter].GetComponent<Button>()); //Solution
        }
    }

	//Create the colors for the squares
    public void ChangeColor()
    {
        int looper = btns.Count - 1;
        int index = 0;

        for (int counter = 0; counter < looper; counter++)
        {
            if (index == 4)
            {
                index = 0;
            }

            gamePuzzles.Add(puzzles[index]);
            btns[counter].image.sprite = gamePuzzles[counter];
            index++;
        }
        gamePuzzles.Add(blank);
        btns[15].image.sprite = blank;
    }

    public void SolutionColor()
    {
        int looper = solution.Count - 1;
        int index = 0;

        for (int counter = 0; counter < looper; counter++)
        {
            if (counter == 0 || counter == 4)
                index = 0;
            if (counter == 2 || counter == 6)
                index = 1;
            else if (counter == 8 || counter == 12)
                index = 2;
            else if (counter == 10 || counter == 14)
                index = 3;

            solutionPuzzle.Add(puzzles[index]); //Solution
            solution[counter].image.sprite = solutionPuzzle[counter];
        }
        solution[15].image.sprite = blank;
    }

	//Create Listeners for the squares
    public void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => CheckBoundaries());
        }
    }

	//Check to see which way the white square can move
    public void CheckBoundaries()
    {
        int index;

		//Switch colors first then it will switch the sprite, complete = true when game is complete
        if (complete == false)
        {
			//Get an index for the square that is being clicked on
            index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            locationCheck(white);

            if (topLeft == true)
            {
                if (btns[index] == btns[white + 1] || btns[index] == btns[white + 4])
                {
                    gamePuzzles[white] = gamePuzzles[index];
                    gamePuzzles[index] = blank;
                    btns[index].image.sprite = gamePuzzles[index];
                    btns[white].image.sprite = gamePuzzles[white];
                    topLeft = false;
                    white = index;
                }
            }

            else if(topRight == true)
            {
                if (btns[index] == btns[white - 1] || btns[index] == btns[white + 4])
                {
                    
                    gamePuzzles[white] = gamePuzzles[index];
                    gamePuzzles[index] = blank;
                    btns[index].image.sprite = gamePuzzles[index];
                    btns[white].image.sprite = gamePuzzles[white];
                    topRight = false;
                    white = index;
                }
            }

            else if(bottomLeft == true)
            {
                if (btns[index] == btns[white + 1] || btns[index] == btns[white - 4])
                {                   
                    
                    gamePuzzles[white] = gamePuzzles[index];
                    gamePuzzles[index] = blank;
                    btns[index].image.sprite = gamePuzzles[index];
                    btns[white].image.sprite = gamePuzzles[white];
                    bottomLeft = false;
                    white = index;
                }
            }

            else if(bottomRight == true)
            {
                if (btns[index] == btns[white - 1] || btns[index] == btns[white - 4])
                {
                    
                    gamePuzzles[white] = gamePuzzles[index];
                    gamePuzzles[index] = blank;
                    btns[index].image.sprite = gamePuzzles[index];
                    btns[white].image.sprite = gamePuzzles[white];
                    bottomRight = false;
                    white = index;
                }
            }

            else if(top == true)
            {
                if (btns[index] == btns[white - 1] || btns[index] == btns[white + 1] || btns[index] == btns[white + 4])
                { 
                    
                    gamePuzzles[white] = gamePuzzles[index];
                    gamePuzzles[index] = blank;
                    btns[index].image.sprite = gamePuzzles[index];
                    btns[white].image.sprite = gamePuzzles[white];
                    top = false;
                    white = index;
                }
            }

            else if(bottom == true)
            {
                if (btns[index] == btns[white - 1] || btns[index] == btns[white + 1] || btns[index] == btns[white - 4])
                { 
                    
                    gamePuzzles[white] = gamePuzzles[index];
                    gamePuzzles[index] = blank;
                    btns[index].image.sprite = gamePuzzles[index];
                    btns[white].image.sprite = gamePuzzles[white];
                    bottom = false;
                    white = index;
                }
            }

            else if(left == true)
            {
                if (btns[index] == btns[white + 1] || btns[index] == btns[white + 4] || btns[index] == btns[white - 4])
                {                   
                    
                    gamePuzzles[white] = gamePuzzles[index];
                    gamePuzzles[index] = blank;
                    btns[index].image.sprite = gamePuzzles[index];
                    btns[white].image.sprite = gamePuzzles[white];
                    left = false;
                    white = index;
                }
            }

            else if(right == true)
            {
                if (btns[index] == btns[white - 1] || btns[index] == btns[white + 4] || btns[index] == btns[white - 4])
                {
                    
                    gamePuzzles[white] = gamePuzzles[index];
                    gamePuzzles[index] = blank;
                    btns[index].image.sprite = gamePuzzles[index];
                    btns[white].image.sprite = gamePuzzles[white];
                    right = false;
                    white = index;
                }
            }

            else if(middle == true)
            {
                if (btns[index] == btns[white - 1] || btns[index] == btns[white + 1] || btns[index] == btns[white + 4] || btns[index] == btns[white - 4])
                { 
                    
                    gamePuzzles[white] = gamePuzzles[index];
                    gamePuzzles[index] = blank;
                    btns[index].image.sprite = gamePuzzles[index];
                    btns[white].image.sprite = gamePuzzles[white];
                    middle = false;
                    white = index;
                }
            }

            StartCoroutine(Checking()); //Runs completion check
        }
    }
    
    //Check for completion
    IEnumerator Checking()
    {
        yield return new WaitForSeconds(1f);
        if (complete == false)
        {
            CheckCompletion();
        }
    }

    public void CheckCompletion()
    {
        int index;

        for (index = 0; index < 15; index++)
        {
            if (gamePuzzles[index] == solutionPuzzle[index])
            {
                if (index == 14)
                {
                    Debug.Log("Game is Finished");
                    complete = true;
                }
            }
            else
            {
                index = 0;
                break;
            }
        }
    }
    
	//Check where white is located to check where it can switch
    public void locationCheck(int white)
    {
        if (white == 0)
        {
            topLeft = true;
        }

        else if (white > 0 && white < 3)
        {
            top = true;
        }

        else if (white == 3)
        {
            topRight = true;
        }

        else if (white == 4 && white == 8)
        {
            left = true;
        }

        else if (white == 7 && white == 11)
        {
            right = true;
        }

        else if (white == 12)
        {
            bottomLeft = true;
        }

        else if (white > 12 && white < 15 )
        {
            bottom = true;
        }

        else if (white == 15)
        {
            bottomRight = true;
        }

        else
        {
            middle = true;
        }
    }

}
