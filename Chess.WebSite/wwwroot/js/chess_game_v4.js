"use strict";

const  chessGameStatus = {
    CREATE:"create",
    IN_GAME:"in game",
    END_GAME:"end game"
  }
const chessGameEndCause = {
    CHECKMATE:"przez szach-mat",
    LEAVE:"przez opuszczenie gry",
    DRAW:"przez remis"
}

const activateClassConst = "#move-history .active_move";
const movesListClassConst = "#move-history .move";


var whiteSquareGrey = '#a9a9a9'
var blackSquareGrey = '#696969'

var chess_game_duration;


var renderMoveHistory = function (moves) {
    var historyElement = $('#move-history').empty();
    historyElement.empty();
    let move_tag;
    let movesLength = moves.length;
    for (var i = 0; i < movesLength; i ++) {
        if (i%2 === 0)
        {
            move_tag = createHTMLTag("div","d-flex");
            let half_move_number_tag = createHTMLTag("div",['p-3', 'flex-shrink-1', "move_number"]);
            half_move_number_tag.innerHTML = i/2 + 1;
            move_tag.append(half_move_number_tag);
        }
        let half_move_tag = createHTMLTag("div",['p-3', 'flex-grow-1', "move"]);
        half_move_tag.innerHTML = moves[i];
        move_tag.append(half_move_tag);
        historyElement.append(move_tag);
    }
    //add event for list move
    let movesHtml = document.querySelectorAll(movesListClassConst);
    movesHtml.forEach(move => {
        move.addEventListener('click', selectMove);
    })
    setActiveMove(movesHtml,movesLength-1);
    updateMoveHistoryScroll();
};

var getRenderMoveFen = function (moves, index) {
    chessGame.copy_game.reset();
    for (var i = 0; i <= index; i++) {
        chessGame.copy_game.move(moves[i]);
    }
    return chessGame.copy_game.fen();
}

var selectMove = function (e) {
    var movesHtml = document.querySelectorAll(movesListClassConst);
    const moveArray = Array.from(movesHtml);
    const index = moveArray.indexOf(e.target);
    
    clearActiveMove(activateClassConst);
    setActiveMove(movesHtml, index);
    updateFENInChessboard(index);
    console.log(chessGame.currentIndex);
}


var updateFENInChessboard = function (index){
    chessGame.board.position(getRenderMoveFen(chessGame.game.history(), index));
}

var createHTMLElement = function(elementName, elementText){
    let new_element_list = document.createElement(elementName);
    new_element_list.innerHTML = elementText;
    return new_element_list;
}

var createHTMLTag = function (element, class_list){
    let tag = document.createElement(element);
    typeof class_list === "string" ? tag.classList.add(class_list) : tag.classList.add(...class_list);
    return tag;
}

