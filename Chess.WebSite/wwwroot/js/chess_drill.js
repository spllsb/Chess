//dodac mierzenie czasu 
//po ukonczeniu, komunikat :
//ile czasu zajelo
//punkty za wykonanie
//jaka byl skutecznosc innych graczy



//po ukonczeniu zapis do bazy wynikwo 
//mozliwosc uruchomienia kolejnej losowej gry



"use strict";

const activateClassConst = "#move-history .active_move";
const movesListClassConst = "#move-history span";


var squareToHighlight = null;
var colorToHighlight = null;


var whiteSquareGreyColour = '#a9a9a9'


var blackSquareGreyColour = '#696969'
var incorrectMoveSquareColor = '255, 0, 51'
var correctMoveSquareColor = '127, 255, 0'
var hintSquareColour = '50, 168, 153'
var normalSquareColour = '170, 170, 170';
var normalSquareRgbaValue = '255,0,51';

function colourSquare (square, rgbValue) {
    var $square = $('#chessboard .square-' + square)
    //if square is dark is
    // let transparentClass = $square.hasClass('black-3c85d') ? 'transparent':''
    let opacity = $square.hasClass('black-3c85d') ? 1 : 0.5;
    let colorRgba = 'rgba('+rgbValue+','+opacity+')';
    $square.css('background', colorRgba);
    // $square.css('backgroundColor',transparentClass);
    

}

  
function removeColorChessboard(){
    $('#chessboard .square-55d63').css('background', '');
}

const chess_mod = {
    LIVE_GAME: 'live_game',
    DRILLS: 'drills',
    GAME_REPEAT:'game_repeat'
};

const chess_searching_typ = {
    RANDOM: 'random',
    PARTICULAR: 'particular'
};



// Configure chessGame
//Chess layout
const pieceTheme_path = "/lib/chessboardjs/img/chesspieces/wikipedia/{piece}.png";

const chessGameDurationInfoHTML = document.getElementById("clock-top") ? document.getElementById("clock-top").dataset.timer : null;
const container_for_list_move_html_name = "game__controls__wrapper";

//HTML DOM
var move_list_content;
var moves;
var chessGameId;

//control game
// const chess_mod_selected = chess_mod.DRILLS;
// const chess_mod_selected = chess_mod.LIVE_GAME;
const chess_searching_typ_selected = chess_searching_typ.RANDOM;

var move_index = -1;

const config = {
    pieceTheme: pieceTheme_path,
    draggable: isDraggable(),
    onSnapEnd: onSnapEnd,
    onDrop: onDrop,
    onDragStart : onDragStart,
    onMoveEnd: onMoveEnd,
    position: 'start',
    moveSpeed: 'slow'

}

//connect to signalRd
var connection;
//chess game
var chess_game;

var drill_chess;
var pgn_chess;

var board;

var correct_moves;


document.addEventListener('DOMContentLoaded', () => {
    switch(chess_mod_selected) {
        case chess_mod.DRILLS:
            
            initDrill();
            initHTML();
            initTimer();
            break;
    }
    $(window).resize(board.resize);
})

var initDrill = function (){
    pgn_chess = new Chess();
    pgn_chess.load_pgn(pgn);
    drill_chess = new Chess(pgn_chess.header().FEN);
    correct_moves = pgn_chess.history({ verbose: true })
    config.position = drill_chess.fen(); 
    config.orientation = drill_chess.turn() == 'w' ? 'white' : 'black';
    board = new Chessboard('chessboard', config);
    move_index = -1;
}
var initHTML = function (){
    
    drillStartHTML.forEach(element => {
        element.classList.remove('d-none');
    });
    drillEndHTML.forEach(element => {
        element.classList.add('d-none');
    });
}


//#region chess function  

//#region chess move function  


function onSnapEnd() {
    board.position(drill_chess.fen());
    colourSquare(squareToHighlight, colorToHighlight);
}
//#endregion chess function move 

//#region chess configuration function  


//opcja false, ustawiona jest np w przypadku "zdjęć"
function isDraggable() {
    return true;
}
//#endregion  chess function configuration 
//#endregion chess function  

function onDragStart (source, piece, position, orientation) {
    // do not pick up pieces if the game is over
    if (drill_chess.game_over()) return false;
    if (piece.search(config.orientation.substr(0,1))) return false;
  
  }

function onDrop(source, target, orientation) {
    // see if the move is legal
    var move = drill_chess.move({
        from: source,
        to: target,
        promotion: 'q' // NOTE: always promote to a queen for example simplicity
    });
    // illegal move
    
    if (move === null) return 'snapback';

    drillMove(move);
    updateDrillStatus();
}

