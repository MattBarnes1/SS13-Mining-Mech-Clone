using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public static class DiceRoller
{
    public static int RollDice(String myString)
    {
        Debug.Write("Request to roll: " + myString);
        int FirstNumber = 0;
        int SecondNumber = 0;
        int Bonus = 0;
        parseRollAndDiceAndBonus(myString, ref FirstNumber, ref SecondNumber, ref Bonus);
        return RollDice(FirstNumber, SecondNumber, Bonus);
    }

    public static int RollDice(int FirstNumber, int SecondNumber, int Bonus)
    {
        Random myRandom = new Random();
        int TotalRolled = 0;
        for (int i = 0; i < FirstNumber; i++)
        {
            TotalRolled += (myRandom.Next(Math.Max(FirstNumber - 1, 0), SecondNumber) + Bonus);
        }
        Debug.Write("Total Rolled: " + TotalRolled);
        return TotalRolled;
    }

    public static void parseRollAndDice(string myString, ref int FirstNumber, ref int SecondNumber)
    {
        String myFirstInt = "";
        String mySecondInt = "";
        int CharacterCount = 0;
        while (Char.IsDigit(myString[CharacterCount]))
        {
            myFirstInt += myString[CharacterCount];
            CharacterCount++;
        }
        CharacterCount++;
        while (Char.IsDigit(myString[CharacterCount]))
        {
            mySecondInt += myString[CharacterCount];
            CharacterCount++;
        }
        FirstNumber = int.Parse(myFirstInt);
        SecondNumber = int.Parse(mySecondInt);

        Debug.Write(myString + " was converted into: " + FirstNumber + " and " + SecondNumber);
    }

    public static void parseRollAndDiceAndBonus(string myString, ref int numToRoll, ref int DieToRoll, ref int Bonus)
    {
        String myFirstInt = "";
        String mySecondInt = "";
        String myBonusInt = "";
        int CharacterCount = 0;
        while (Char.IsDigit(myString[CharacterCount]))
        {
            myFirstInt += myString[CharacterCount];
            CharacterCount++;
        }
        CharacterCount++;
        while (myString.Length > CharacterCount && Char.IsDigit(myString[CharacterCount]))
        {
            mySecondInt += myString[CharacterCount];
            CharacterCount++;
        }
        if (CharacterCount < myString.Length)
        {
            if (myString[CharacterCount] == '-')
            {
                myBonusInt += '-';
            }
            CharacterCount++;
            while (Char.IsDigit(myString[CharacterCount]))
            {
                myBonusInt += myString[CharacterCount];
                CharacterCount++;
            }
        }
        else
        {
            myBonusInt = "0";
        }


        numToRoll = int.Parse(myFirstInt);
        DieToRoll = int.Parse(mySecondInt);
        Bonus = int.Parse(myBonusInt);
        Debug.Write(myString + " was converted into: " + numToRoll + " and " + DieToRoll + " with a bonus of: " + Bonus);
    }
}

