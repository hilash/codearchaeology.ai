#include <stdio.h>


#define N 15
#define ROW 1
#define COL 2
#define MAIN_DIAG 3
#define SUB_DIAG 4
#define FREE_SQUARE '-'


/* initialize board - read input */
void initBoard(char [N][N], int);

/* count the number of squares of the current player in the given row/column/diagonal */
int count(char board[N][N], int n, char player, int item, int which); /* which : ROW, COL, MAIN_DIAG, SUB_DIAG */

/* finds the first free place for the current player in the given row/column/diagonal.
 * return: 1 if found (and (*row, *col) are the next free place
 *         else  0 (and *row=*col=-1) */
int firstFreePlace(char board[N][N], int n, int item, int which, int *row, int *col); /* which : ROW, COL, MAIN_DIAG, SUB_DIAG */

/* calculates the next move. first phase: check if the is no-enemy row/col/diagonal, and put
 * it there (where the player has maximum squares taken). else, second phase: check the next 
 * row/column/diagonal with the maximum player's squares.
 * returns: 1 if found next place (and win),
 *          2 if found next place,
 *          0 didn't found next place */
int nextMove(char board[N][N], int n, char player, int *row, int *col); /* return : 1 if found and win. 2 - found but still not win. 0 - not found */

/* returns the player's enemy */
char getEnemy(char player); 

/* clacluate the next step phase - check where the player has the most quares (whitin row/column/diagonal,
 * and check if the row/column/diagonal item is zero).
 * returns: 1 if found next place (and win),
 *          2 if found next place,
 *          0 didn't found next place */
int calculatePhase(char board[N][N], int n, char player, int *row, int *col, 
        int search_rows[N], int search_cols[N], int search_main_diagonal, int search_sub_diagonal,
        int phase);

/* help function for 'calculatePhase'. calculates if the next step gives more benefit (more player's squares), 
 * and if so update the max counters. */
void updateMaxSquare(char board[N][N], int n, char player, int item, int which,
        int *max_num_squars, int *max_which, int *max_item, int *max_row, int *max_col); 

/* if item is non-zero return zero, if zero return one. help method for 'nextMove' */
int flipEnemyItemCount(int item);

/* display the current game board */
void displayBoard (char [N][N], int);

/* swap two integers */
void swap(int *a, int *b);


int main()
{
    int n;
    char player;
    char board[N][N];
    int player_steps_count = 0;
    int enemy_steps_count = 0;
    int last_result = 0;
    int result = 0;
    int row = -1;
    int col = -1;

    /* scan size of board, first player, and init board */
    scanf("%d\n", &n);
    scanf("%c\n", &player);
    initBoard(board, n);

    /* game loop */
    while (result != 1) {
        /*find the next step,  mark on board, and increment number of steps if needed */
        result = nextMove(board, n, player, &row, &col);
        if ((result == 1) || (result == 2)) {
            player_steps_count++;
            board[row][col]=player;
        }

        /* win! */
        if (result == 1) {
            printf("%c won in %d steps\n", player, player_steps_count); 
            displayBoard(board, n);
            break;
        }

        /* no win! */
        if ((last_result == 0) && (result == 0)){
            printf("no winner\n");
            displayBoard(board, n);
            break;
        }
        
        /* switch to enemey */
        last_result = result;
        player = getEnemy(player);
        swap(&player_steps_count, &enemy_steps_count);
    }

    return 0;
}


void initBoard(char board[N][N], int n)
{
    int i, j;
    for (i=0; i<n; i++){
        for (j=0; j<n; j++){
            scanf("%*[\r\n]"); /*ignore new line headeace */
            scanf("%c", &(board[i][j]));
        }
    }
}

/* count the number of player squres in row/column/diagonal/sub-diagonal */
int count(char board[N][N], int n, char player, int item, int which) /* which : ROW, COL, MAIN_DIAG, SUB_DIAG */
{
    int num_squars = 0;
    int i = 0;

    switch (which){
        case ROW:
            for (i=0; i<n; i++){
                if (board[item][i]==player){
                    num_squars++;
                }
            }
            break;
        case COL:
            for (i=0; i<n; i++){
                if (board[i][item]==player){
                    num_squars++;
                }
            }
            break;
        case MAIN_DIAG:
            for (i=0; i<n; i++){
                if (board[i][i]==player){
                    num_squars++;
                }
            }
            break;
        case SUB_DIAG:
            for (i=0; i<n; i++){
                if (board[i][n-1-i]==player){
                    num_squars++;
                }
            }
    }
    return num_squars;
}

int firstFreePlace(char board[N][N], int n, int item, int which, int *row, int *col) /* which : ROW, COL, MAIN_DIAG, SUB_DIAG */
{
    int result = 0;
    int i = 0;
    *row = *col = -1;

     switch (which){
        case ROW:
            for (i=0; i<n; i++){
                if (board[item][i]==FREE_SQUARE){
                    *row = item;
                    *col = i;
                    result = 1;
                    break;
                }
            }
            break;
        case COL:
            for (i=0; i<n; i++){
                if (board[i][item]==FREE_SQUARE){
                    *row = i;
                    *col = item;
                    result = 1;
                    break;
                }
            }
            break;
        case MAIN_DIAG:
            for (i=0; i<n; i++){
                if (board[i][i]==FREE_SQUARE){
                    *row = *col = i;
                    result = 1;
                    break;
                }
            }
            break;
        case SUB_DIAG:
            for (i=0; i<n; i++){
                if (board[i][n-1-i]==FREE_SQUARE){
                    *row = i;
                    *col = n-1-i;
                    result = 1;
                    break;
                }
            }
    }
    return result;
}

