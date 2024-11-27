# Lottery App Jude Wood

## Read this bit first

### Areas where I varied from /interpreted requirements
1. I am enabling the user to quit 
2. I am ending the program when either the player or all CPU players have insufficient funds
3. I interpreted `rounded to the nearest whole number` to be rounding down to the nearest cent
4. My console output is more verbose than the sample and I removed the company name as I am uploading to GitHub. I figured it would be easier to check with more output. For production I would match the sample and make all the console calls through to UI service 🙂

### Things that could be improved/extended with more time applied:
1. Program.cs could have app.builder type dependency injection. 
2. I have added a sample set of unit tests - they are not exhaustive due to time constraints
3. The code is auto-linted with VSCode extension that puts the opening `{` on the next line (annoying I know 😪 sorry). I am working on configuring this linter better. It also doesn't do nice things like auto remove unused usings and I might have missed some.
4. I haven't included logging, environment variables, launch.json etc
5. There are no try/catches since the only external input is the number of tickets and that is validated. 
6. Arguably prizes could be a separate service.

## Description

The user (Player 1) is be prompted via the console to purchase their desired number of tickets.
- The remaining participants are computer-generated players (CPU),
labelled sequentially as Player 2, Player 3, etc. Their ticket purchases are determined randomly by the system.

## Boundaries
The total number of players in the lottery game ranges
between 10 and 15.
- Each player is allowed to purchase between 1 and 10 tickets dependant on their balance.
- Each player starts with a balance of $10.00
- Each ticket costs $1.00
- The system ensures that no player can purchase more tickets than their balance allows

## Game play

The player takes part in multiple lottery draws until one of the following ends the program
- The player has insufficient funds to buy a ticket
- All the CPU players have insufficient funds to buy a ticket
- The player enters 'Q' or 'q' to force the program to end

The player is informed via the CPU how many tickets the CPU players purchased plus the prize distribution and updated balances.

## Prize distribution
For each lottery draw there will be the following prizes: 

- Grand Prize: A single ticket will win 50% of the total ticket revenue.
- Second Tier: 10% of the total number of tickets (rounded down to the nearest cent) will share 30% of the total ticket revenue equally.
- Third Tier: 20% of the total number of tickets (rounded to the nearest cent) will share 10% of the total ticket revenue equally.

