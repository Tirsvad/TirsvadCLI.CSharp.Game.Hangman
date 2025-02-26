[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<br />
<div align="center">
    <a href="https://github.com/TirsvadCLI/CSharp.Game.Hangman">
        <img src="logo/logo.png" alt="Logo" width="80" height="80">
    </a>
    <h3 align="center">Hangman Game</h3>
    <p align="center">
    A classic hangman game for windows console and linux console
    <br />
    <br />
    <!-- PROJECT SCREENSHOTS -->
    <br />
    <a href="https://github.com/TirsvadCLI/CSharp.Game.Hangman"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/TirsvadCLI/CSharp.Game.Hangman/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    ·
    <a href="https://github.com/TirsvadCLI/CSharp.Game.Hangman/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
    </p>
</div>

# Hangman

This is a simple hangman game that I created in CSharp. The game is played in the console and the user has to guess the word that the computer has chosen. The user has 6 lives and if they run out of lives, they lose the game. The user can also guess the whole word at once. If the user guesses the word correctly, they win the game.

It is in development and less playable yet.

## Table of Contents

- [About The Hangman](#about-the-hangman)
- [Getting Started](#getting-started)
    - [Prequisites](#prerequisites)
        -[Optional for changing the code](#optional-for-changing-the-code)
- [Run the game](#run-the-game)
    - [Alternativ using dot net command](#alternativ-using-dot-net-command)
- [Features](#features)
- [Roadmap](#roadmap)
- [Change Log](#change-log)
- [Folder Structure](#folder-structure)

## About The Hangman
Though the origins of the game are unknown it is believed to have originated in Victorian times, where it was a popular parlor game. The game is still played today, and it is a fun way to test your vocabulary and general knowledge.

## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.

- .NET 9.0
    ```
    https://dotnet.microsoft.com/download/dotnet/9.0
    ```

#### Optional for changing the code

- Visual Studio 2022
    ```
    https://visualstudio.microsoft.com/
    ```

### Run the game

#### Alternativ using dot net command

1. Clone the repo

    ```
    git clone git@github.com:TirsvadCLI/CSharp.Game.Hangman.git
    ```

2. Go to main folder in a console and enter the command 

    ```
    cd .\CSharp.Game.Hangman\
    dotnet run
    ```

## Features

- [x] Windows console support
- [x] Linux console support
- [x] Multi language support

## Roadmap

- [ ] Change game mode (easy, medium, hard)
- [ ] Change game type (animals, countries, etc.)
- [ ] Add more words
- [ ] Help
- [ ] Score system
- [ ] Save and load highscore
- [ ] World highscore online
- [ ] Multi language word list

## Change Log

## Folder Structure

CSharp.Game.Hangman/                # Root folder that contains the solution
|---Hangman/                        # Project folder
|   |---bin/                        # Contains the compiled files
|---images/                         # Contains images
|---logo/                           # Contains logo
|---documentation/                  # Contains documentation
|   |---doxygen/                    # Contains doxygen documentation


<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/TirsvadCLI/CSharp.Game.Hangman?style=for-the-badge
[contributors-url]: https://github.com/TirsvadCLI/CSharp.Game.Hangman/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/TirsvadCLI/CSharp.Game.Hangman?style=for-the-badge
[forks-url]: https://github.com/TirsvadCLI/CSharp.Game.Hangman/network/members
[stars-shield]: https://img.shields.io/github/stars/TirsvadCLI/CSharp.Game.Hangman?style=for-the-badge
[stars-url]: https://github.com/TirsvadCLI/CSharp.Game.Hangman/stargazers
[issues-shield]: https://img.shields.io/github/issues/TirsvadCLI/CSharp.Game.Hangman?style=for-the-badge
[issues-url]: https://github.com/TirsvadCLI/CSharp.Game.Hangman/issues
[license-shield]: https://img.shields.io/github/license/TirsvadCLI/CSharp.Game.Hangman?style=for-the-badge
[license-url]: https://github.com/TirsvadCLI/CSharp.Game.Hangman/blob/master/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/jens-tirsvad-nielsen-13b795b9/