var drillComputerMove = function(){
    var newMoveIndex = move_index + 1;

    removeColorChessboard();

    colourSquare(correct_moves[newMoveIndex].from, normalSquareColour);
    drill_chess.move(correct_moves[newMoveIndex].san);

    squareToHighlight = correct_moves[newMoveIndex].to ;
    colorToHighlight = normalSquareColour ;
    
    board.position(drill_chess.fen());

    move_index = newMoveIndex;
}


var drillMoveIsCorrect = function (san, move_index)
{
    return san == correct_moves[move_index].san ? true : false;
}

var drillMove = function(move){
    removeColorChessboard();
    if (drillMoveIsCorrect(move.san, ++move_index))
    {
        squareToHighlight = move.to;
        colorToHighlight = correctMoveSquareColor;
        colourSquare(squareToHighlight, colorToHighlight);
        if(!drillIsComplete()){
            window.setTimeout(drillComputerMove, 200);
        }
    }
    else 
    {
        squareToHighlight = move.to;
        colorToHighlight = incorrectMoveSquareColor;
        colourSquare(squareToHighlight, colorToHighlight);
    }
}


var updateDrillStatus = function(){
    if (colorToHighlight == incorrectMoveSquareColor){
        endDrill();
    }
    else if (drillIsComplete())
    {
        endDrill();
    }
}

var endDrill = function(){
    stopTimer();
    drillSummaryTimeHTML.innerText = h1.textContent;

    drillStartHTML.forEach(element => {
        element.classList.add('d-none');
    });
    drillEndHTML.forEach(element => {
        element.classList.remove('d-none');
    });
}


var drillIsComplete = function(){
    return move_index == correct_moves.length-1 ? true : false;
}

function onMoveEnd(){
    colourSquare(squareToHighlight, colorToHighlight);
}


function convertSeconds(s){
    let min = Math.floor(s / 60);
    let sec = s % 60;
    let formattedNumber = ("0" + min).slice(-2);
    let formattedSecond = ("0" + sec).slice(-2);
    return formattedNumber + ':' + formattedSecond;
}
const drillSummaryTimeHTML = document.getElementById("drill_summary_time");
const drillOrientationMoveHTML = document.getElementById("drill_orientation_move");

const drillSummaryHtml = document.querySelector(".end__drill__summary");

const drillStartHTML = document.querySelectorAll(".drill_start");
const drillEndHTML = document.querySelectorAll(".drill_end");

const reloadDrillIcon = document.querySelector(".fa-undo");
const hintDrillIcon = document.querySelector(".fa-lightbulb");
const nextDrillIcon = document.querySelector(".fa-arrow-right");

const flipBoardIcon = document.querySelector(".fa-retweet");
const downloadPGNIcon = document.querySelector(".fa-download");

//do testow, bedzie to podpowiedz
const shareIcon = document.querySelector(".fa-share-alt");

const previousMoveIcon = document.querySelector(".fa-chevron-left");
const nextMoveIcon = document.querySelector(".fa-chevron-right");

reloadDrillIcon.addEventListener("click",function(){
    initHTML();
    initDrill();
});

hintDrillIcon.addEventListener("click",function(){
    var hint_index = move_index + 1;
    colourSquare(correct_moves[hint_index].from, hintSquareColour);
    colourSquare(correct_moves[hint_index].to, hintSquareColour);
});

nextDrillIcon.addEventListener("click",function(){
    
});








var h1 = document.getElementsByTagName('time')[0],
    seconds = 0, minutes = 0, hours = 0,
    t;

function add() {
    seconds++;
    if (seconds >= 60) {
        seconds = 0;
        minutes++;
        if (minutes >= 60) {
            minutes = 0;
            hours++;
        }
    }
    
    h1.textContent = (minutes ? (minutes > 9 ? minutes : "0" + minutes) : "00") + ":" + (seconds > 9 ? seconds : "0" + seconds);

    timer();
}
function timer() {
    t = setTimeout(add, 1000);
}

var initTimer = function (){
    timer();
}

var stopTimer = function(){
    clearTimeout(t);
}
// /* Start button */
// start.onclick = timer;

// /* Stop button */
// stop.onclick = function() {
//     clearTimeout(t);
// }

// /* Clear button */
// clear.onclick = function() {
//     h1.textContent = "00:00:00";
//     seconds = 0; minutes = 0; hours = 0;
// }