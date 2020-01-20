"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chessMatchHub").build();


connection.on("ReceivePosition", function (posit, user, move) {
  game.move(move);
  board.position(game.fen());
  updateListMoves(move.color,move.san,game.fen());
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

//usunac innerHTML += -> wolne 
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
}

$( document ).ready(function() {
$('.vertical__move__list__element__move').click(
  function(event){
    var aaa = document.getElementsByClassName('vertical__move__list__element__move__clickable');
    var aaaa = aaa[1];
    var qqqq = document.querySelector(".vertical__move__list__element__move__clickable");
    var aaaaaa = aaaa.next;
    var aaaaa = aaaa.nextElementSibling;
    var qq = this.querySelector('vertical__move__list__element__move__clickable');
    var xxaaa = this.nextSibling;
    var xxxxx = $(event).next();
    var xxxxax = $(event).next;
    var xxxx = this.innerHTML;
    var xxxxx = xxxx.innerHTML;
    console.log($(event.target).text());
    console.log(aaa.nextElementSibling);

    board.position(this.querySelector(".vertical__move__list__element__fen").innerHTML);
    // board = Chessboard('myBoard',fen);
});
});

//#region Manage chessboard buttons 
function Resign()
{
  alert("sadasd");
}
function Draw()
{
  // zrobic signalR z propozycja na remis
  game.in_draw();
}
function Begin(){
  board.position("start");
};
function Current()
{
  board.position(game.fen());
}
function Back()
{
  var aa = game.history({ verbose: true });
  let aaaa = document.querySelector('.move__text__selected');
  let aaaaa = aaaa.parentNode;
  let aaaaaa = aaaaa.previousSibling;
  let aaaaaaa = aaaaaa.previousSibling;
  let aaaaaaaa = aaaaaaa.previousSibling;
  // let aaa = aaaa.previousElementSibling;
  console.log(aaaaa.innerHTML);
  
  // let aa = document.getElementsByClassName('vertical__move__list__element__fen');
  // console.log(aa[1].innerHTML);
  // console.log(this.querySelector(".vertical__move__list__element__fen").innerHTML);
}

function Next()
{
}
//#endregion