int nextMove(char board[N][N], int n, char player, int *row, int *col) /* return : 1 if found and win. 2 - found but still not win. 0 - not found */
{
    int enemy_rows_count[N] = {0};
    int enemy_cols_count[N] = {0};
    int enemy_diagonal_count = 0;
    int enemy_sub_diagonal_count = 0;
    int i = 0;
    int result = 0;
    char enemy = getEnemy(player);

    /* detrmine how many enemy squars in each  rows/columns/diagonals */
    for(i=0; i<n; i++){
        enemy_rows_count[i] = count(board, n, enemy, i, ROW);
        enemy_cols_count[i] = count(board, n, enemy, i, COL);
    }
    enemy_diagonal_count = count(board, n, enemy, 0, MAIN_DIAG);
    enemy_sub_diagonal_count = count(board, n, enemy, 0, SUB_DIAG);

    /* first phase - search in rows/cloumn/diagonals with no enemy square whithin them.
     * take the row/column/diagonal with maximum of current player squars. */
    result = calculatePhase(board, n, player, row, col,
            enemy_rows_count, enemy_cols_count, enemy_diagonal_count, enemy_sub_diagonal_count, 1);

    /* second phase - search in the rest of the rows/columns/diagonals
     * take the row/column/diagonal with maximum of current player squars. */
    if (result == 0){
        /* go over the rows/columns/diagonals where the emeny have squares. flip the numbers */
        for(i=0; i<n; i++){
            enemy_rows_count[i] = flipEnemyItemCount(enemy_rows_count[i]);
            enemy_cols_count[i] = flipEnemyItemCount(enemy_cols_count[i]);
        }
        enemy_diagonal_count = flipEnemyItemCount(enemy_diagonal_count);
        enemy_sub_diagonal_count = flipEnemyItemCount(enemy_sub_diagonal_count);
        
        result = calculatePhase(board, n, player, row, col,
                enemy_rows_count, enemy_cols_count, enemy_diagonal_count, enemy_sub_diagonal_count, 2);
    }

    return result;
}

/* for the second phase, flip the counts of enemy squares (for none-zero to zero and vice verca)
 * for re-checking the remaining rows/cols/diags with enemy quares */ 
int flipEnemyItemCount(int item)
{
    if (item == 0){
        return 1;
    }
    return 0;
}
/* calculate the next avilable step (row/col/diagonal with max number of player squars,
 * from a given set of row/col/diagonal (indicated by zero value at row/col cell or search_*_diagonal).
 * return: 0 - didn't find square for next move (all squars in the given sets are full)
 *         1 - found square for next step, that yields victory
 *         2 - found square for next step, that doesn't yields victory
 *         *col = *row = -1 upon not finding next step(case 0), else the contains the next step. */
int calculatePhase(char board[N][N], int n, char player, int *row, int *col, 
        int search_rows[N], int search_cols[N], int search_main_diagonal, int search_sub_diagonal,
        int phase)
{
    int i=0;
    int result = 0; /* no win - board is full */
    int max_which = ROW;
    int max_item = 0;
    int max_num_squars = -1;
    int max_row = -1;
    int max_col = -1;

    /* first, find the row with the maximum number of player squres */
    for (i=0; i<n;i++){
        if (search_rows[i] == 0){ /*then search that row */ 
            updateMaxSquare(board, n, player, i, ROW,
                    &max_num_squars, &max_which, &max_item, &max_row, &max_col); 
        }
    }
     /* find the col with the maximum number of player squars */
    for (i=0; i<n;i++){
        if (search_cols[i] == 0){  
            updateMaxSquare(board, n, player, i, COL,
                    &max_num_squars, &max_which, &max_item, &max_row, &max_col); 
        } 
    }

    /* find the diagonal/sub diagobal with max number of player squars */
    if (search_main_diagonal == 0) {
        updateMaxSquare(board, n, player, 0, MAIN_DIAG,
                &max_num_squars, &max_which, &max_item, &max_row, &max_col); 
    }

    if (search_sub_diagonal == 0) {
        updateMaxSquare(board, n, player, 0, SUB_DIAG,
                &max_num_squars, &max_which, &max_item, &max_row, &max_col); 
    }

    /* check if the chosen square leads to victory. in phase 2 we can't win (since there are
     * enemy squares in the row/col/diagonal */
    if ((max_row != -1) && (max_col != -1)) {
        if ((phase == 1) && (n-1==count(board, n, player, max_item, max_which))){
            result = 1; /* find next step & win! */
        }
        else {
            result = 2; /* find next step */
        }
    }
    *row = max_row;
    *col = max_col;
    return result;
}


void updateMaxSquare(char board[N][N], int n, char player, int item, int which,
        int *max_num_squars, int *max_which, int *max_item, int *max_row, int *max_col) 
{
    int next_row = -1;
    int next_col = -1;
    int num_squars = 0;
    
    num_squars = count(board, n, player, item, which);
    if ((num_squars > *max_num_squars) && (1==firstFreePlace(board, n, item, which, &next_row, &next_col))){
        *max_which = which;
        *max_item = item;
        *max_num_squars = num_squars;
        *max_row = next_row;
        *max_col = next_col;
    }
}

char getEnemy(char player) /* get the player's enemy */
{
    if (player=='X')
        return 'Y';
    else if (player=='Y')
        return 'X';
    else return '-';
}

void displayBoard (char board[N][N], int n)
{
    int i = 0;
    int j = 0;

    for (i=0; i<n; i++){
        for (j=0; j<n; j++){
            if (j==n-1){
                printf("%c\n", board[i][j]);
            }
            else {
                printf("%c ", board[i][j]);
            }
        }
    }
}

void swap(int *a, int *b)
{
    int temp = *a;
    *a = *b;
    *b = temp;
}
