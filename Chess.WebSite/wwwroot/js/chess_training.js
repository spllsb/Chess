"use strict";


//#region  


//#endregion  

//#region variables declaration
var game = new Chess('r1k4r/p2nb1p1/2b4p/1p1n1p2/2PP4/3Q1NB1/1P3PPP/R5K1 b - - 1 19');
var board_id_name = 'chessboard';
var board = Chessboard(board_id_name);
//#endregion  veriable declaration




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
    if (move === null) return 'snapback'
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






$(document).ready(function () {
    var config = {
        pieceTheme: '/lib/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
        draggable: isDraggable(),
        orientation: getOrientation(),
        onSnapEnd: onSnapEnd,
        onDrop: onDrop,
        position: 'r1k4r/p2nb1p1/2b4p/1p1n1p2/2PP4/3Q1NB1/1P3PPP/R5K1'
    };

    board = Chessboard(board_id_name, config);

    $(window).resize(board.resize);
}
)


