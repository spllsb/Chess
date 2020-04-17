import ChessGame from './ChessGame.js';

window.addEventListener('load',() =>
{
    new ChessGame('#myBoard');
    console.log('OK');
});