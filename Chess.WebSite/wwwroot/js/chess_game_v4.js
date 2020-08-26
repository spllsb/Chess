"use strict";

const activateClassConst = "#move-history .active_move";
const movesListClassConst = "#move-history span";


var whiteSquareGrey = '#a9a9a9'
var blackSquareGrey = '#696969'



var renderMoveHistory = function (moves) {
    var historyElement = $('#move-history').empty();
    historyElement.empty();

    let movesLength = moves.length;
    for (var i = 0; i < movesLength; i ++) {
        let new_element_list = createHTMLElement("span", moves[i]);   
        historyElement.append(new_element_list);
    }
    //add event for list move
    let movesHtml = document.querySelectorAll(movesListClassConst);
    movesHtml.forEach(move => {
        move.addEventListener('click', selectMove);
    })

    setActiveMove(movesHtml,movesLength-1);
};



var getRenderMoveFen = function (moves, index) {
    chess_game.copy_game.reset();
    for (var i = 0; i <= index; i++) {
        chess_game.copy_game.move(moves[i]);
    }
    return chess_game.copy_game.fen();
}

var selectMove = function (e) {
    var movesHtml = document.querySelectorAll(movesListClassConst);
    const moveArray = Array.from(movesHtml);
    const index = moveArray.indexOf(e.target);
    
    clearActiveMove(activateClassConst);
    setActiveMove(movesHtml, index);
    updateFENInChessboard(index);
    console.log(chess_game.currentIndex);
}


var updateFENInChessboard = function (index){
    chess_game.board.position(getRenderMoveFen(chess_game.game.history(), index));
}

var createHTMLElement = function(elementName, elementText){
    let new_element_list = document.createElement(elementName);
    new_element_list.innerHTML = elementText;
    return new_element_list;
}

var clearActiveMove = function(parent){
    var movesHtml = document.querySelectorAll(parent);
    [].forEach.call(movesHtml, function(el) {
        el.classList.remove("active_move");
    });
}
var setActiveMove = function (moves, index){
    moves[index].className = "active_move";
    chess_game.currentIndex = index;
}






function greySquare (square) {
    var $square = $('#chessboard .square-' + square)

    var background = whiteSquareGrey
    if ($square.hasClass('black-3c85d')) {
        background = blackSquareGrey
    }
    $square.css('background', background)
}

  
function removeColorChessboard(){
    $('#chessboard .square-55d63').css('background', '');
}

function colorChessboard(from, to){
    greySquare(from);
    greySquare(to);
}



  



// !! zmienne koniecznie musza byc malymi znakami
const chessboard_piece_color = {
    WHITE: 'white',
    BLACK: 'black'
};

const chess_mod = {
    LIVE_GAME: 'live_game',
    DRILLS: 'drills',
    GAME_REPEAT:'game_repeat'
};

const chess_searching_typ = {
    RANDOM: 'random',
    PARTICULAR: 'particular'
};


var currentPlayer;

// Configure chessGame
//Chess layout
const pieceTheme_path = "/lib/chessboardjs/img/chesspieces/wikipedia/{piece}.png";

//SignalR configuration
const chessHub_connect_URL = "/chessMatchHub";
let config_chessGameSignalR = 0;

const chessGameDurationInfoHTML = document.getElementById("clock-top") ? document.getElementById("clock-top").dataset.timer : null;
//HTML game tag


const currentUserHTML = document.getElementById("chess_my_name");
const opponentHTML = document.getElementById("chess_opponent_name");

const container_for_list_move_html_name = "game__controls__wrapper";
const new_move_design_class = "game__controls_move";
const active_move_class_name = "active";
const new_move_added_to_list_move_class_name = "div";

//HTML DOM
var move_list_content;
var moves;
var chessGameId;

//control game
// const chess_mod_selected = chess_mod.DRILLS;
// const chess_mod_selected = chess_mod.LIVE_GAME;
const chess_searching_typ_selected = chess_searching_typ.RANDOM;


const config = {
    pieceTheme: pieceTheme_path,
    draggable: isDraggable(),
    onSnapEnd: onSnapEnd,
    onDrop: onDrop,
    onDragStart : onDragStart,
    position: 'start' 
}

//connect to signalRd
var connection;
//chess game
var chess_game;


document.addEventListener('DOMContentLoaded', () => {
    switch(chess_mod_selected) {
        case chess_mod.DRILLS:
            config.position = getStartPositionFromHTML();
            config.orientation = chessboard_piece_color.WHITE;
            chess_game = new ChessGame('chessboard', config);
            chess_game.game.load(config.position);
            break;
        case chess_mod.LIVE_GAME:
            // chessGameId = document.getElementById("ChessGameId").textContent;
            switch(chess_searching_typ_selected){
                case chess_searching_typ.RANDOM:

                break;
                case chess_searching_typ.PARTICULAR:

                break;
            }
            initChessGameSignalR();
            chess_game = new ChessGame('chessboard', config);
            chess_game.game.load(config.position);
        break;

        case chess_mod.GAME_REPEAT:
            chess_game = new ChessGame('chessboard', config);
            chess_game.game.load_pgn(pgn);
            chess_game.board.position(chess_game.game.fen());
            renderMoveHistory(chess_game.game.history());
            break;
    }


    $(window).resize(chess_game.board.resize);
})


