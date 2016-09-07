using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.UI;

// First Project: Word Guessing Game.
/* My first project for portfolio, is a simple rendition of a word guessing game, unlike the traditional hangman 
 * game this, carries on until the timer expires.
 * Some features include having a final score which goes up for every word the user gets right and 
 * a multiplier which increases/decrease the less letters a user guesses incorrectly,
 * along with a hint for each of the hidden word */

// To-do List:
// -- Add timer to the game, decrement/increment depending if user guesses word correctly.
// -- Let the hidden word ignore spaces so can use multiple words.
// -- Create a hint for each of the hidden word possibly using the split() method.
// Make the game detect keypresses from keyboard as a second input method.
// Use Bootstrap & CSS to make things more pretty.

public partial class _Default : System.Web.UI.Page
{
    // Here we define all the variables that would be used in the program.

    private static string[] readFile;
    private static bool correctWord;
 
    private static double score;
    private static double currentScore;
    private static int chainMulti;
    private static int currentChain;

    private static char[] hiddenChar;
    private static char[] wordChar;

    private static string word;
    private static string hidden;
    private static string letter;
    private static string hint;

    private static double multiplier;
    private static int randomIndex;
    private static int wrongGuess;
    private static int seconds { get; set; }

    private static Dictionary<string, string> splitFile = new Dictionary<string, string>();
    private static List<Button> btnList;


