
"use strict";



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






var playerColor = "black";

document.addEventListener('DOMContentLoaded', () => {


    var connection = new signalR.HubConnectionBuilder().withUrl("/chessMatchHub").build();


    connection.on("ReceivePosition", function (roomId, user, move) {
        console.log("Info z signalR: " + roomId + '  ' + user)

      chess_game.game.move(move);
      chess_game.board.position(chess_game.game.fen());
    //   updateListMoves(move.color,move.san,game.fen());
      console.log(chess_game.game.pgn());
    //   updateStatus();

    //remove active class
    clearClassFromDiv(active_move_class_name);
    //create new element from html with active class
    move_list_content.appendChild(creatChessMoveElementHTML(move.san));
    moves = document.querySelectorAll('.game__controls__wrapper div');
    chess_game.current_active_index = chess_game.moves_array.length;
    
    chess_game.moves_array.push(new Move(chess_game.moves_array.length, chess_game.game.fen(), move.san, move.color));
    console.log("Current index: " + chess_game.current_active_index);
    });
    
    connection.start().then(function () {
    }).catch(function (err) {
        return console.error(err.toString());
    });
    

    const active_move_class_name = "active";
    const move_list_content = document.querySelector('.game__controls__wrapper');
    let moves = document.querySelectorAll('.game__controls__wrapper div');

    let config = {
        pieceTheme: '/lib/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
        draggable: isDraggable(),
        orientation: getOrientation(),
        onSnapEnd: onSnapEnd,
        onDrop: onDrop,
        position: 'start'
    }
    var chess_game = new ChessGame('chessboard', config);
    $(window).resize(chess_game.board.resize);



    moves.forEach(move => {
        move.addEventListener('click', clickSelectMoveToDisplay);
    })

    //list element
    function clickSelectMoveToDisplay(e) {
        const moveArray = Array.from(moves);
        const index = moveArray.indexOf(e.target);
        clearClassFromDiv(active_move_class_name);
        updateActiveMoveFromHTMLBoard(index);
        updateMoveInHTMLBoard(index);
    }


    //#region chess function  

    //#region chess move function  
    function onDrop(source, target, orientation) {
        // see if the move is legal
        var move = chess_game.game.move({
            from: source,
            to: target,
            promotion: 'q' // NOTE: always promote to a queen for example simplicity
        });
        // illegal move
        if (move === null) return 'snapback';
        else{
            connection.invoke("SendPosition", "test", "userTest", move).catch(function (err) {
            return console.error(err.toString());
        })}
    };

    function onSnapEnd() {
        chess_game.board.position(chess_game.game.fen())
    }
    //#endregion chess function move 

    //#region chess configuration function  
    // losowanie kto jest jakim kolorem ? 
    function getOrientation() {
        return playerColor;
    }

    //opcja false, ustawiona jest np w przypadku "zdjęć"
    function isDraggable() {
        return true;
    }
    //#endregion  chess function configuration 
    //#endregion chess function  




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

    /**
     * Move 
     * @type {class_name: string}
     */
    //remove all active move in move list
    function clearClassFromDiv(class_name) {
        let div_with_move_active_class = document.querySelectorAll('.' + class_name);
        [].forEach.call(div_with_move_active_class, function (el) {
            el.classList.remove(class_name);
        });
    }

    function creatChessMoveElementHTML(textInnerElement){
        let new_element_list = document.createElement('div');
        new_element_list.addEventListener('click', clickSelectMoveToDisplay);
        new_element_list.className = active_move_class_name;
        new_element_list.classList.add("game__controls_move");
        new_element_list.innerHTML = textInnerElement;
        return new_element_list;
    }
})



function convertSeconds(s){
    let min = Math.floor(s / 60);
    let sec = s % 60;
    let formattedNumber = ("0" + min).slice(-2);
    let formattedSecond = ("0" + sec).slice(-2);
    return formattedNumber + ':' + formattedSecond;
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
    constructor(playerName, startOrientation){
        this.playerName = playerName;
        this.startOrientation = startOrientation;
        this.isYourMove = false;
        this.timeLeft = 0;
    }
}

class Timer{
    constructor(fullTime, htmlName){
        this.fullTime = fullTime;
        this.htmlName = htmlName;
        this.counter = 0;
        this.isOn = false;
        this.timerInterval;
    }

    getAvailableTime(){
        return this.fullTime - this.counter;
    }

    stopTimer(){

    }


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




var Timer2 = new Timer($("#opponentTimer").data('timer'),"opponentTimer");
var Timer1 = new Timer($("#opponentTimer").data('timer'),"myTimer");
var timerTimeout;
var timerTimeout1;



function timeIt(t,timer){
    document.getElementById(timer.htmlName).innerHTML = convertSeconds(timer.getAvailableTime());
    timer.counter++;
    t = setTimeout(timeIt, 1000, t, timer);
}

function startTimer(t){
    let timer = Timer2;
    if (!timer.isOn) {
        timer.isOn = 1;
        timeIt(t,timer);
    }
}

function stopTimer(t) {
    let timer = Timer2;
    clearTimeout(t);
    timer.isOn = 0;
}



function startTimer1(t){
    let timer = Timer1;
    if (!timer.isOn) {
        timer.isOn = 1;
        timeIt(t,timer);
    }
}

function stopTimer1(t) {
    let timer = Timer1;
    clearInterval(t);
    timer.isOn = 0;
}