function getStartPositionFromHTML()
{
    const chess_fen_html = document.getElementById("StartPosition").value;
    const game_fen = chess_fen_html + ' b - - 1 19';
    return game_fen;
}

/**
 * A move 
 * @typedef {Object} Move
 * @property{number} id - Move Index
 * @property{string} fen - Fen
 * @property{string} display_move - Move display name
 */
/**
* @type {Move}
*/
class Move {
    constructor(index, fen, from, to, display_move, orientation) {
        this.index = index;
        this.fen = fen;
        this.from = from;
        this.to = to;
        this.display_move = display_move;
        this.orientation = orientation;
        
    }
    /**
     * Move 
     * @returns {string} - Display all information about variable
     */
    getInformation() {
        return 'Move: ' + this.index + ' Fen: ' + this.fen + 'From: ' + this.from + ' To: ' + this.to + ' Display_move ' + this.display_move + ' Orientation ' + this.orientation;
    }
}


class ChessGame {
    constructor(html_id_name, config){
        this.game = new Chess();
        this.copy_game = new Chess();
        this.board = Chessboard(html_id_name, config);
        this.moves_array = [];
        this.current_index = 0; 
    }
}

/**
 * A move 
 * @typedef {Object} Move
 * @property{number} id - Move Index
 * @property{string} fen - Fen
 * @property{string} display_move - Move display name
 */
/**
* @type {Player}
*/
class Player{
    constructor(playerName){
        this.playerName = playerName;
        // this.startOrientation = startOrientation;
        this.isYourMove = false;
        this.timeLeft = 0;
    }
}

function initChessGameSignalR (){
    config_chessGameSignalR = 1;

    connection = new signalR.HubConnectionBuilder()
        .withUrl(chessHub_connect_URL)
        .build();

    connection.start()
        .then( document.getElementById("chessboard").style.display = "")
        .catch(function (err) {
            return console.error(err.toString());
        }).then(function(){
            connection.invoke('SearchRandomChessGame', chessGameDurationInfoHTML).then(function(message){
                if (message == "Player found game"){
                    connection.invoke('SendCommunication',"No to zaczynajmy gre",sessionStorage.getItem("roomId"));
                }
            })
        });
    connection.on("ReceiveCommunication", function (communication){
        alert(communication);
    });

    connection.on("ReceiveRoom", function (roomId){
        sessionStorage.setItem("roomId", roomId);
    });

    connection.on("GetColorPiece", function(message){
        config.orientation = message;
        chess_game = new ChessGame('chessboard', config);
        chess_game.game.load(config.position);
    });
    
    connection.on("GetOpponent", function(message){
        updateOpponentInformationHTML(message);
    });
    connection.on("ReceivePosition", updatePositionSignalR);
}





function updatePositionSignalR (roomId, user, move) {
    console.log("Info z signalR: " + roomId + '  ' + user)
    chess_game.game.move(move);
    chess_game.board.position(chess_game.game.fen());
    removeColorChessboard();
    colorChessboard(move.from, move.to);
    renderMoveHistory(chess_game.game.history());
    manageTimer(user);
}

function manageTimer(user){
    if(currentUserHTML.textContent == user)
    {
        clock_my.stop();
        clock_opponent.start();
    }else
    {
        clock_opponent.stop();
        clock_my.start(); 
    }
}

function updateOpponentInformationHTML(userName){
    opponentHTML.textContent = userName;
}






//#region chess function  

//#region chess move function  


function onSnapEnd() {
    chess_game.board.position(chess_game.game.fen())
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
    if (chess_game.game.game_over()) return false;
    if (piece.search(config.orientation.substr(0,1))) return false;
  
  }

function onDrop(source, target, orientation) {
    // see if the move is legal
    var move = chess_game.game.move({
        from: source,
        to: target,
        promotion: 'q' // NOTE: always promote to a queen for example simplicity
    });
    // illegal move
    
    if (move === null) return 'snapback';

    
    if(config_chessGameSignalR)
    {
        let session_roomId = sessionStorage.getItem("roomId");
        connection.invoke("SendPosition", session_roomId, move)
        .then(function () {
            connection.invoke("SaveInformationAboutGame", session_roomId, getPGN(), chess_game.game.fen());
        })
        .catch(function (err) {
        return console.error(err.toString());})
    }
    else
    {
        renderMoveHistory(chess_game.game.history());
    }
    updateStatus();
}

function endGame(){
    connection.invoke("EndGame", getPGN())
        .catch(function (err) {
            return console.error(err.toString());})
}