var clearActiveMove = function(parent){
    var movesHtml = document.querySelectorAll(parent);
    [].forEach.call(movesHtml, function(el) {
        el.classList.remove("active_move");
    });
}
var setActiveMove = function (moves, index){
    moves[index].classList.add("active_move");
    chessGame.currentIndex = index;
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



  
var updateMoveHistoryScroll = function(){
    let lastItemPosition = $('.move-history .active_move').offset().top;
    moveHistoryHTML.scrollTop = lastItemPosition;
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

//HTML game tag
//scroll 
const moveHistoryHTML =  document.getElementById('move-history');


const currentUserAvatarHTML = document.getElementById("chess_current_user_avatar");
const currentUserNameHTML = document.getElementById("chess_current_user_name");
const currentUserRatingHTML = document.getElementById("chess_current_user_rating");
const opponentNameHTML = document.getElementById("chess_opponent_name");
const opponentRatingHTML = document.getElementById("chess_opponent_rating");
const opponentAvatarHTML = document.getElementById("chess_opponent_avatar");

const informationGameHtml = document.getElementById("game-information-component");

const onlyRandomModElementHTML = document.querySelectorAll(".random_mod");

const container_for_list_move_html_name = "";


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
// const chess_searching_typ_selected = chess_searching_typ.RANDOM;


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
var chessGame;


document.addEventListener('DOMContentLoaded', () => {
    switch(chess_mod_selected) {
        case chess_mod.DRILLS:
            config.position = getStartPositionFromHTML();
            config.orientation = chessboard_piece_color.WHITE;
            chessGame = new ChessGame('chessboard', config);
            chessGame.game.load(config.position);
            break;
        case chess_mod.LIVE_GAME:
            config_chessGameSignalR = 1;

            connection = new signalR.HubConnectionBuilder()
                        .withUrl(chessHub_connect_URL)
                        .build();
            initChessGameSignalR();
            switch(chess_searching_typ_selected){
                case chess_searching_typ.RANDOM:
                    onlyRandomModElementHTML.forEach(element => {
                        element.classList.remove('d-none');
                    });
                break;
                case chess_searching_typ.PARTICULAR:
                    sessionStorage.setItem("roomId", "0d42ca40-8999-4b03-a375-f86c3e4f66ee");
                    connection.start()
                            .then( document.getElementById("chessboard").style.display = "")
                            .catch(function (err) {
                                return console.error(err.toString());
                            }).then(function(){
                                connection.invoke('AddUserToParticularChessGameRoom', sessionStorage.getItem("roomId"));
                            });
                break;
            }
            chessGame = new ChessGame('chessboard',config);
            chessGame.setGameDuration(chess_game_duration);
            chessGame.setStatus(chessGameStatus.IN_GAME);
            break;

        case chess_mod.GAME_REPEAT:
            config.draggable = false;
            chessGame = new ChessGame('chessboard', config);
            chessGame.game.load_pgn(pgn);
            chessGame.board.position(chessGame.game.fen());
            initHTMLGameRepeat();
            renderMoveHistory(chessGame.game.history());
            break;
    }


    $(window).resize(chessGame.board.resize);
})

function initSignalRRandomChess (chess_game_duration){
    connection.start()
        .then( function(){
            connection.invoke('InitChessGame', 
                            chess_game_duration);
        })
        .catch(function (err) {
            return console.error(err.toString());
        });
}


var initHTMLGameRepeat = function ()
{
    updateOpponentInformationHTML(chessGame.game.header().Black, chessGame.game.header().BlackElo);
    updateCurrentUserInformationHTML(chessGame.game.header().White, chessGame.game.header().WhiteElo);
}
// function getStartPositionFromHTML()
// {
//     const chess_fen_html = document.getElementById("StartPosition").value;
//     const game_fen = chess_fen_html + ' b - - 1 19';
//     return game_fen;
// }

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
        this.status = chessGameStatus.CREATE;
        this.endCause = "";
        this.gameDuration = "";
    }
    setGameDuration(gameDuration){
        this.gameDuration = gameDuration; 
    }
    getGameDuration(){
        return this.gameDuration; 
    }

    setStatus(status) {
        this.status = status;
    }
    getStatus(){
        return this.status;
    }
    setEndCause(endCause) {
        this.endCause = endCause;
    }
    getEndCause(){
        return this.endCause;
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
    constructor(username, ratingELO, avatarFileName, pieceColor){
        this.username = username;
        this.ratingELO = ratingELO;
        this.avatarFileName = avatarFileName;
        this.pieceColor = pieceColor;
        this.isYourMove = false;
        this.timeLeft = 0;
        this.result=0;
    }
}
var playerOpponent;
var player;

function initChessGameSignalR (){
    
    connection.on("ReceiveCommunication", function (communication){
        addGameInformationMessage(communication);
    });

    connection.on("ReceiveRoom", function (roomId){
        sessionStorage.setItem("roomId", roomId);
    });

    connection.on("GetNewRatingELO", function(newEloForWin, newEloForDraw, newEloForLose){
        let deltaWin = newEloForWin - player_rating_elo;
        let deltaWinDisplay = deltaWin > 0 ? "+" + deltaWin : deltaWin;
        let deltaDraw = newEloForDraw - player_rating_elo;
        let deltaDrawDisplay = deltaDraw > 0 ? "+" + deltaDraw : deltaDraw;
        let deltaLose = newEloForLose - player_rating_elo;
        let deltaLoseDisplay = deltaLose > 0 ? "+" + deltaLose : deltaLose;
        var message = "Wygrana " + parseInt(deltaWinDisplay) + " | Remis " + parseInt(deltaDrawDisplay) + " | Przegrana " + parseInt(deltaLoseDisplay); 
        addGameInformationMessage(message);
    });
    

    connection.on("GetColorPiece", function(message){
        config.orientation = message;
        player = new Player(currentUserNameHTML.textContent, currentUserRatingHTML.textContent, currentUserAvatarHTML.src, message);

        chessGame = new ChessGame('chessboard', config);
        chessGame.game.load(config.position);
    });
    
    connection.on("GetOpponentInformation", function(username, ratingELO, avatarFileName, pieceColor){
        updateOpponentInformationHTML(username, ratingELO, avatarFileName);
        let message_header = "<h5>Nowa gra</h5>";
        let message_body_player_bottom = "<b> " + player_name + "</b>" + "(" + player_rating_elo + ")";
        let message_body_player_top = "<b> " + username + "</b>" + "(" + ratingELO + ")";
        let message = message_body_player_bottom + " vs " + message_body_player_top; 
        addGameInformationMessage(message_header);
        addGameInformationMessage(message);

        playerOpponent = new Player(username, ratingELO, avatarFileName, pieceColor);
    });
    connection.on("ReceivePosition", updatePositionSignalR);

    connection.on("InitializeChessGame", initChessGame);
}

function displayWinner(message)
{
    alert(message);
}

function initChessGame(){
    config.onDrop = onDropSignalR;
    chessGame = new ChessGame('chessboard', config);
    chessGame.game.load(config.position);
}

function updatePositionSignalR (roomId, user, move) {
    chessGame.game.move(move);
    chessGame.board.position(chessGame.game.fen());
    removeColorChessboard();
    colorChessboard(move.from, move.to);
    renderMoveHistory(chessGame.game.history());
    manageTimer(user);
}

var addGameInformationMessage = function(message){
    var tag = createHTMLTag("div","game-message-component");
    tag.innerHTML = message;
    informationGameHtml.append(tag);
}

function updatePositionSignalR (roomId, user, move) {
    chessGame.game.move(move);
    chessGame.board.position(chessGame.game.fen());
    removeColorChessboard();
    colorChessboard(move.from, move.to);
    renderMoveHistory(chessGame.game.history());
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

function updateOpponentInformationHTML(userName, ratingELO, avatarFileName){
    opponentAvatarHTML.src = "/img/"+avatarFileName;
    opponentNameHTML.textContent = userName;
    opponentRatingHTML.textContent = ratingELO;
}
function updateCurrentUserInformationHTML(userName, ratingELO){
    currentUserNameHTML.textContent = userName;
    currentUserRatingHTML.textContent = ratingELO;
}



//#region chess function  

//#region chess move function  


function onSnapEnd() {
    chessGame.board.position(chessGame.game.fen())
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
    if (chessGame.game.game_over()) return false;
    if (piece.search(config.orientation.substr(0,1))) return false;
  
  }
  function onDrop(source, target, orientation) {
    // see if the move is legal
    var move = chessGame.game.move({
        from: source,
        to: target,
        promotion: 'q' // NOTE: always promote to a queen for example simplicity
    });
    // illegal move
    
    if (move === null) return 'snapback';
    renderMoveHistory(chessGame.game.history());
    updateStatus();
}

function onDropSignalR(source, target, orientation) {
    // see if the move is legal
    var move = chessGame.game.move({
        from: source,
        to: target,
        promotion: 'q' // NOTE: always promote to a queen for example simplicity
    });
    // illegal move
    
    if (move === null) return 'snapback';

    if(config_chessGameSignalR )
    {
        let session_roomId = sessionStorage.getItem("roomId");
        connection.invoke("SendPosition", session_roomId, move)
          .then(function () {
                connection.invoke("SaveInformationAboutGame", 
                                session_roomId, getPGN(), 
                                chessGame.game.fen());
            })
            .catch(function (err) {
                return console.error(err.toString());})
            }
    else
    {
        renderMoveHistory(chessGame.game.history());
    }
    updateStatus();
}

// Do celów dokumentacyjnych
// function onDrop(source, target, orientation) {
//     // see if the move is legal
//     var move = chessGame.game.move({
//         from: source,
//         to: target,
//         promotion: 'q' // NOTE: always promote to a queen for example simplicity
//     });
//     // illegal move
//     if (move === null) return 'snapback';
//     connection.invoke("SendPosition", session_roomId, move)
//               .then(function () {
//         connection.invoke("SaveInformationAboutGame", 
//                         session_roomId, getPGN(), 
//                         chessGame.game.fen());
//     })
//         .catch(function (err) {
//             return console.error(err.toString());
//         });
//     updateStatus();
// }

















function endGame(){
    connection.invoke("EndGame", getPGN())
        .catch(function (err) {
            return console.error(err.toString());})
    
    getPGNModal();

}



function updateStatus () {
    var moveColor = 'White'
    if (chessGame.game.turn() === 'b') {
      moveColor = 'Black';      
    }

    // checkmate?
    if (chessGame.game.in_checkmate()) {
      chessGame.setStatus(chessGameStatus.END_GAME);
      chessGame.setEndCause(chessGameEndCause.CHECKMATE);
      player.pieceColor.toUpperCase() === moveColor.toUpperCase() 
                                        ? playerOpponent.result = 1 
                                        : player.result = 1;
      endGame();
    }
    // draw?
    else if (chessGame.game.in_draw()) {
      chessGame.setStatus(chessGameStatus.END_GAME);
      chessGame.setEndCause(chessGameEndCause.DRAW);
      player.result = 0.5;
      playerOpponent.result = 0.5;
      endGame();
    }
    // game still on
    else {
      status = moveColor + ' w ruchu'
      // check?
      if (chessGame.game.in_check()) {
        status += ', ' + moveColor + ' jest szachowany'
      }
    }
  }




function updateMoveInHTMLBoard(index) {
    chessGame.board.position(chessGame.moves_array[index].fen);
}


function convertSeconds(s){
    let min = Math.floor(s / 60);
    let sec = s % 60;
    let formattedNumber = ("0" + min).slice(-2);
    let formattedSecond = ("0" + sec).slice(-2);
    return formattedNumber + ':' + formattedSecond;
}

function preparePGN(){
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();
    today = dd + '.' + mm + '.' + yyyy;

    const currentUserNameHTML = document.getElementById("chess_current_user_name");
    const currentUserRatingHTML = document.getElementById("chess_current_user_rating");
    const opponentNameHTML = document.getElementById("chess_opponent_name");
    const opponentRatingHTML = document.getElementById("chess_opponent_rating");
    const opponentAvatarHTML = document.getElementById("chess_opponent_avatar");
    

    chessGame.game.header('Site', 'Szachy.pl');
    chessGame.game.header('Date', today);
    chessGame.game.header('Event', 'Player vs Player');
    chessGame.game.header('Round', '0');
    chessGame.game.header('White', 'Player1');
    chessGame.game.header('Black', 'Player10');
    chessGame.game.header('FEN', chessGame.game.fen());
    chessGame.game.header('Result', '*');
    // chessGame.game.header('CurrentPosition', chessGame.moves_array[chessGame.current_active_index].fen);
}

   

function getPGN(){
    preparePGN();
    // separator for windows "\r\n"
    // separator for linux "\n"
    return chessGame.game.pgn({ max_width: 5, newline_char: "\r\n" });
}

const durationGameButton = document.getElementById("durationGameButton");

const flipBoardIcon = document.querySelector(".fa-retweet");
const downloadPGNIcon = document.querySelector(".fa-download");
const shareIcon = document.querySelector(".fa-share-alt");

const previousMoveIcon = document.querySelector(".fa-chevron-left");
const nextMoveIcon = document.querySelector(".fa-chevron-right");

downloadPGNIcon.addEventListener("click",function(){
    getPGNModal();
});

shareIcon.addEventListener("click",function(){
    alert("shareIcon");
});

previousMoveIcon.addEventListener("click",function(){
    let movesHtml = document.querySelectorAll(movesListClassConst);
    let currentIndex = chessGame.currentIndex;
    let newIndex = currentIndex - 1 >= 0 ? currentIndex - 1 : 0;
    console.log("Previous index: " + newIndex);
    clearActiveMove(activateClassConst);
    setActiveMove(movesHtml, newIndex);
    updateFENInChessboard(newIndex);
});

nextMoveIcon.addEventListener("click",function(){
    let movesHtml = document.querySelectorAll(movesListClassConst);
    let currentIndex = chessGame.currentIndex;
    let newIndex = currentIndex + 1 < movesHtml.length ? currentIndex + 1 : currentIndex;
    console.log("Next index: " + newIndex);
    clearActiveMove(activateClassConst);
    setActiveMove(movesHtml, newIndex);
    updateFENInChessboard(newIndex);
});

flipBoardIcon.addEventListener("click",function(){
    console.log('Board orientation is: ' + chessGame.board.orientation());
    chessGame.board.flip();

    // change orientation information about player 
    let att = document.getElementById("layout__main");
    if (getComputedStyle(att).flexDirection == "column"){
        document.getElementById("layout__main").setAttribute("style", "flex-direction:column-reverse");
    }
    else{
        document.getElementById("layout__main").setAttribute("style", "flex-direction:column");
    }
});
durationGameButton.addEventListener("click",function(){
    chess_game_duration = getDropdownValue("gameDurationDropdown");
    initSignalRRandomChess(parseInt(chess_game_duration));
    clock_top.dataset.timer = convertSeconds(chess_game_duration*60);
    clock_bottom.dataset.timer = convertSeconds(chess_game_duration*60);

    onlyRandomModElementHTML.forEach(element => {
        element.classList.add('d-none');
    });
});



var getDropdownValue = function(dropdownTagId){
    var e = document.getElementById(dropdownTagId);
    return e.options[e.selectedIndex].value;
}




const clock_top = document.getElementById("clock-top");
const clock_bottom = document.getElementById("clock-bottom");

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



function getPGNModal(){
    $("#PGNModal").modal();
    document.getElementById("fenInPgnModal").value = chessGame.game.fen();
    document.getElementById("pgnInPgnModal").value = getPGN();
}

function getPGNModal(){
    document.getElementById("liveGameModalCauseEnd").innerText = chessGame.getEndCause();

    document.getElementById("liveGameModalFirstPlayerAvatar").src = player.avatarFileName;
    document.getElementById("liveGameModalFirstPlayerName").innerText = player.username;

    document.getElementById("liveGameModalResult").innerText = player.result + " - " + playerOpponent.result;
    
    document.getElementById("liveGameModalSecondPlayerAvatar").src = "/img/" + playerOpponent.avatarFileName;
    document.getElementById("liveGameModalSecondPlayerName").innerText = playerOpponent.username;

    // document.getElementById("liveGameModalNewRating").innerText = player.ratingELO + ;
    document.getElementById("liveGameModalNextGameButton").innerText = "Zagraj nowe " + chessGame.getGameDuration() + " minuty";

    liveGameModalFirstPlayerAvatar
    $("#liveGameModal").modal();
}


