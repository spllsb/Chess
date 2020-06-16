"use strict";

// !! zmienne koniecznie musza byc malymi znakami
const chessboard_color = {
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
const orientation = generateOrientation();


// Configure chessGame
//Chess layout
const pieceTheme_path = "/lib/chessboardjs/img/chesspieces/wikipedia/{piece}.png";

//SignalR configuration
const chessHub_connect_URL = "/chessMatchHub";
let config_chessGameSignalR = 0;


//HTML game tag
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
const chess_mod_selected = chess_mod.LIVE_GAME;
const chess_searching_typ_selected = chess_searching_typ.RANDOM;


const config = {
    pieceTheme: pieceTheme_path,
    draggable: isDraggable(),
    orientation: orientation,
    onSnapEnd: onSnapEnd,
    onDrop: onDrop,
    onDragStart : onDragStart,
    position: 'start' 
}

//connect to signalRd
var connection;
//chess game
var chess_game;
var playerColor = generateOrientation();


/**
 * Get the URL parameters
 * source: https://css-tricks.com/snippets/javascript/get-url-variables/
 * @param  {String} url The URL
 * @return {Object}     The URL parameters
 */
var getParams = function (url) {
    var params = {};
    var parser = document.createElement('a');
    parser.href = url;
    var query = parser.search.substring(1);
    var vars = query.split('&');
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split('=');
        params[pair[0]] = decodeURIComponent(pair[1]);
    }
    return params;
};

function initPlayer(){
    return new Player(generateName());
}

function updatePlayerSectionHTML(playerInfo, section_HTML_Id){
    document.getElementById(section_HTML_Id).textContent = playerInfo.playerName;
}


document.addEventListener('DOMContentLoaded', () => {
    currentPlayer = initPlayer();
    sessionStorage.setItem("playerId", generateGuidId());
    sessionStorage.setItem("playerName", currentPlayer.playerName);
    updatePlayerSectionHTML(sessionStorage.getItem("playerName"), "currentPlayer");

    
    switch(chess_mod_selected) {
        case chess_mod.DRILLS:
            config.position = getStartPositionFromHTML();
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




function capFirst(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

function getRandomInt(min, max) {
  	return Math.floor(Math.random() * (max - min)) + min;
}

function generateGuidId(){
    var guidId = ["c5b84954-d711-40fe-8bb2-8620b7e4b8be","ea83fbac-d08f-4109-8312-1c7924b716ad","0d78f06a-c8ea-4495-828d-6a2bae99d6b4","a5b84954-d711-40fe-8bb2-8620b7e4b8be"];
	var guid = guidId[getRandomInt(0, guidId.length + 1)];
	return guid;

}


function generateName(){
	var name1 = ["abandoned","able","absolute","adorable","adventurous","academic","acceptable","acclaimed","accomplished","accurate","aching","acidic","acrobatic","active","actual","adept","admirable","admired","adolescent","adorable","adored","advanced","afraid","affectionate","aged","aggravating","aggressive","agile","agitated","agonizing","agreeable","ajar","alarmed","alarming","alert","alienated","alive","all","altruistic","amazing","ambitious","ample","amused","amusing","anchored","ancient","angelic","angry","anguished","well-off","well-to-do","well-worn","wet","which","whimsical","whirlwind","whispered","white","whole","whopping","wicked","wide","wide-eyed","wiggly","wild","willing","wilted","winding","windy","winged","wiry","wise","witty","wobbly","woeful","wonderful","wooden","woozy","wordy","worldly","worn","worried","worrisome","worse","worst","worthless","worthwhile","worthy","wrathful","wretched","writhing","wrong","wry","yawning","yearly","yellow","yellowish","young","youthful","yummy","zany","zealous","zesty","zigzag","rocky"];

	var name2 = ["people","history","way","art","world","information","map","family","government","health","system","computer","meat","year","thanks","music","person","reading","method","data","food","understanding","theory","law","bird","literature","problem","software","control","knowledge","power","ability","economics","love","internet","television","science","library","nature","fact","product","idea","temperature","beat","burn","deposit","print","raise","sleep","somewhere","advance","consist","dark","double","upstairs","usual","abroad","brave","calm","concentrate","estimate","grand","male","mine","prompt","quiet","refuse","regret","reveal","rush","shake","shift","shine","steal","suck","surround","bear","brilliant","dare","dear","delay","drunk","female","hurry","inevitable","invite","kiss","neat","pop","punch","quit","reply","representative","resist","rip","rub","silly","smile","spell","stretch","stupid","tear","temporary","tomorrow","wake","wrap","yesterday","Thomas","Tom"];

	var name = capFirst(name1[getRandomInt(0, name1.length + 1)]) + ' ' + capFirst(name2[getRandomInt(0, name2.length + 1)]);
	return name;

}

function generateOrientation(){
    var color = ["white","black"];
    var orientation = color[getRandomInt(0, color.length)];
	return orientation;
}





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
        })
            .then(function(){
                connection.invoke('GetConnectionId').then(function(connectionId){
            })
            .then(function(){
                connection.invoke('SearchRandomChessGame', sessionStorage.getItem("playerId"))
                .then(function(message){
                    if (message == "Player found game"){
                        connection.invoke('SendCommunication',"No to zaczynajmy gre");
                    }
                })
            });
    });
    // connection.ser.JoinChessMatch(chessGameId);
    connection.on("ReceiveCommunication", function (communication){
        alert(communication);
    });

    connection.on("ReceiveRoom", function (roomId){
        sessionStorage.setItem("roomId", roomId);
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
    if (piece.search(orientation.substr(0,1))) return false
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




// var timeFull = 60;
// var timerInterval;

// var timeLeft = timeFull;
// var counter = 0;
// var timeIsOn = 0;
// var playerBlackTimerName = "timerBlackPlayer";


// function timeIt(){
//     document.getElementById("timer").innerHTML = convertSeconds(timeLeft);
//     counter ++;
//     timeLeft = timeFull - counter;
//     timerInterval = setTimeout(timeIt, 1000);
// }

// function startTimer(){
//     if (!timeIsOn) {
//         timeIsOn = 1;
//         timeIt();
//     }
// }

// function stopTimer() {
//     clearInterval(timerInterval);
//     timeIsOn = 0;
// }


// https://jsbin.com/IgaXEVI/167/edit?html,js,output


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

 