function updateStatus () {
    var status = ''
  
    var moveColor = 'White'
    if (chess_game.game.turn() === 'b') {
      moveColor = 'Black'
    }
  
    // checkmate?
    if (chess_game.game.in_checkmate()) {
      status = 'Game over, ' + moveColor + ' is in checkmate.';
      endGame();
    }
  
    // draw?
    else if (chess_game.game.in_draw()) {
      status = 'Game over, drawn position';
      endGame();
    }
  
    // game still on
    else {
      status = moveColor + ' to move'
  
      // check?
      if (chess_game.game.in_check()) {
        status += ', ' + moveColor + ' is in check'
      }
    }
    console.log(status);
  }




function updateMoveInHTMLBoard(index) {
    chess_game.board.position(chess_game.moves_array[index].fen);
}


function convertSeconds(s){
    let min = Math.floor(s / 60);
    let sec = s % 60;
    let formattedNumber = ("0" + min).slice(-2);
    let formattedSecond = ("0" + sec).slice(-2);
    return formattedNumber + ':' + formattedSecond;
}

function preparePGN(){
    chess_game.game.header('Site', 'Szachy.pl');
    chess_game.game.header('Date', '2020.04.24');
    chess_game.game.header('Event', 'Player vs Player');
    chess_game.game.header('Round', '0');
    chess_game.game.header('White', 'Player1');
    chess_game.game.header('Black', 'Player10');
    chess_game.game.header('Result', '*');
    // chess_game.game.header('CurrentPosition', chess_game.moves_array[chess_game.current_active_index].fen);
}

function getPGN(){
    console.log("Printing PGN");
    preparePGN();
    // separator for windows "\r\n"
    // separator for linux "\n"
    return chess_game.game.pgn({ max_width: 5, newline_char: "\\n" });
}


const flipBoardIcon = document.querySelector(".fa-retweet");
const downloadPGNIcon = document.querySelector(".fa-download");
const shareIcon = document.querySelector(".fa-share-alt");

const previousMoveIcon = document.querySelector(".fa-chevron-left");
const nextMoveIcon = document.querySelector(".fa-chevron-right");

downloadPGNIcon.addEventListener("click",function(){
    document.getElementById("PGN").innerHTML = getPGN(); ;
});

shareIcon.addEventListener("click",function(){
    alert("shareIcon");
});

previousMoveIcon.addEventListener("click",function(){
    let movesHtml = document.querySelectorAll(movesListClassConst);
    let currentIndex = chess_game.currentIndex;
    let newIndex = currentIndex - 1 >= 0 ? currentIndex - 1 : 0;
    console.log("Previous index: " + newIndex);
    clearActiveMove(activateClassConst);
    setActiveMove(movesHtml, newIndex);
    updateFENInChessboard(newIndex);
});

nextMoveIcon.addEventListener("click",function(){
    let movesHtml = document.querySelectorAll(movesListClassConst);
    let currentIndex = chess_game.currentIndex;
    let newIndex = currentIndex + 1 < movesHtml.length ? currentIndex + 1 : currentIndex;
    console.log("Next index: " + newIndex);
    clearActiveMove(activateClassConst);
    setActiveMove(movesHtml, newIndex);
    updateFENInChessboard(newIndex);
});

flipBoardIcon.addEventListener("click",function(){
    console.log('Board orientation is: ' + chess_game.board.orientation());
    chess_game.board.flip();

    // change orientation information about player 
    let att = document.getElementById("layout__main");
    if (getComputedStyle(att).flexDirection == "column"){
        document.getElementById("layout__main").setAttribute("style", "flex-direction:column-reverse");
    }
    else{
        document.getElementById("layout__main").setAttribute("style", "flex-direction:column");
    }
});




var Stopwatch = function(elem, options) {
  
    var timer       = createTimer(),
        startValue = initStartValue(),
        offset,
        clock,
        interval;
    
    // default options
    options = options || {};
    options.delay = options.delay || 1;
   
    
    // initialize
    reset();
    


    // private functions
    function createTimer() {
      return document.createElement("span");
    }
    
    function initStartValue(){
        return elem.dataset.timer;
    }

    function start() {
      if (!interval) {
        offset   = Date.now();
        interval = setInterval(update, options.delay);
      }
    }
    
    function stop() {
      if (interval) {
        clearInterval(interval);
        interval = null;
      }
    }
    
    function reset() {
      clock = 0;
      render(0);
    }
    
    function update() {
      clock += delta();
      render();
    }
    
    function render() {
      elem.dataset.timer = convertSeconds(startValue - ((clock/1000).toFixed()));
    }
    
    function delta() {
      var now = Date.now(),
          d   = now - offset;
      
      offset = now;
      return d;
    }

    function manageState(){
        let state = interval;
        if(interval){
            stop();
        }
        else{
            start();
        }
    }
    
    // public API
    this.start  = start;
    this.stop   = stop;
    this.manageState  = manageState;

    this.reset  = reset;
  };
  const clock_opponent = new Stopwatch(document.getElementById("clock-top"));
  const clock_my = new Stopwatch(document.getElementById("clock-bottom"));

 

