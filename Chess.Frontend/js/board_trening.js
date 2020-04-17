"use strict";
var board = Chessboard('myBoard');

var game = new Chess()

var config = {
  pieceTheme: '../Chess.Frontend/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
  draggable: true,
  position: 'start',
}
board = Chessboard('myBoard', config)

