﻿/*
 * Made by Martin Krendželák
 * 28-05-2022
 * 00:59
 */

using System.Drawing;

//solids
#pragma warning disable CA1416
var one = Image.FromFile("../../../images/1.png");
var two = Image.FromFile("../../../images/2.png");
var three = Image.FromFile("../../../images/3.png");
var four = Image.FromFile("../../../images/4.png");
var five = Image.FromFile("../../../images/5.png");
var six = Image.FromFile("../../../images/6.png");
var seven = Image.FromFile("../../../images/7.png");

//eight-ball
var eight = Image.FromFile("../../../images/8.png");

//stripes
var nine = Image.FromFile("../../../images/9.png");
var ten = Image.FromFile("../../../images/10.png");
var eleven = Image.FromFile("../../../images/11.png");
var twelve = Image.FromFile("../../../images/12.png");
var thirteen = Image.FromFile("../../../images/13.png");
var fourteen = Image.FromFile("../../../images/14.png");
var fifteen = Image.FromFile("../../../images/15.png");

var firstPlayerNameFile = File.CreateText("../../../outputs/Player1.txt");
var secondPlayerNameFile = File.CreateText("../../../outputs/Player2.txt");

var firstPlayerScore = 0;
var secondPlayerScore = 0;
var firstPlayerScoreFile = File.CreateText("../../../outputs/Player1Score.txt");
using (var sw = firstPlayerScoreFile) sw.Write(firstPlayerScore);

var secondPlayerScoreFile = File.CreateText("../../../outputs/Player2Score.txt");
using (var sw = secondPlayerScoreFile) sw.Write(secondPlayerScore);

var solids = new List<Image>
{
    one, two, three, four, five, six, seven
};

var stripes = new List<Image>
{
    nine, ten, eleven, twelve, thirteen, fourteen, fifteen
};

void GenerateImages(bool leftPlayerHasSolids)
{
    #region Solids

    var maxSolidsHeight = solids.Select(x => x.Height).Max();
    var totalWidthOfSolidsBitMap = solids.Select(x => x.Width).Sum();
    var solidsImage = new Bitmap(totalWidthOfSolidsBitMap, maxSolidsHeight);
    using var solidsG = Graphics.FromImage(solidsImage);

    var totalSolidsWidth = 0;
    foreach (var img in solids)
    {
        solidsG.DrawImage(img, totalSolidsWidth, 0);
        totalSolidsWidth += img.Width;
    }
    
    #endregion

    #region Stripes

    var maxStripesHeight = stripes.Select(x => x.Height).Max();
    var totalWidthOfStripesBitMap = stripes.Select(x => x.Width).Sum();
    var stripesImage = new Bitmap(totalWidthOfStripesBitMap, maxStripesHeight);
    using var stripesG = Graphics.FromImage(stripesImage);

    var totalWidth = 0;
    foreach (var img in stripes)
    {
        stripesG.DrawImage(img, totalWidth, 0);
        totalWidth += img.Width;
    }
    
    #endregion

    if (leftPlayerHasSolids)
    {
        solidsImage.Save("../../../outputs/left.png");
        stripesImage.Save("../../../outputs/right.png");
    }
    else
    {
        stripesImage.Save("../../../outputs/left.png");
        solidsImage.Save("../../../outputs/right.png");
    }
}

Console.Write("Enter name of the first player: ");
var nameOfFirstPlayer = Console.ReadLine();
Console.Write("Enter name of the second player: ");
var nameOfSecondPlayer = Console.ReadLine();

using (var sw = firstPlayerNameFile) sw.Write(nameOfFirstPlayer);
using (var sw = secondPlayerNameFile) sw.Write(nameOfSecondPlayer);

Console.WriteLine($"Does {nameOfFirstPlayer} have solid balls?(y/n)");
var firstPlayerHasSolidBallsInput = Console.ReadLine();
var firstPlayerHasSolidBalls = firstPlayerHasSolidBallsInput == "y";
var isGameFinished = false;
GenerateImages(firstPlayerHasSolidBalls);


while (true)
{
    Game();
}
void Game()
{
    while (true)
    {
        Console.Write("Number of potted ball: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out var intInp))
        {
            switch (intInp)
            {
                case 1:
                    solids.Remove(one);
                    break;
                case 2:
                    solids.Remove(two);
                    break;
                case 3:
                    solids.Remove(three);
                    break;
                case 4:
                    solids.Remove(four);
                    break;
                case 5:
                    solids.Remove(five);
                    break;
                case 6:
                    solids.Remove(six);
                    break;
                case 7:
                    solids.Remove(seven);
                    break;
                case 8:
                    if(solids.Count == 0) solids.Remove(eight);
                    if(stripes.Count == 0) stripes.Remove(eight);
                    isGameFinished = true;
                    break;
                case 9:
                    stripes.Remove(nine);
                    break;
                case 10:
                    stripes.Remove(ten);
                    break;
                case 11:
                    stripes.Remove(eleven);
                    break;
                case 12:
                    stripes.Remove(twelve);
                    break;
                case 13:
                    stripes.Remove(thirteen);
                    break;
                case 14:
                    stripes.Remove(fourteen);
                    break;
                case 15:
                    stripes.Remove(fifteen);
                    break;
                default:
                    Console.WriteLine("Wrong ball number, type numbers from 1 to 15\n");
                    break;
            }

            if (isGameFinished)
            {
                GenerateImages(firstPlayerHasSolidBalls);
                Console.Write("Did first player win this leg?(y/n) ");
                var firstPlayerWonLeg = Console.ReadLine();
                if (firstPlayerWonLeg == "y")
                {
                    firstPlayerScore++;
                    firstPlayerScoreFile = File.CreateText("../../../outputs/Player1Score.txt");
                    using var sw = firstPlayerScoreFile;
                    sw.Write(firstPlayerScore);
                }
                else
                {
                    secondPlayerScore++;
                    secondPlayerScoreFile = File.CreateText("../../../outputs/Player2Score.txt");
                    using var sw = secondPlayerScoreFile;
                    sw.Write(secondPlayerScore);
                }
                    
                Restart();
                isGameFinished = false;
                break;
            }
            
            if(stripes.Count == 0) stripes.Add(eight);
            if(solids.Count == 0) solids.Add(eight);

            GenerateImages(firstPlayerHasSolidBalls);
        }
        else
        {
            if (input == "restart")
            {
                Restart();
            }
            else
            {
                Console.WriteLine("Wrong ball number, type numbers from 1 to 15");
            }
        }
    }
}

void Restart()
{
    Console.WriteLine("Restarting game...");
    Thread.Sleep(2000);

    Console.Clear();
    Console.WriteLine($"Does {nameOfFirstPlayer} have solid balls?(y/n) ");
    firstPlayerHasSolidBallsInput = Console.ReadLine();
    firstPlayerHasSolidBalls = firstPlayerHasSolidBallsInput == "y";
    solids = new List<Image>
    {
        one, two, three, four, five, six, seven
    };
    stripes = new List<Image>
    {
        nine, ten, eleven, twelve, thirteen, fourteen, fifteen
    };
    
    GenerateImages(firstPlayerHasSolidBalls);
}