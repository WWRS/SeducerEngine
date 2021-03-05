# SeducerEngine example project

I forked this from [https://github.com/WWRS/SeducerEngine](https://github.com/WWRS/SeducerEngine) which is a good base project, but the creator made no proper readme or explanation how to use it, and hasn't been updated in 2 years. So I'm attempting to provide more of an explanation to how it works, and what is required to add more Scenarios and choices. 

Demonstration of the game when running (assets are included in this repo):
[https://www.youtube.com/watch?v=E3i96P9AGAs](https://www.youtube.com/watch?v=E3i96P9AGAs)

## Preparation

You need Visual Studio 2019 to build the project.

Also if you don't have it already, you need to download the `.NET Framework 4.7.1` SDK from here: [https://dotnet.microsoft.com/download/visual-studio-sdks](https://dotnet.microsoft.com/download/visual-studio-sdks), be sure to download the 4.7.1 "Developer Pack".  

## Building and Running

Open the `WPF.csproj` file contained in the `WPF` folder. This should automatically open Visual Studio and load the project. 

Build the project by pressing F5, or clicking "Start" button at the top. 

## Engine Documentation

All Scenarios are contained within their own folders in the `Assets/Scenarios`. Every Scenario folder needs to be setup like this:

```
/Scenario1
   |  bg.png
   |  pre.avi
   |  scenario.txt
   |  1/
         | options.txt
         | w.avi
         | l.avi
         | e.avi
   |  2/
         | options.txt
         | w.avi
         | l.avi
         | e.avi
   | etc...
```
`bg.png` - the image to display on the Scenario select screen.

`pre.avi` - the first video file to play for this scenario. 

`scenario.txt` - a text file containing the scenario title and description. The first line of the txt file will be read as the title, the remaining lines are read as the decription.

`1/` - contains the choices and reaction videos for each

`1/options.txt` - the text to display as options. The first line is read as the Win (w) choice, the second line is the Lose (l) choice, and the third line is the End choice (ends the game).

`1/w.avi` - the video to play when the Win choice (w) is selected.

`1/l.avi` - the video to play when the Lose choice (l) is selected.

`1/e.avi` - the video to play when the End choice (e) is selected.

## Considerations

- After playing a Win video, the game advances to the next choice.
- After playing a Lose video, the game makes the user select another answer.
- After an End video, the game displays the Game Over screen.
- You win the game when there are no more numbered folders to progress to. If there are only 3 folders, after the 3rd choice is a W, the game displays the WinScreen. 
- I added my own LoseScreen, because there wasn't one. The number of the losing condition is `-1`


## Issues

- We can only make one of each Win, Lose, and End choice. The engine **assigns button types based on the line number in the options.txt file**. First line is read as the Win, second line is read as the Lose, and the third is the End. We need to be develop this further to include more types of responses, and to add the ability to make more than a single button of each type. 


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
