﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Game : System.Web.UI.Page
{
    private static string[] readFile;
    private static string word;
    private static string hidden;
    private static string hint;

    private static char[] hiddenChar;
    private static char[] wordChar;
    private static char letter;

    private static double maxLives;
    private static double score;
    private static double currentScore;
    private static double multiplier;

    private static int maxWords;
    private static int randomIndex;
    private static int wordChain;
    private static int wrongGuess;

    private static int pauseCount;
    private static int correctCount;
    private static int currentCount;
    private static int wrongWord;

    private static int seconds { get; set; }
    private static bool skipClick;
    private static bool correctWord;
    private static bool gameOver;

    private static Dictionary<string, string> splitFile = new Dictionary<string, string>();
    private static List<Button> btnList;
    private static List<Button> usedBtn;
    private static CheckBox prevDiff = null;
    private static Button prevSub = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Here we add the all the different buttons on the form, representing each of the alphabet keys.
        btnList = new List<Button>() { qBtn, wBtn, eBtn, rBtn, tBtn, yBtn, uBtn, iBtn, oBtn, pBtn,
                                       aBtn, sBtn, dBtn, fBtn, gBtn, hBtn, jBtn, kBtn, lBtn,
                                       zBtn, xBtn, cBtn, vBtn, bBtn, nBtn, mBtn };

        // This if statement goes through as long as the page is not in a postback state.
        if (!IsPostBack)
        {
            usedBtn = new List<Button>();
            addScoreLbl.Visible = false;
            fScoreLbl.Visible = false;

            if (Global.EasyBool == true) {
                var prevMaster = PreviousPage.Master.FindControl("MainContent");
                prevDiff = (CheckBox)prevMaster.FindControl("easyTick");
                diffLbl.Text = String.Format("Difficulty: {0}.", prevDiff.Text);
            }

            if (Global.NormBool == true) {
                var prevMaster = PreviousPage.Master.FindControl("MainContent");
                prevDiff = (CheckBox)prevMaster.FindControl("normTick");
                diffLbl.Text = String.Format("Difficulty: {0}.", prevDiff.Text);
            }

            if (Global.HardBool == true) {
                var prevMaster = PreviousPage.Master.FindControl("MainContent");
                prevDiff = (CheckBox)prevMaster.FindControl("hardTick");
                diffLbl.Text = String.Format("Difficulty: {0}.", prevDiff.Text);
            }

            if (Global.PrgBool == true) {
                var prevMaster = PreviousPage.Master.FindControl("MainContent");
                prevSub = (Button)prevMaster.FindControl("prgBtn");
                subLbl.Text = String.Format("Subject: {0}.", prevSub.Text);
            }

            if (Global.GnlBool == true) {
                var prevMaster = PreviousPage.Master.FindControl("MainContent");
                prevSub = (Button)prevMaster.FindControl("gnlBtn");
                subLbl.Text = String.Format("Subject: {0}.", prevSub.Text);
            }

            if (Global.VgmBool == true) {
                var prevMaster = PreviousPage.Master.FindControl("MainContent");
                prevSub = (Button)prevMaster.FindControl("vgmBtn");
                subLbl.Text = String.Format("Subject: {0}.", prevSub.Text);
            }

            // Here we clear the Dictionary just incase there some items left in from a previous session.
            splitFile.Clear();

            // The readFile variable reads our file from the resources directory.
            // Note that we don't need to specify the whole file path.
            if (Global.GnlBool == true) {
                readFile = File.ReadAllLines(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\wordsG.txt"));
            }
            else if (Global.PrgBool == true) {
                readFile = File.ReadAllLines(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\wordsP.txt"));
            }
            else if (Global.VgmBool == true) {
                readFile = File.ReadAllLines(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resources\wordsV.txt"));
            }

            // Here we split a the above readFile (which is a string array) by the character _ to seperate the word and hint.
            // We then use a lambda expression to split each respective part as a key and value in the dictionary.
            splitFile = readFile.Select(l => l.Split('_')).ToDictionary(a => a[0], a => a[1]);

            // Here we take the dictionary and order it randomly (shuffling) by using the random class and a lambda expression.
            splitFile = splitFile.OrderBy(x => new Random().Next()).ToDictionary(item => item.Key, item => item.Value);

            // We set these values all to 0 as a way to reset the game and to clear previous data on the browser.
            currentScore = 0;
            score = 0;
            multiplier = 0;
            wordChain = 0;
            correctCount = 0;
            wrongWord = 0;
            pauseCount = 0;
            maxLives = 0;
            currentCount = 1;

            // We also set the max amount of lives a player has to half the maxWords amount.
            if (Global.EasyBool == true) {
                while (splitFile.Count() > 9) { splitFile.Remove(splitFile.Keys.Last()); }
                // Here we set the maxWords to max items found in the dictionary. 
                maxWords = splitFile.Count();
                maxLives = Math.Ceiling((double)maxWords / 1.3);
                pauseCount = 2;
            }
            else if (Global.NormBool == true) {
                while (splitFile.Count() > 14) { splitFile.Remove(splitFile.Keys.Last()); }
                // Here we set the maxWords to max items found in the dictionary. 
                maxWords = splitFile.Count();
                maxLives = Math.Floor((double)maxWords / 1.6);
                pauseCount = 3;
            }
            else if (Global.HardBool == true) {
                while (splitFile.Count() > 20) { splitFile.Remove(splitFile.Keys.Last()); }
                // Here we set the maxWords to max items found in the dictionary. 
                maxWords = splitFile.Count();
                maxLives = Math.Floor((double)maxWords / 3.0);
                pauseCount = 4;
            }

            // Here we update these labels to some default values for when the project gets built
            // Only called here once because it changes throughout the game.
            scoreLbl.Text = "SCORE: " + currentScore;
            chainNoLbl.Text = "x1";
            countNoLbl.Text = "0 / " + maxWords;
            livesNoLbl.Text = maxLives.ToString();
            wrongNoLbl.Text = "0 / " + maxWords;
            currentNoLbl.Text = "1 / " + maxWords;
            pauseBtn.Text = String.Format("Pause ({0})", pauseCount);
            bonusNoLbl.Text = "(End Bonus: x0)";

            // Finally we enable all the buttons in the game again as it's a new word at this point.
            foreach (Button b in btnList) { b.Enabled = true; }

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
        hintLbl.Text = "";

        // We set the restart button to false as we don't want it to display until the end of the game.
        newBtn.Visible = false;

        // Here we set the timer to 36 seconds for EACH new word generated (AKA when player guesses word correctly)
        // The multiplier and wrong guesses is set to the default value for each new word.
        if (Global.EasyBool == true) {
            startTimer(58);
            multiplier = 1.75;
        }
        else if (Global.NormBool == true) {
            startTimer(43);
            multiplier = 2.00;
        }
        else if (Global.HardBool == true) {
            startTimer(28);
            multiplier = 2.50;
        }
        
        seconds = (int)Session["timer"];
        wrongGuess = 0;

        // We set the correctWord to false as it's a new word.
        correctWord = false;
        skipClick = false;
        gameOver = false;

        // A mini if statement is here to check whether current lives is 0, if it is we reset to the default starting lives.
        // This is called here because we reach here from clicking the restart button.
        if (maxLives == 0) { maxLives = Math.Floor((double)maxWords / 2); }

        // These two labels text will be set to reflect the integer being reset.
        guessNoLbl.Text = wrongGuess.ToString(); ;
        multiNoLbl.Text = "x" + multiplier;

        // Random item from dictionary is chosen using a random index, note it was already shuffled at page load. 
        // The -1 is needed because problem occurs after we remove the item from dictionary if not set.
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

        skipBtn.Enabled = true;

        if (pauseCount != 0) {
            playBtn.Enabled = false;
            playBtn.Visible = false;

            pauseBtn.Visible = true;
            pauseBtn.Enabled = true;
        }

        usedBtn.Clear();
        updateScreen();
    }

    // The letterGuessed is called on every button click in which all buttons have this same method assigned to onCommand.
    // We also use the Command argument e instead of the usual sender as button for the value.
    public void letterGuessed(object sender, EventArgs e)
    {
        // We take the sender argument and change identify it as a button
        Button btn = sender as Button;

        // We then turn both the hidden and word into character arrays.
        hiddenChar = hidden.ToCharArray();
        wordChar = word.ToCharArray();

        // The letter char variable will be the first element in btn.Text, since its a single character it will always be 0.
        letter = btn.Text.ElementAt(0);

        // We use this foreach loop to make sure the same button cannot be pressed again.
        foreach (Button b in btnList) {
            if (b.Text.Equals(btn.Text)) {
                b.Enabled = false;
                usedBtn.Add(b);
            }
        }
    
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
            if (Global.EasyBool == true) {
                if (seconds >= 51) { score = 300; }
                else if (seconds >= 41) { score = 255; }
                else if (seconds >= 31) { score = 195; }
                else if (seconds >= 21) { score = 135; }
                else if (seconds >= 11) { score = 75; }
                else { score = 50; }
            }
            else if (Global.NormBool == true) {
                if (seconds >= 34) { score = 350; }
                else if (seconds >= 28) { score = 225; }
                else if (seconds >= 22) { score = 150; }
                else if (seconds >= 16) { score = 100; }
                else if (seconds >= 10) { score = 75; }
                else { score = 50; }
            }
            else if (Global.HardBool == true) {
                if (seconds >= 20) { score = 400; }
                else if (seconds >= 17) { score = 320; }
                else if (seconds >= 14) { score = 240; }
                else if (seconds >= 11) { score = 160; }
                else if (seconds >= 9) { score = 80; }
                else { score = 50; }
            }

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
            // This mini if else statement block changes the multiplier depending on the number of incorrect guesses.
            if (Global.EasyBool == true) {
                decreaseTime(3);
                if (wrongGuess >= 3 && wrongGuess < 6) { multiplier = 1.75; }
                else if (wrongGuess >= 6 && wrongGuess < 7) { multiplier = 1.50; }
                else if (wrongGuess >= 7 && wrongGuess < 8) { multiplier = 1.25; }
                else { multiplier = 1.00; }
            }
            else if (Global.NormBool == true) {
                decreaseTime(4);
                if (wrongGuess >= 2 && wrongGuess < 4) { multiplier = 2.00; }
                else if (wrongGuess >= 4 && wrongGuess < 6) { multiplier = 1.75; }
                else if (wrongGuess >= 6 && wrongGuess < 8) { multiplier = 1.50; }
                else { multiplier = 1.00; }
            }
            else if (Global.HardBool == true) {
                decreaseTime(5);
                if (wrongGuess >= 1 && wrongGuess < 3) { multiplier = 2.50; }
                else if (wrongGuess >= 3 && wrongGuess < 5) { multiplier = 2.00; }
                else if (wrongGuess >= 5 && wrongGuess < 7) { multiplier = 1.75; }
                else { multiplier = 1.00; }
            }

            // Finally again we called updateScreen() in order to update the relevant visuals.
            updateScreen();
        }
        // Finally if the hidden word has all been revealed (made equal to word), this if block would run. 
       if (hidden == word) {

            foreach (Button b in btnList) { b.Enabled = false; }

            hintLbl.Text = "";
            correctWord = true;

            // First we multiply the above score gotten by the multiplier (best to have less wrong guesses!)
            score *= multiplier;

            // We check that the seconds is over 0, if so this if statement block will run.
            if ((wrongGuess == 0) && (seconds != 0)) {
                // Here we increment wordChain by 1 each time.
                wordChain = wordChain + 1;

                // A nested mini if statement block, gives a SECOND multiplier based on how long the player can chain correct words for.
                // So for best results player should no get any wrong guesses and will consecutively guess the right word.
                // Note that it also checks how long user took to guess the word correctly and see which bonus matches for it.
                // If the user takes too long wordChain would get reset back to 0.

                if (Global.EasyBool == true) {
                    if (wordChain >= 4 && seconds >= 51) { score *= 2.00; }
                    else if (wordChain >= 3 && seconds >= 41) { score *= 1.60; }
                    else if (wordChain >= 2 && seconds >= 31) { score *= 1.40; }
                    else if (wordChain >= 1 && seconds >= 21) { score *= 1.20; }
                    else if (seconds < 11) { score *= 1.00; wordChain = 0; }
                }
                else if (Global.NormBool == true)  {
                    if (wordChain >= 6 && seconds >= 34) { score *= 2.50; }
                    else if (wordChain >= 5 && seconds >= 28) { score *= 2.00; }
                    else if (wordChain >= 4 && seconds >= 22) { score *= 1.75; }
                    else if (wordChain >= 3 && seconds >= 16) { score *= 1.50; }
                    else if (wordChain >= 2 && seconds >= 10) { score *= 1.25; }
                    else if (seconds < 9) { score *= 1.00; wordChain = 0; }
                }
                else if (Global.HardBool == true) {
                    if (wordChain >= 8 && seconds >= 20) { score *= 3.00; }
                    else if (wordChain >= 6 && seconds >= 17) { score *= 2.50; }
                    else if (wordChain >= 4 && seconds >= 14) { score *= 2.00; }
                    else if (wordChain >= 3 && seconds >= 11) { score *= 1.75; }
                    else if (wordChain >= 2 && seconds >= 9) { score *= 1.50; }
                    else if (seconds < 7) { score *= 1.00; wordChain = 0; }
                }
            }

            // If there was a wrong guess, we reset wordChain to 0.
            else { wordChain = 0; }

            // Here we increment correctCount by 1 each time, for each correct word guessed.
            correctCount++;
            currentCount++;
            // In this case we update the visuals with updateScreen, and then call the finishScreen method.
            updateScreen();

            // Because we got the word right and we want to go through to the next item in the dictionary
            // We remove the current item we have at the random index from the dictionary.
            splitFile.Remove(splitFile.Keys.ElementAt(randomIndex));

            // Here we set the boolean for correctWord to true to trigger the if statement in updateScreen.
            // This if statement is called if there is more then 1 item in the dictonary.
            if (splitFile.Count() == 0) {   
                gameOver = true;
                finishScreen();
            }
            // Else statement is called when the its the last item remaining in dictionary.
            else {
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

        if (currentCount > maxWords) { currentCount = maxWords; }
        currentNoLbl.Text = String.Format("{0} / {1}", currentCount, maxWords);

        // If correctWord was set to true from guessing the word correctly before, this if statement block runs.
        if (correctWord == true) {
            hintLbl.Text = "";
            // Note that currentScore is made equal to score, if we set it to score for the label, it would never increment.
            // So by storing it inside currentScore, it would just add the new score onto the current.
            currentScore += score;
            addScoreLbl.Visible = true;

            // A tiny nested if statement block to check what current word chain and time is and updates the label accordingly.
            if (Global.EasyBool == true) {
                if (wordChain >= 4 && seconds >= 51) { chainNoLbl.Text = "x2"; }
                else if (wordChain >= 3 && seconds >= 41) { chainNoLbl.Text = "x1.6"; }
                else if (wordChain >= 2 && seconds >= 31) { chainNoLbl.Text = "x1.4"; }
                else if (wordChain >= 1 && seconds >= 21) { chainNoLbl.Text = "x1.2"; }
                else { chainNoLbl.Text = "x1"; }
            }
            else if (Global.NormBool == true) {
                if (wordChain >= 6 && seconds >= 34) { chainNoLbl.Text = "x2.5"; }
                else if (wordChain >= 5 && seconds >= 28) { chainNoLbl.Text = "x2"; }
                else if (wordChain >= 4 && seconds >= 22) { chainNoLbl.Text = "x1.75"; }
                else if (wordChain >= 3 && seconds >= 16) { chainNoLbl.Text = "x1.5"; }
                else if (wordChain >= 2 && seconds >= 10) { chainNoLbl.Text = "x1.25"; }
                else { chainNoLbl.Text = "x1"; }
            }
            else if (Global.HardBool == true) {
                if (wordChain >= 8 && seconds >= 20) { chainNoLbl.Text = "x3"; }
                else if (wordChain >= 6 && seconds >= 17) { chainNoLbl.Text = "x2.5"; }
                else if (wordChain >= 4 && seconds >= 14) { chainNoLbl.Text = "x2"; }
                else if (wordChain >= 3 && seconds >= 11) { chainNoLbl.Text = "x1.75"; }
                else if (wordChain >= 2 && seconds >= 9) { chainNoLbl.Text = "x1.5"; }
                else { chainNoLbl.Text = "x1"; }
            }

            if (correctCount == maxWords) { bonusNoLbl.Text = "(End Bonus: x2)"; }
            else if (correctCount >= Math.Round(maxWords * 0.87)) { bonusNoLbl.Text = "(End Bonus: x1.75)"; }
            else if (correctCount >= Math.Round(maxWords * 0.75)) { bonusNoLbl.Text = "(End Bonus: x1.5)"; }
            else if (correctCount >= Math.Round(maxWords * 0.63)) { bonusNoLbl.Text = "(End Bonus: x1.25)"; }
  
            // We update the label with how much correct words was guessed out of the max amount of words.
            // Here we demonstrate some string formatting usage.
            countNoLbl.Text = String.Format("{0} / {1}", correctCount, maxWords);
            // Finally we update the score using the currentScore and rounding it up to nearest whole value.
            scoreLbl.Text = "SCORE: " + Math.Ceiling(currentScore);
            addScoreLbl.Text = String.Format("(+{0})", score);
        }

        // We update these respective labels for the multiplier, number of wrong guesses, wrong words count
        // And finally remaining lives.
        guessNoLbl.Text = wrongGuess.ToString();
        multiNoLbl.Text = "x" + multiplier;
        wrongNoLbl.Text = String.Format("{0} / {1}", wrongWord, maxWords);
        livesNoLbl.Text = maxLives.ToString();

        // Finally we use this for loop to keep the string format the same throughout the game.
        for (int i = 0; i < hidden.Length; i++) {
            wordLbl.Text += hidden.Substring(i, 1);
            wordLbl.Text += " ";
        }
    }

    // The finishScreen is as it sounds, the final visual to be shown either when game ends or when the user quits.
    public void finishScreen()
    {
        timeMsgLbl.Visible = true;
        addScoreLbl.Visible = false;

        if (gameOver == false) {
            timer.Enabled = true;
            timeMsgLbl.Text = "<span style='color: red;'> Next Word In ";
            seconds = 6;
            wordChain = 0;
            chainNoLbl.Text = "x1";
        }

        // Here we use another if statement block, to check how much words was correct out of the max available.
        // As seen the minimum 1.1x bonus is always there, but if the user gets over 63% of the word list 
        // guessed the final multiplier rises accordingly.
        if ((maxLives == 0) || (splitFile.Count == 0)) {
            timeMsgLbl.Text = "<span style='color: red;'> GAME OVER! </span>";
            timeLbl.Visible = false;
            fScoreLbl.Visible = true;
            timer.Enabled = false;

            if (correctCount == maxWords) {
                fScoreLbl.Text = String.Format("<span style='color: red;'> FINAL SCORE = {0} (x2 BONUS) </span>", Math.Ceiling(currentScore * 2));
            }
            else if (correctCount >= Math.Round(maxWords * 0.87)) {
                fScoreLbl.Text = String.Format("<span style='color: red;'> FINAL SCORE = {0} (x1.75 BONUS)</span>", Math.Ceiling(currentScore * 1.75));
            }
            else if (correctCount >= Math.Round(maxWords * 0.75)) {
                fScoreLbl.Text = String.Format("<span style='color: red;'> FINAL SCORE = {0} (x1.5 BONUS)</span>", Math.Ceiling(currentScore * 1.50));
            }
            else if (correctCount >= Math.Round(maxWords * 0.63)) {
                fScoreLbl.Text = String.Format("<span style='color: red;'> FINAL SCORE = {0} (x1.25 BONUS)</span>", Math.Ceiling(currentScore * 1.25));
            }
            else {
                fScoreLbl.Text = String.Format("<span style='color: red;'> FINAL SCORE = {0} </span>", Math.Ceiling(currentScore * 1.10));
            }

            if (score != 0) {
                addScoreLbl.Visible = true;
                addScoreLbl.Text = String.Format("(+{0})", score);
            }
            else { addScoreLbl.Visible = false; }

            // Here we finally make the restart button visible so the user can start another game if necessary.
            newBtn.Visible = true;
        }

        // We show on the word label the whole word which the user was currently attempting.
        wordLbl.Text = word;
        hintLbl.Text = hint;

        skipBtn.Enabled = false;
        pauseBtn.Enabled = false;
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
            timeLbl.Text = seconds.ToString();

            if (Global.EasyBool == true) {
                if (seconds == 56) {  foreach (Button b in btnList) { b.Enabled = true; } }
                else if (seconds == 53) { addScoreLbl.Visible = false; }
            }
            else if (Global.NormBool == true) {
                if (seconds == 41) { foreach (Button b in btnList) { b.Enabled = true; } }
                else if (seconds == 38) { addScoreLbl.Visible = false; }
            }
            else if (Global.HardBool == true) {
                if (seconds == 26) { foreach (Button b in btnList) { b.Enabled = true; } }
                else if (seconds == 23) { addScoreLbl.Visible = false; }
            }

        }
        // Else if seconds is not over (aka is 0) this block of instructions goes off.
        else {
            timeMsgLbl.Visible = false;
            score = 0;
            // Because time has ran out for the current hidden word, we decrement maxLives by 1.

            if (!skipClick) {
                maxLives--;
                wrongWord++;
                currentCount++;
                // We then call updateScreen to update for the above visuals.
            }

            // Because we want to move onto the next random word, we also need to remove the current item from here.
            // Note that it uses if statement check that there are still elements in the dictionary.
            if (splitFile.Count > 0) {
                splitFile.Remove(splitFile.Keys.ElementAt(randomIndex));
            }

            // Tiny if else statement block to check whether lives is over 0, if it is we generate a new word.
            // Otherwise we end the game and call finishScreen.
            if ((splitFile.Count() == 0) || (maxLives == 0)) {
                gameOver = true;
                // Here we disable every letter button in the game.
                foreach (Button b in btnList) { b.Enabled = false; }
                finishScreen();
            }
            else { setup(); }
        }
    }

    // Clicking on the Restart button is pretty much a carbon copy of calling page load again.
    protected void newBtn_Click(object sender, EventArgs e)
    {
        // Fail safe we clear the dictionary just incase there was remaining items from last game.
        splitFile.Clear();
        // Again we assign to the splitFile dictionary the words we read from the file and split them.
        splitFile = readFile.Select(l => l.Split('_')).ToDictionary(a => a[0], a => a[1]);
        // Then again we order and shuffle the list, so the items order are not the same as before.
        splitFile = splitFile.OrderBy(x => new Random().Next()).ToDictionary(item => item.Key, item => item.Value);

        timeMsgLbl.Visible = false;
        fScoreLbl.Visible = false;
        addScoreLbl.Visible = false;
        timeLbl.Visible = true;
        timer.Enabled = true;

        // We set these values all to 0 as we want the game to be a fresh start.
        currentScore = 0;
        score = 0;
        multiplier = 0;
        wordChain = 0;
        correctCount = 0;
        wrongWord = 0;
        currentCount = 1;

        // We also set the max amount of lives a player has to half the maxWords amount.
        if (Global.EasyBool == true) {
            while (splitFile.Count() > 9) { splitFile.Remove(splitFile.Keys.Last()); }
            // Here we set the maxWords to max items found in the dictionary. 
            maxWords = splitFile.Count();
            startTimer(58);
            maxLives = Math.Ceiling((double)maxWords / 1.3);
        }
        else if (Global.NormBool == true) {
            while (splitFile.Count() > 14) { splitFile.Remove(splitFile.Keys.Last()); }
            // Here we set the maxWords to max items found in the dictionary. 
            maxWords = splitFile.Count();
            startTimer(43);
            maxLives = Math.Floor((double)maxWords / 1.6);
        }
        else if (Global.HardBool == true) {
            while (splitFile.Count() > 20) { splitFile.Remove(splitFile.Keys.Last()); }
            // Here we set the maxWords to max items found in the dictionary. 
            maxWords = splitFile.Count();
            startTimer(28);
            maxLives = Math.Floor((double)maxWords / 3.0);
        }

        // Here we update these labels to the default values required.
        // We use a string format this time for the count and wrong label.
        scoreLbl.Text = "SCORE: " + currentScore;
        chainNoLbl.Text = "x" + wordChain;
        livesNoLbl.Text = maxLives.ToString(); ;
        countNoLbl.Text = String.Format("{0} / {1}", correctCount, maxWords);
        wrongNoLbl.Text = String.Format("{0} / {1}", wrongWord, maxWords);
        pauseBtn.Text = String.Format("Pause ({0})", pauseCount);
        bonusNoLbl.Text = "(End Bonus: x0)";

        foreach (Button b in btnList) { b.Enabled = true; }

        // Finally we restart the game by recalling setup().
        setup();
    }

    // Clicking the Quit button at the any time will end the game and calls the finishScreen method.
    protected void skipBtn_Click(object sender, EventArgs e)
    {
        score = 0;

        if (splitFile.Count == 1) {
            wrongWord++;
            maxLives--;
            splitFile.Clear();
            gameOver = true;
            updateScreen();
        }
        else {
            skipClick = true;
            maxLives--;
            wrongWord++;
            currentCount++;
        }

        // Here we disable every letter button in the game.
        foreach (Button b in btnList) { b.Enabled = false; }
        finishScreen();
    }

    protected void backBtn_Click(object sender, EventArgs e)
    {
        Global.EasyBool = false;
        Global.NormBool = false;
        Global.HardBool = false;
        Global.GnlBool = false;
        Global.PrgBool = false;
        Global.VgmBool = false;
        Response.Redirect("Default.aspx");
    }
 
    protected void pauseBtn_Click(object sender, EventArgs e)
    {
        pauseBtn.Enabled = false;
        pauseBtn.Visible = false;
        playBtn.Visible = true;
        playBtn.Enabled = true;

        pauseCount--;
        pauseBtn.Text = String.Format("Pause ({0})", pauseCount);
        foreach (Button b in btnList) { b.Enabled = false; }
        timer.Enabled = false;       
    }

    protected void playBtn_Click(object sender, EventArgs e)
    {
        playBtn.Visible = false;
        playBtn.Enabled = false;
        pauseBtn.Visible = true;

        if (pauseCount != 0) { pauseBtn.Enabled = true; }
        foreach (Button b in btnList) { b.Enabled = true; }

        foreach (Button btn in btnList) {
            foreach (Button b in usedBtn) {
                if (b.Text == btn.Text) { btn.Enabled = false; }
            }
        }

        timer.Enabled = true;
    }
}