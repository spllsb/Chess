

// https://jsfiddle.net/lhartikk/m5q6fgtb/1/

"use strict";

const activateClassConst = "#move-history .active_move";
const movesListClassConst = "#move-history span";

var board,
    game = new Chess(),
    copy_game = new Chess();
var currentIndex;

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

var clearActiveMove = function(parent){
    var movesHtml = document.querySelectorAll(parent);
    [].forEach.call(movesHtml, function(el) {
        el.classList.remove("active_move");
    });
}

var setActiveMove = function (moves, index){
    moves[index].className = "active_move";
    currentIndex = index;
}

var getRenderMoveFen = function (moves, index) {
    copy_game.reset();
    for (var i = 0; i <= index; i++) {
        copy_game.move(moves[i]);
    }
    return copy_game.fen();
}

var createHTMLElement = function(elementName, elementText){
    let new_element_list = document.createElement(elementName);
    new_element_list.innerHTML = elementText;
    return new_element_list;
}


var selectMove = function (e) {
    var movesHtml = document.querySelectorAll(movesListClassConst);
    const moveArray = Array.from(movesHtml);
    const index = moveArray.indexOf(e.target);
    
    clearActiveMove(activateClassConst);
    board.position(getRenderMoveFen(game.history(), index));
    setActiveMove(movesHtml, index);
    console.log(currentIndex);
}


var onDrop = function (source, target) {

    var move = game.move({
        from: source,
        to: target,
        promotion: 'q'
    });

    removeGreySquares();
    if (move === null) {
        return 'snapback';
    }

    renderMoveHistory(game.history());
};

var onSnapEnd = function () {
    board.position(game.fen());
};

var onMouseoverSquare = function(square, piece) {
    var moves = game.moves({
        square: square,
        verbose: true
    });

    if (moves.length === 0) return;

    greySquare(square);

    for (var i = 0; i < moves.length; i++) {
        greySquare(moves[i].to);
    }
};

var onMouseoutSquare = function(square, piece) {
    removeGreySquares();
};

var removeGreySquares = function() {
    $('#board .square-55d63').css('background', '');
};

var greySquare = function(square) {
    var squareEl = $('#board .square-' + square);

    var background = '#a9a9a9';
    if (squareEl.hasClass('black-3c85d') === true) {
        background = '#696969';
    }

    squareEl.css('background', background);
};

var cfg = {
    pieceTheme: '../Chess.Frontend/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
    draggable: true,
    position: 'start',
    onDrop: onDrop,
    onMouseoutSquare: onMouseoutSquare,
    onMouseoverSquare: onMouseoverSquare,
    onSnapEnd: onSnapEnd
};
board = ChessBoard('board', cfg);



const previousMoveIcon = document.querySelector(".fa-chevron-left");
const nextMoveIcon = document.querySelector(".fa-chevron-right");


previousMoveIcon.addEventListener("click",function(){
    let movesHtml = document.querySelectorAll("#move-history span");
    let newIndex = currentIndex - 1 >= 0 ? currentIndex - 1 : 0;
    console.log("Previous index: " + newIndex);
    clearActiveMove(activateClassConst);
    setActiveMove(movesHtml, newIndex);
    board.position(getRenderMoveFen(game.history(), newIndex));
});
nextMoveIcon.addEventListener("click",function(){
    let movesHtml = document.querySelectorAll("#move-history span");
    let newIndex = currentIndex + 1 < movesHtml.length ? currentIndex + 1 : currentIndex;
    console.log("Next index: " + newIndex);
    clearActiveMove(activateClassConst);
    setActiveMove(movesHtml, newIndex);
    board.position(getRenderMoveFen(game.history(), newIndex));
});