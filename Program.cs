/*
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

var solids = new List<Image>
{
    one, two, three, four, five, six, seven, eight
};

var stripes = new List<Image>
{
    nine, ten, eleven, twelve, thirteen, fourteen, fifteen, eight
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
        solidsImage.Save("../../../images/left.png");
        stripesImage.Save("../../../images/right.png");
    }
    else
    {
        stripesImage.Save("../../../images/left.png");
        solidsImage.Save("../../../images/right.png");
    }
}

Console.WriteLine("Does first player have solid balls?( type 1 for yes, anything for no )");
var hasLeftPlayerSolidBalls = Console.ReadLine();
var isGameFinished = false;
GenerateImages(hasLeftPlayerSolidBalls == "1");


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
                    solids.Remove(eight);
                    stripes.Remove(eight);
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
                GenerateImages(hasLeftPlayerSolidBalls == "1");
                Restart();
                isGameFinished = false;
                break;
            }

            GenerateImages(hasLeftPlayerSolidBalls == "1");
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
    Console.WriteLine("Does first player have solid balls?( type 1 for yes, anything for no )");
    hasLeftPlayerSolidBalls = Console.ReadLine();
    solids = new List<Image>
    {
        one, two, three, four, five, six, seven, eight
    };
    stripes = new List<Image>
    {
        nine, ten, eleven, twelve, thirteen, fourteen, fifteen, eight
    };
    
    GenerateImages(hasLeftPlayerSolidBalls == "1");
}