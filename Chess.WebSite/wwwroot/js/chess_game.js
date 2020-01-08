"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chessMatchHub").build();

// //Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceivePosition", function (position, user) {
    var li = document.createElement("li");
    li.textContent = position + " " + user;
    document.getElementById("movesList").appendChild(li);
    // game.move(move);
    // board.position(game.fen());

});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// function sendMove (event) {
//     let position = document.getElementById("fen").innerHTML;
//     let user = document.getElementById("name").value;
    
    
//     board.position(position);
//     connection.invoke("SendPosition", position, user).catch(function (err) {
//         return console.error(err.toString());
//     });
//     event.preventDefault();
// };











var board = Chessboard('myBoard');

var board = null
var game = new Chess()
var $status = $('#status')
var $fen = $('#fen')
var $pgn = $('#pgn')

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
    let position = document.getElementById("fen").innerHTML;
    let user = document.getElementById("name").value;
    connection.invoke("SendPosition", position, user).catch(function (err) {
    return console.error(err.toString());
  })}

  updateStatus()
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
  $fen.html(game.fen())
  $pgn.html(game.pgn())
}

var config = {
  pieceTheme: '/lib/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
  draggable: true,
  position: 'start',
  onDragStart: onDragStart,
  onDrop: onDrop,
  onSnapEnd: onSnapEnd
}
board = Chessboard('myBoard', config)

updateStatus()