    // The set of instructions which is done as soon as the page is loaded.
    protected void Page_Load(object sender, EventArgs e)
    {
        // Here we add the all the different buttons on the form, representing each of the alphabet keys.
        btnList = new List<Button>() { qBtn, wBtn, eBtn, rBtn, tBtn, yBtn, uBtn, iBtn, oBtn, pBtn,
                                       aBtn, sBtn, dBtn, fBtn, gBtn, hBtn, jBtn, kBtn, lBtn,
                                       zBtn, xBtn, cBtn, vBtn, bBtn, nBtn, mBtn };

        // This if statement goes through as long as the page is not in a postback state.
        if (!IsPostBack) 
        {
            // The readFile variable reads our file from the resources directory.
            // Note that we don't need to specify the whole file path.
            readFile = File.ReadAllLines(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\wordsP.txt"));

            // Here we split a the above readFile (which is a string array) by the character _ to seperate the word and hint.
            // We then use a lambda expression to split each respective part as a key and value in the dictionary.
            splitFile = readFile.Select(l => l.Split('_')).ToDictionary(a => a[0], a => a[1]);

            // Sets the Score label to 0, only called here once because it changes throughout the game.
            // Then calls the setup method, which sets up a new hidden word and starts the timer for the game.
            scoreLbl.Text = "SCORE: 0";
            chainLbl.Text = "Current Word Chain: " + chainMulti;
            setup();
        }   
    }

    // The setup() method is called everytime a word has been guessed to set up a new one.
    protected void setup()
    {
        // These three labels have to be reset to empty otherwise the display will not change for each new word.
        hidden = "";
        lettersLbl.Text = "";
        wordLbl.Text = "";

        if (seconds == 0) { startTimer(121); seconds = (int)Session["timer"]; }

        newBtn.Visible = false;
        exitBtn.Visible = false; 

        // We set the correctWord to false as it's a new word.
        correctWord = false;

        // The following integers and doubles are reset for the score and multiplier as well as number of wrong guesses.
        wrongGuess = 0;
        multiplier = 2.00;
        score = 500;

        // These two labels text will be set to reflect the integer being reset.
        guessNoLbl.Text = "Wrong Letter Count: " + wrongGuess;
        multiLbl.Text = "Current Multiplier: x" + multiplier;

        // The random index uses the random class to assign new random number to take from the list.
        // It's max length never goes over max string count in the list.
        randomIndex = new Random().Next(splitFile.Count());

        // We assign to the word and hint the same random index, and to take a single object from a list.
        // We also make all the letters in the word all uppercase.
        word = splitFile.Keys.ElementAt(randomIndex).ToUpper();
        hint = splitFile.Values.ElementAt(randomIndex);

        // We set this label to the random hint from the above list.
        hintLbl.Text = hint;

        // Here we use a regular expression to detect spaces in the word and replace it with a - character.
        word = new Regex(" ").Replace(word, "-");
        // We make hidden equal to the word.
        hidden = word;
        // And finally we replace all the letters OR numbers in hidden with a _ character.
        hidden = new Regex("([a-zA-z0-9])").Replace(hidden, "_");

        // Using some minor string formating with substring we then place a space between each character.
        for (int i = 0; i < hidden.Length; i++) {
            wordLbl.Text += hidden.Substring(i, 1);
            wordLbl.Text += " ";
        }

        // Finally we enable all the buttons in the game again as it's a new word at this point.
        foreach (Button b in btnList) {
            b.Enabled = true;
        }
    }

    // The letterGuessed is called on every button click in which all buttons have this same method assigned to onCommand.
    // We also use the Command argument e instead of the usual sender as button for the value.
    public void letterGuessed (object sender, CommandEventArgs e)
    {    
        // We take the command argument of the button then it into a string and finally a character array.
        letter = e.CommandArgument.ToString();
        letter.ToCharArray();

        // We then turn both the hidden and word into character arrays.
        hiddenChar = hidden.ToCharArray();
        wordChar = word.ToCharArray();

        // This if statement block checks the word in uppercase and see if it contains the above letter (char array)
        if (word.Contains(letter)) {     

            // Using a for loop, which carries on until it reaches the max length of the word, it checks
            // Whether the character in the word matches the letter guessed. 
            // If it does we assign to the hidden string the new letter.
            for (int i = 0; i < word.Length; i++) {
                if (wordChar[i] == letter[0] && wordChar[i] != '-') { hiddenChar[i] = letter[0]; }
            }

            // Hidden then recompiles as a new string the above hidden character array with the new letter.
            hidden = new string(hiddenChar);
            increaseTime(2);

            // We call the updateScreen() method to update all the visuals on screen.
            updateScreen();
        }
        // The else statement is called if the word didn't contain the letter.
        else {
            // It increments the letter label with each incorrect letter the user guessed.
            lettersLbl.Text += e.CommandArgument;

            // It also increments the wrong guess by a 1 on each incorrect guess.
            wrongGuess++;
            decreaseTime(5);

            // This mini if else statement block changes the multiplier depending on the number of incorrect guesses.
            if (wrongGuess >= 1 && wrongGuess < 3) { multiplier = 1.75; }
            else if (wrongGuess >= 3 && wrongGuess < 5) { multiplier = 1.50; }
            else if (wrongGuess >= 5 && wrongGuess < 7) { multiplier = 1.25; }
            else { multiplier = 1.00; }

            // Finally again we called updateScreen() in order to update the relevant visuals.
            updateScreen();
        }

        // We use this foreach loop to make sure the same button cannot be pressed again.
        foreach (Button b in btnList) { if (b.Text.Equals(e.CommandArgument)) { b.Enabled = false; } }

        // Finally if the hidden word has all been revealed (made equal to word), this if block would run. 

        if (hidden == word) {

            if (wrongGuess == 0) { chainMulti = chainMulti + 1; }
            else { chainMulti = 0; }
    
            score *= multiplier;
            // Here we set correctWord to true.
            correctWord = true;

            //wordLbl.Text = "That was the correct word!";

            // Then updateScreen() method for visuals and finally the setup() method to start a new word.
            updateScreen();
            setup();
        }
    }
 
    // The updateScreen() method is used to refresh all the relevant information shown on screen to the user.
    protected void updateScreen()
    {
        // We have to set the word label to empty on each click otherwise it would just get longer and longer.
        wordLbl.Text = "";  

        // If the user guessed the correct word, we finally increment the score with this if statement.
        if (correctWord == true) {
            // Note that currentScore is made equal to score which is always a static 500
            // But the score label withs currentScore rather score as to increment on each correct word guess.
            currentChain += chainMulti;
            currentScore += score;
            scoreLbl.Text = "SCORE: " + currentScore;
        }

        // We update these respective labels the multiplier and the number of wrong guesses.
        guessNoLbl.Text = "Wrong Letter Count: " + wrongGuess;
        multiLbl.Text = "Current Multiplier: x" + multiplier;
        chainLbl.Text = "Current Word Chain: " + chainMulti;

        // Finally we use this for loop to keep the string format the same throughout the game.
        for (int i = 0; i < hidden.Length; i++) {
            wordLbl.Text += hidden.Substring(i, 1);
            wordLbl.Text += " ";
        }

    }

    public void decreaseTime(int decrease)
    {
        Session["timer"] = seconds;
        seconds = seconds - decrease;
    }

    public void increaseTime(int increase)
    {
        Session["timer"] = seconds;
        seconds = seconds + increase;
    }
    // This mini method is purely here to start and increment the timer we use in the game.
    public void startTimer(int seconds)
    {
        // Note that it adds the timer on a new session created.
        Session.Add("timer", seconds);
        timer.Enabled = true;
    }
    // The timer tick method updates every 1000 milliseconds (1 second) with the following instructions
    protected void timer_Tick(object sender, EventArgs e)
    {
        // If seconds is over 0, the if statement block goes through, and decrements the seconds each second
        if (seconds > 0) {
            seconds--;
            // We make sure that timer session is equavilent to the seconds in question
            Session["timer"] = seconds;
            // Finally we assign to the timer label the seconds remaining
            timeLbl.Text = "Time remaining: " + seconds.ToString();
        }

        // Else if seconds is not over (aka is 0) this block of instructions goes off.
        else {

            if ((chainMulti >= 2 && chainMulti < 4)) {
                currentScore *= 3;
                scoreLbl.Text = "FINAL SCORE (x3): " + currentScore;
                chainLbl.Text = String.Format("Good Job! 3x Chain Multiplier for: {0} Word Chain!", chainMulti);
            }

            else if (chainMulti >= 4) {
                currentScore *= 4.5;
                scoreLbl.Text = "FINAL SCORE (x4.5): " + currentScore;
                chainLbl.Text = String.Format("Great Job! 4.5x Chain Multiplier for: {0} Word Chain!", chainMulti);
            }

            else if (chainMulti == 1) {
                currentScore *= 1.5;
                scoreLbl.Text = "FINAL SCORE (x1.5): " + currentScore;
                chainLbl.Text = String.Format("Not Bad! 1.5x Chain Multiplier for: {0} Word Chain!", chainMulti);
            }

            else {
                scoreLbl.Text = "FINAL SCORE (x0): " + currentScore;
                chainLbl.Text = String.Format("Sadly .. 0x Chain Multiplier for: {0} Word Chain :(", chainMulti);
            }

            newBtn.Visible = true;
            exitBtn.Visible = true;

            // We show on the word label the whole word which the user was currently attempting.
            wordLbl.Text = word;

            // Disable every button found in the game.
            foreach (Button b in btnList) { b.Enabled = false; }

            timeLbl.Text = "Time remaining: 0";

            // Finally we set the timer to not enabled (visible).
            timer.Enabled = false;
        }
    }

    protected void exitBtn_Click(object sender, EventArgs e)
    {


    }

    protected void newBtn_Click(object sender, EventArgs e)
    {
        currentScore = 0;
        chainMulti = 0;
        setup();
    }
}