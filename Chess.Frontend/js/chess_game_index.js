// import ChessGame from './ChessGame.js';

// window.addEventListener('load',() =>
// {
//     var chessGame = new ChessGame('#myBoard');
//     console.log('OK');
// });




"use strict";


//#region  


//#endregion  



const flipBoardIcon = document.querySelector(".fa-retweet");
const downloadIcon = document.querySelector(".fa-download");
const shareIcon = document.querySelector(".fa-share-alt");

const previousIcon = document.querySelector(".fa-chevron-left");
const nextIcon = document.querySelector(".fa-chevron-right");

//#region variables declaration
var game = new Chess('r1k4r/p2nb1p1/2b4p/1p1n1p2/2PP4/3Q1NB1/1P3PPP/R5K1 w - - 1 19');
var board_id_name = 'chessboard';
var board = Chessboard(board_id_name);
//#endregion  veriable declaration





function updateListMoves(color, san, fen)
{
  //jezli kolor jest biały -> dodajemy nowy wiersz
    let move_san = "<span class='vertical__move__list__element__move col-5 vertical__move__list__element__move__clickable move__text__selected'>" + san + "</span>";
    let move_fen = "<span class='vertical__move__list__element__fen'>" + fen.split(" ",1) + "</span>";
    let move = move_san + move_fen;
    var aa = document.querySelectorAll('.vertical__move__list__element__move__clickable.move__text__selected');
    [].forEach.call(aa, function(el) {
        var zz = el.classList;
        var zz = el.id;
        el.classList.remove("move__text__selected");
    
    });
    
    //jezli czarny dodajemy do 
    if(color=="w")
    {
        let move_number  = "<div class='vertical__move__list__element__move col-2'><span>1.</span></div>";
        let move_white = move;
        let move_black = "<span class='vertical__move__list__element__move col-5' id='vertical__move__list__element__timestamp'></span>";
        document.getElementById("vertical__move__list__component").innerHTML  += "<div class='vertical__move__list__element'>" + move_number + move_white + move_black + "</div>";
        vertical__move__list__element__move__clickable
    }
    else
    {
        document.getElementById('vertical__move__list__element__timestamp').innerHTML = move ;
        document.getElementById('vertical__move__list__element__timestamp').removeAttribute("id");
    }
};




//#region chess function  

//#region chess move function  
function onDrop(source, target) {
    // see if the move is legal
    var move = game.move({
        from: source,
        to: target,
        promotion: 'q' // NOTE: always promote to a queen for example simplicity
    });
    // illegal move
    if (move === null) return 'snapback';


    console.log(move.color + " " + move.san  + " " + game.fen());
    updateListMoves('b','b3','sdasda');

};

function onSnapEnd() {
    board.position(game.fen())
}
//#endregion chess function move 

//#region chess configuration function  
// losowanie kto jest jakim kolorem ? 
function getOrientation() {
    return 'black';
}

//opcja false, ustawiona jest np w przypadku "zdjęć"
function isDraggable() {
    return true;
}
//#endregion  chess function configuration 


//#endregion chess function  






    var config = {
        // pieceTheme: '/lib/chessboardjs/img/chesspieces/wikipedia/{piece}.png',

        pieceTheme: '../Chess.Frontend/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
        draggable: isDraggable(),
        // orientation: getOrientation(),
        onSnapEnd: onSnapEnd,
        onDrop: onDrop,
        position: 'r1k4r/p2nb1p1/2b4p/1p1n1p2/2PP4/3Q1NB1/1P3PPP/R5K1'
    };

    board = Chessboard(board_id_name, config);

    $(window).resize(board.resize);











    //#region event 
    
    flipBoardIcon.addEventListener("click",function(){
        console.log('Board orientation is: ' + board.orientation());
        board.flip();
    });

    downloadIcon.addEventListener("click",function(){
        alert("downloadIcon");
    });

    shareIcon.addEventListener("click",function(){
        alert("shareIcon");
    });
    


    previousIcon.addEventListener("click",function(){
        alert("previousIcon");
    });

    nextIcon.addEventListener("click",function(){
        alert("nextIcon");
    });
    //#endregion


