"use strict";

// !! zmienne koniecznie musza byc malymi znakami
const chessboard_piece_color = {
    WHITE: 'white',
    BLACK: 'black'
};

const chess_mod = {
    LIVE_GAME: 'live_game',
    DRILLS: 'drills'
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
            config.orientation = getOrientationMoveFromHTML();
            break;
        case chess_mod.LIVE_GAME:
            chessGameId = document.getElementById("ChessGameId").textContent;
            switch(chess_searching_typ_selected){
                case chess_searching_typ.RANDOM:
                break;
                case chess_searching_typ.PARTICULAR:
                break;
            }
            initChessGameSignalR();
        break;
    }

    move_list_content = document.querySelector('.'+container_for_list_move_html_name);
    moves = document.querySelectorAll('.'+container_for_list_move_html_name+" "+new_move_added_to_list_move_class_name);
    moves.forEach(move => {
        move.addEventListener('click', clickSelectMoveToDisplay);
    })
    console.log('Start position: ' + config.position);
    chess_game = new ChessGame('chessboard', config);
    chess_game.game.load(config.position);
    $(window).resize(chess_game.board.resize);
})

function getOrientationMoveFromHTML()
{
    const chess_orientation = document.getElementById("OrientationMove").value;
    return chess_orientation;
}

function getStartPositionFromHTML()
{
    const game_fen = generateFen();
    return game_fen;
}

function generateFen()
{
    const chess_fen_html = document.getElementById("StartPosition").value;
    const chess_orientation = document.getElementById("OrientationMove").value;
    const game_fen = chess_fen_html + ' ' + chess_orientation.substr(0,1) + ' - - 1 19';
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
    constructor(index, fen, display_move, orientation) {
        this.index = index;
        this.fen = fen;
        this.display_move = display_move;
        this.orientation = orientation;
    }
    /**
     * Move 
     * @returns {string} - Display all information about variable
     */
    getInformation() {
        return 'Move: ' + this.index + ' Fen: ' + this.fen + ' Display_move ' + this.display_move + ' Orientation ' + this.orientation;
    }
}


class ChessGame {
    constructor(html_id_name, config){
        this.game = new Chess();
        this.board = Chessboard(html_id_name, config);
        this.moves_array = [];
        this.current_active_index = -1; 
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
    console.log(chess_game.game.pgn());
  //   updateStatus();
    addNewItemToMoveListHTML(move);
    manageTimer(user);
}

function manageTimer(user){
    if(sessionStorage.getItem("playerName") == user)
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

function addNewItemToMoveListHTML(move){
    //remove active class
    clearClassFromDiv(active_move_class_name);
    //create new element from html with active class
    move_list_content.appendChild(creatChessMoveElementHTML(move.san));
    moves = document.querySelectorAll('.'+container_for_list_move_html_name+" "+new_move_added_to_list_move_class_name);
    chess_game.current_active_index = chess_game.moves_array.length;
    chess_game.moves_array.push(new Move(chess_game.moves_array.length, chess_game.game.fen(), move.san, move.color));
    console.log("Current index: " + chess_game.current_active_index);
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
    if (chess_game.game.game_over()) return false
  
    // only pick up pieces for White
    if (piece.search(config.orientation.substr(0,1))) return false
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
        connection.invoke("SendPosition", sessionStorage.getItem("roomId"), sessionStorage.getItem("playerName"), move).catch(function (err) {
        return console.error(err.toString());})
    }
    else
    {
        addNewItemToMoveListHTML(move);
    }
}


//list element
function clickSelectMoveToDisplay(e) {
    const moveArray = Array.from(moves);
    const index = moveArray.indexOf(e.target);
    clearClassFromDiv(active_move_class_name);
    updateActiveMoveFromHTMLBoard(index);
    updateMoveInHTMLBoard(index);
}



function creatChessMoveElementHTML(textInnerElement){
    let new_element_list = document.createElement(new_move_added_to_list_move_class_name);
    new_element_list.addEventListener('click', clickSelectMoveToDisplay);
    new_element_list.className = active_move_class_name;
    new_element_list.classList.add(new_move_design_class);
    new_element_list.innerHTML = textInnerElement;
    return new_element_list;
}


function clearClassFromDiv(class_name) {
    let div_with_move_active_class = document.querySelectorAll('.' + class_name);
    [].forEach.call(div_with_move_active_class, function (el) {
        el.classList.remove(class_name);
    });
}

function updateActiveMoveFromHTMLBoard(index){
    console.log("Previous index: " + chess_game.current_active_index);
    console.log('Selected index ' + index + ' move from moves array');
    console.log(chess_game.moves_array[index].getInformation());
    moves[index].classList.add(active_move_class_name);
    chess_game.current_active_index = index;
    console.log("Current index: " + chess_game.current_active_index);
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


const flipBoardIcon = document.querySelector(".fa-retweet");
const downloadPGNIcon = document.querySelector(".fa-download");
const shareIcon = document.querySelector(".fa-share-alt");

const previousMoveIcon = document.querySelector(".fa-chevron-left");
const nextMoveIcon = document.querySelector(".fa-chevron-right");

downloadPGNIcon.addEventListener("click",function(){
    console.log("Printing PGN");
    chess_game.game.header('Site', 'Szachy.pl');
    chess_game.game.header('Date', '2020.04.24');
    chess_game.game.header('Event', 'Player vs Player');
    chess_game.game.header('Round', '0');
    chess_game.game.header('White', 'Player1');
    chess_game.game.header('Black', 'Player10');
    chess_game.game.header('Result', '*');
    chess_game.game.header('CurrentPosition', chess_game.moves_array[chess_game.current_active_index].fen);
    document.getElementById("PGN").innerHTML = chess_game.game.pgn({ max_width: 5, newline_char: '<br />' });
});
shareIcon.addEventListener("click",function(){
    alert("shareIcon");
});
previousMoveIcon.addEventListener("click",function(){
    let newIndex = chess_game.current_active_index - 1;
    try{
        clearClassFromDiv(active_move_class_name);
        updateActiveMoveFromHTMLBoard(newIndex);
    } catch (e) {
        chess_game.board.position("start");
        chess_game.current_active_index = -1;
        return;
    }
    console.log("Previous index: " + chess_game.current_active_index);
    updateMoveInHTMLBoard(newIndex);
});
nextMoveIcon.addEventListener("click",function(){
    let newIndex = chess_game.current_active_index + 1;
    try{
        clearClassFromDiv(active_move_class_name);
        updateActiveMoveFromHTMLBoard(newIndex);
    } catch (e) {
        return;
    }
    updateMoveInHTMLBoard(newIndex);
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







 

