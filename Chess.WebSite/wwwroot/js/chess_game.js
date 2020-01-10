"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chessMatchHub").build();


connection.on("ReceivePosition", function (posit, user, move) {
  game.move(move);
  board.position(game.fen());
  updateListMoves(move.color,move.san);
  console.log(game.pgn());
  updateStatus();
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


var board = Chessboard('myBoard');
var board = null
var game = new Chess()
var $status = $('#status')


function onDragStart (source, piece, position, orientation) {
  // do not pick up pieces if the game is over
  if (game.game_over()) return false

  // only pick up pieces for the side to move
  if ((game.turn() === 'w' && piece.search(/^b/) !== -1) ||
      (game.turn() === 'b' && piece.search(/^w/) !== -1)) {
    return false
  }
}

function onDrop (source, target) {
  // see if the move is legal
  var move = game.move({
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
}

// update the board position after the piece snap
// for castling, en passant, pawn promotion
function onSnapEnd () {
  board.position(game.fen())
}

function updateStatus () {
  var status = ''

  var moveColor = 'White'
  if (game.turn() === 'b') {
    moveColor = 'Black'
  }

  // checkmate?
  if (game.in_checkmate()) {
    status = 'Game over, ' + moveColor + ' is in checkmate.'
  }

  // draw?
  else if (game.in_draw()) {
    status = 'Game over, drawn position'
  }

  // game still on
  else {
    status = moveColor + ' to move'

    // check?
    if (game.in_check()) {
      status += ', ' + moveColor + ' is in check'
    }
  }

  $status.html(status)
}
var config = {
    pieceTheme: '/lib/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
    draggable: true,
    position: 'start',
    onDragStart: onDragStart,
    onDrop: onDrop,
    onSnapEnd: onSnapEnd
  }
  board = Chessboard('myBoard', config);


function updateListMoves(color, san)
{
  //jezli kolor jest biały -> dodajemy nowy wiersz

  //jezli czarny dodajemy do 
  if(color=="w")
  {
    document.getElementById("vertical__move__list__component").innerHTML  += "<div class='vertical__move__list__element d-flex'><div class='vertical__move__list__element__move col-2'><span>1.</span></div><div class='vertical__move__list__element__move col-5'><span class='vertical__move__list__element__move__clickable'>" + san + "</span></div><div class='vertical__move__list__element__move col-5' id='vertical__move__list__element__timestamp'></div>";
  }
  else
  {
    document.getElementById('vertical__move__list__element__timestamp').innerHTML += "<span class='vertical__move__list__element__move__clickable'>" + san + "</span>" ;
    document.getElementById('vertical__move__list__element__timestamp').removeAttribute("id");
  }
}