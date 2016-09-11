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
// -- Make random not repeat the same word twice.
// -- Make the game detect keypresses from keyboard as a second input method. (Only from Frontend)
// Use Bootstrap & CSS to make things more pretty.

// Added Features:
// Made score more interactive by making the amount obtained dependant on time remaining.
// Changed overall theme of game from getting as much words as possible in time frame to a time per word theme.
// Time resets for each new word, and the game will carry on until the list of words has been gone through.
// Or if lives run out (lives goes down if time runs out for a word), the score will be cumulative throughout.

public partial class _Default : System.Web.UI.Page
{
    // Here we define all the variables that would be used in the program.
    private static string[] readFile;
    private static bool correctWord;
    private static int correctCount;
    private static int maxWords;
    private static double maxLives;
   
    private static double score;
    private static double currentScore;
    private static double multiplier;
    private static int wordChain;
    private static int randomIndex;
    private static int wrongGuess;

    private static char[] hiddenChar;
    private static char[] wordChar;
    private static char letter;
    private static string word;
    private static string hidden;
    private static string hint;

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
            readFile = File.ReadAllLines(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\wordsN.txt"));
            // Here we split a the above readFile (which is a string array) by the character _ to seperate the word and hint.
            // We then use a lambda expression to split each respective part as a key and value in the dictionary.
            splitFile = readFile.Select(l => l.Split('_')).ToDictionary(a => a[0], a => a[1]);
            // Here we take the dictionary and order it randomly (shuffling) by using the random class and a lambda expression.
            splitFile = splitFile.OrderBy(x => new Random().Next())
               .ToDictionary(item => item.Key, item => item.Value);

            // We set these values all to 0 as a way to reset the game and to clear previous data on the browser.
            currentScore = 0;
            score = 0;
            multiplier = 0;
            wordChain = 0;
            correctCount = 0;

            // Here we set the maxWords to max items found in the dictionary. 
            maxWords = splitFile.Count();
            // We also set the max amount of lives a player has to half the maxWords amount.
            maxLives = Math.Floor((double)maxWords / 2);

            // Here we update these labels to some default values for when the project gets built
            // Only called here once because it changes throughout the game.
            scoreLbl.Text = "SCORE: " + currentScore;
            chainLbl.Text = "Word Chain Multiplier: x" + wordChain;
            countLbl.Text = "Correct Words: 0 out of " + maxWords;
            livesLbl.Text = "Lives remaining: " + maxLives;

            // Then calls the setup method, which sets up a new hidden word and starts the timer for the game.
            setup();    
        }   
    }

    // The setup() method is called everytime a word has been guessed to set up a new one.
    public void setup()
    {
        // These three labels have to be reset to empty otherwise the display will not change for each new word.
        hidden = "";
        lettersLbl.Text = "";
        wordLbl.Text = "";

        // We set the restart button to false as we don't want it to display until the end of the game.
        newBtn.Visible = false;

        // Here we set the timer to 36 seconds for EACH new word generated (AKA when player guesses word correctly)
        startTimer(31);
        seconds = (int)Session["timer"];

        // We set the correctWord to false as it's a new word.
        correctWord = false;

        // The multiplier and wrong guesses is set to the default value for each new word.
        wrongGuess = 0;
        multiplier = 1.50;
        
        // A mini if statement is here to check whether current lives is 0, if it is we reset to the default starting lives.
        // This is called here because we reach here from clicking the restart button.
        if (maxLives == 0) { maxLives = Math.Floor((double)maxWords / 2); }

        // These two labels text will be set to reflect the integer being reset.
        guessNoLbl.Text = "Wrong Letter Count: " + wrongGuess;
        multiLbl.Text = "Correct Guess Multiplier: x" + multiplier;

        // Random item from dictionary is chosen using a random index, note it was already shuffled at page load. 
        // The -1 is needed because problem occurs after we remove the item from dictionary if not set.
        randomIndex = new Random().Next(splitFile.Count() -1);

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
    public void letterGuessed (object sender, EventArgs e)
    {
        // We take the sender argument and change identify it as a button
        Button btn = sender as Button;

        // We then turn both the hidden and word into character arrays.
        hiddenChar = hidden.ToCharArray();
        wordChar = word.ToCharArray();

        // The letter char variable will be the first element in btn.Text, since its a single character it will always be 0.
        letter = btn.Text.ElementAt(0);

        // This if statement block checks the word in uppercase and see if it contains the above letter (char)
        if (word.Contains(letter)) {     

            // Using a for loop, which carries on until it reaches the max length of the word, it checks
            // Whether the character in the word matches the letter guessed. 
            // If it does we assign to the hidden string the new letter.
            for (int i = 0; i < word.Length; i++) {
                if (wordChar[i] == letter && wordChar[i] != '-') { hiddenChar[i] = letter; }
            }

            // Hidden then recompiles as a new string the above hidden character array with the new letter.
            hidden = new string(hiddenChar);

            // Here we use a tiny if statement block to see how long it took the for the user to guess the word
            // And based on the amount of time took it gives a interactive score to reflect it.
            if (seconds >= 23) { score = 300; }
            else if (seconds >= 18) { score = 250; }
            else if (seconds >= 13) { score = 200; }
            else if (seconds >= 8) { score = 150; }
            else { score = 100; }

            // We call the updateScreen() method to update all the visuals on screen.
            updateScreen();
        }
        // The else statement is called if the word didn't contain the letter.
        else {
            // It increments the letter label with each incorrect letter the user guessed.
            lettersLbl.Text += letter;

            // It also increments the wrong guess by a 1 on each incorrect guess.
            wrongGuess++;
            // Here we decrease the timer by 4 seconds for each wrong guess.
            decreaseTime(4);

            // This mini if else statement block changes the multiplier depending on the number of incorrect guesses.
            if (wrongGuess >= 1 && wrongGuess < 3) { multiplier = 1.50; }
            else if (wrongGuess >= 3 && wrongGuess < 5) { multiplier = 1.25; }
            else if (wrongGuess >= 5 && wrongGuess < 7) { multiplier = 1.10; }
            else { multiplier = 1.00; }

            // Finally again we called updateScreen() in order to update the relevant visuals.
            updateScreen();
        }

        // We use this foreach loop to make sure the same button cannot be pressed again.
        foreach (Button b in btnList) { if (b.Text.Equals(btn.Text)) { b.Enabled = false; } }

        // Finally if the hidden word has all been revealed (made equal to word), this if block would run. 
        if (hidden == word) {
            // First we multiply the above score gotten by the multiplier (best to have less wrong guesses!)
            score *= multiplier;

            // We check if there was wrong guesses and whether seconds is not 0, if so this if statement block will run.
            if (wrongGuess == 0 && seconds != 0) {
                // Here we increment wordChain by 1 each time.
                wordChain = wordChain + 1;

                // A nested mini if statement block, gives a SECOND multiplier based on how long the player can chain correct words for.
                // So for best results player should no get any wrong guesses and will consecutively guess the right word.
                if (wordChain >= 6) { score *= 2.00; }
                else if (wordChain >= 5) { score *= 1.50; }
                else if (wordChain >= 4) { score *= 1.40; }
                else if (wordChain >= 3) { score *= 1.30; }
                else if (wordChain >= 2) { score *= 1.20; }
            }
            // If there was a wrong guess, we reset wordChain to 0.
            else { wordChain = 0; }

            // Here we increment correctCount by 1 each time, for each correct word guessed.
            // Tiny if statement as a failsafe to make sure that the correctCount doesnt exceed the maxWords value.
            //if (correctCount > maxWords) { correctCount = maxWords; }
            // Here we set the boolean for correctWord to true to trigger the if statement in updateScreen.
            correctWord = true;

            // This if statement is called as long as there ARE items in the dictionary remaining.
            if (splitFile.Count() == 0) {
                correctCount++;
                // In this case we update the visuals with updateScreen, and then call the endScreen method.
                updateScreen();
                endScreen();

            }
            // Else statement is called if there is NO more items left inside the dictionary
            else {
                correctCount++;
                // Because we got the word right and we want to go through to the next item in the dictionary
                // We remove the current item we have at the random index from the dictionary.
                splitFile.Remove(splitFile.Keys.ElementAt(randomIndex));
                splitFile.Remove(splitFile.Values.ElementAt(randomIndex));

                // Then call update screen to update any visuals needed.
                updateScreen();
                // Finally we call setup to generate the next word inside the dictionary.
                setup();
            }
        }
    }
 
    // The updateScreen() method is used to refresh all the relevant information shown on screen to the user.
    public void updateScreen()
    {
        // We have to set the word label to empty on each click otherwise it would just get longer and longer.
        wordLbl.Text = "";  

        // If correctWord was set to true from guessing the word correctly before, this if statement block runs.
        if (correctWord == true) {
            // Note that currentScore is made equal to score, if we set it to score for the label, it would never increment.
            // So by storing it inside currentScore, it would just add the new score onto the current.
            currentScore += score;

            // A tiny nested if statement block to check what current word chain is and updates the label accordingly.
            if (wordChain >= 6) { chainLbl.Text = "Word Chain Multiplier: x2"; }
            else if (wordChain >= 5) { chainLbl.Text = "Word Chain Multiplier: x1.5"; }
            else if (wordChain >= 4) { chainLbl.Text = "Word Chain Multiplier: x1.4"; }
            else if (wordChain >= 3) { chainLbl.Text = "Word Chain Multiplier: x1.3"; }
            else if (wordChain >= 2) { chainLbl.Text = "Word Chain Multiplier: x1.2"; }
            else { chainLbl.Text = "Word Chain Multiplier: x0"; }

            // We update the label with how much correct words was guessed out of the max amount of words.
            // Here we demonstrate some string formatting usage.
            countLbl.Text = String.Format("Correct Words: {0} out of {1}", correctCount, maxWords);
            // Finally we update the score using the currentScore and rounding it up to nearest whole value.
            scoreLbl.Text = "SCORE: " + Math.Ceiling(currentScore);
        }

        // We update these respective labels for the multiplier, number of wrong guesses and remaining lives.
        guessNoLbl.Text = "Wrong Letter Count: " + wrongGuess;
        multiLbl.Text = "Correct Guess Multiplier: x" + multiplier;
        livesLbl.Text = "Lives remaining: " + maxLives;

        // Finally we use this for loop to keep the string format the same throughout the game.
        for (int i = 0; i < hidden.Length; i++) {
            wordLbl.Text += hidden.Substring(i, 1);
            wordLbl.Text += " ";
        }
    }

    // The endScreen is as it sounds, the final visual to be shown either when game ends or when user quits.
    public void endScreen()
    {
        // Here we use another if statement block, to check how much words was correct out of the max available.
        // As seen the minimum 1.1x bonus is always there, but if the user gets over 63% of the word list
        // guessed the final multiplier rises accordingly.
        if (correctCount == maxWords) {
            scoreLbl.Text = String.Format("[FINAL SCORE: {0}] [2x BONUS! = {1}]", Math.Ceiling(currentScore), Math.Ceiling(currentScore * 2));
        }
        else if (correctCount >= Math.Ceiling(maxWords * 0.87)) {
            scoreLbl.Text = String.Format("[FINAL SCORE: {0}] [1.75x BONUS! = {1}]", Math.Ceiling(currentScore), Math.Ceiling(currentScore * 1.75));
        }
        else if (correctCount >= Math.Ceiling(maxWords * 0.75)) {
            scoreLbl.Text = String.Format("[FINAL SCORE: {0}] [1.5x BONUS! = {1}]", Math.Ceiling(currentScore), Math.Ceiling(currentScore * 1.50));
        }
        else if (correctCount >= Math.Ceiling(maxWords * 0.63)) {
            scoreLbl.Text = String.Format("[FINAL SCORE: {0}] [1.25x BONUS! = {1}]", Math.Ceiling(currentScore), Math.Ceiling(currentScore * 1.25));
        }
        else {
           scoreLbl.Text = String.Format("[FINAL SCORE: {0}] [1.1x BONUS! = {1}]", Math.Ceiling(currentScore), Math.Ceiling(currentScore * 1.10));
        }

        // Here we finally make the restart button visible so the user can start another game if necessary.
        newBtn.Visible = true;

        // We show on the word label the whole word which the user was currently attempting.
        wordLbl.Text = word;

        // Here we disable every letter button in the game.
        foreach (Button b in btnList) { b.Enabled = false; }

        // The label for timer is updated to 0 incase it is any other value.
        timeLbl.Text = "Time remaining: 0";

        // Finally we set the timer to not enabled (visible).
        timer.Enabled = false;
    }

    // The first of two mini methods, this takes a integer argument to decrease the timer of the game.
    // The integer determines the amount of seconds to decrease by.
    public void decreaseTime(int decrease)
    {
        Session["timer"] = seconds;
        seconds = seconds - decrease;
    }

    // The second of two mini methods, this takes a integer argument to increase the timer of the game.
    // The integer determines the amount of seconds to increase by.
    // ** Currently this isn't used because we changed how the game is played. ** 
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
            // Because time has ran out for the current hidden word, we decrement maxLives by 1.
            maxLives--;
            // We then call updateScreen to update for the above visuals.
            updateScreen();

            // Because we want to move onto the next random word, we also need to remove the current item from here.
            splitFile.Remove(splitFile.Keys.ElementAt(randomIndex));
            splitFile.Remove(splitFile.Values.ElementAt(randomIndex));

            // Tiny if else statement block to check whether lives is over 0, if it is we generate a new word.
            // Otherwise we end the game and call endScreen.
            if (maxLives > 0) { setup(); } 
            else { endScreen(); }    
        }
    }

    // Clicking the Quit button at the any time will end the game and calls the endScreen method.
    protected void exitBtn_Click(object sender, EventArgs e)
    {
        endScreen();
    }

    // Clicking on the Restart button is pretty much a carbon copy of calling page load again.
    protected void newBtn_Click(object sender, EventArgs e)
    {
        // Again we assign to the splitFile dictionary the words we read from the file and split them.
        splitFile = readFile.Select(l => l.Split('_')).ToDictionary(a => a[0], a => a[1]);
        // Then again we order and shuffle the list, so the items order are not the same as before.
        splitFile = splitFile.OrderBy(x => new Random().Next())
           .ToDictionary(item => item.Key, item => item.Value);

        // We set these values all to 0 as we want the game to be a fresh start.
        currentScore = 0;
        score = 0;
        multiplier = 0;
        wordChain = 0;
        correctCount = 0;

        // Here we set the maxWords to max items found in the dictionary. 
        maxWords = splitFile.Count();
        // We also set the max amount of lives a player has to half the maxWords amount.
        maxLives = Math.Floor((double)maxWords / 2);

        // Here we update these labels to the default values required.
        // We use a string format this time for the count label.
        scoreLbl.Text = "SCORE: " + currentScore;
        chainLbl.Text = "Current Word Chain Multiplier: x" + wordChain;
        livesLbl.Text = "Lives remaining: " + maxLives;
        countLbl.Text = String.Format("Correct Words: {0} out of {1}", correctCount, maxWords);

        // Finally we restart the game by recalling setup().
        setup();
    }

}