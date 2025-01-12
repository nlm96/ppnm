__Author:__


__Student number:__


__AU-mail:__



I have obtained 92 points from homework and 10 from exam.
According to the grading score, the result is:

score = 0.7*total_points_for_homeworks/10 + 0.3*points_for_exam_project
= 0.7 * 92/10 + 0.3 *10 = 9.44

------------------------------------------------
#Homework

 ======================================
| # | homework      | A | B | C |  Σ |
 ======================================
| 01 | LinEq         | 6 | 3 | 0 |  9  |
---------------------------------------
| 02 | EVD           | 6 | 3 | 0 |  9  |
---------------------------------------
| 03 | LeastSquares  | 6 | 3 | 1 | 10  |
---------------------------------------
| 04 | Splines       | 6 | 3 | 0 |  9  |
---------------------------------------
| 05 | ODE           | 6 | 3 | 1 | 10  |
---------------------------------------
| 06 | Integration   | 6 | 3 | 0 |  9  |
---------------------------------------
| 07 | Monte Carlo   | 6 | 3 | 0 |  9  |
---------------------------------------
| 08 | Roots         | 6 | 3 | 0 |  9  |
---------------------------------------
| 09 | Minimization  | 6 | 3 | 0 |  9  |
---------------------------------------
| 10 | Neural Network| 6 | 3 | 0 |  9  |
---------------------------------------
 ======================================
|                    total points: 92  |

-------------------------------------------------------------------------------


# Examination project 12 - Bare Bones Particle Swarm Optimization

__Author:__


__Student number:__


__AU-mail:__


# Bare Bones Particle Swarm Optimization Exam Project

## Project Overview

This project focuses on implementing the Bare Bones Particle Swarm Optimization (BBPSO) algorithm as described in the exam question. 
It uses the Irwin-Hall distribution as an approximation for the normal distribution, that are used in the algorithm to generate numbers between [0,1] drawn from a "normal distribution".
BBPSO is utilized to find the global minimum of a given function within a defined search space. The algorithm's behavior is tested and analyzed through various scenarios, and the results are visualized in plots.

The source code can be found in the code folder. Important results and tests are printed in out.txt folder. And lastly, graphs and gif is created in the plots folder.

## Implementation Details

- The BBPSO algorithm is implemented based on the provided exam question guidelines.
- A lambda function representing the function to be minimized is defined.
- The search space bounds are set for the algorithm.
- Parameters like maximum iterations, convergence threshold, and swarm size are explored to observe their effects on convergence.
- Tracking of particle movements is enabled, and the data is saved for visualization.
- Convergence tests are conducted by varying parameters and saving the results for analysis.

## Files and Plots

- `particle_movement.gif`: An animated GIF illustrating the movement of particles during the algorithm's execution.
- `maxIterations_convergence.svg`: Is trying to illustrate the convergence for varying maximum iterations. 
- `swarmSize_convergence`: Shows results for varying swarmSizes.

## Usage

- The code is executed by running the provided `Main()` function. The entire program generating everything can be executed by make in the parent folder (exam folder).
Note, that the code does take some time to run (2 minutes), because of the convergence tests where it runs through several thousands runs 
and  because of the creation of the gif. But the bare bones particle swarm optimization method is in generally very quick and effective on the individual runs on the functions I have tested.
- Results of the convergence tests are displayed in the terminal.
- Visualizations of particle movement and convergence results can be found in the plots folder and out.txt.

## Self-Evaluation

The exam question or the book, didn't state a part A,B,C or any detailed questions. Only a relatively vague statement, that I had to implement the bare bones algorithm.
I believe I have successfully implemented the BBPSO algorithm, tested it on multiple functions to find the global minimum and compared to the true analytical minimum.
I have conducted convergence tests and have also created an animation that effectively visualizes the particle movements, i.e. how the BBPSO algorithm works finding a minimum.
I have analyzed the algorithm's performance under different scenarios. Overall, I would evaluate my project as 10 out of 10.

10/10


