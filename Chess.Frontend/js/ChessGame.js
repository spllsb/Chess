import GameConfig from './GameConfig.js';

// const { Chess } = require('./chessjs/chess.js');

class ChessGame {
    constructor(boardSelector) {
        this.boardSelector = boardSelector;

        const game = new Chess()
        this.setConfig();
        this.initBorder();

    }

    //opcja false, ustawiona jest np w przypadku zdjęć
    onDragStart(source, piece, position, orientation) {
        // do not pick up pieces if the game is over
        console.log(source);
    }

    onDrop(source, target) {
        // see if the move is legal
        console.log(this.board);
        // var move = this.game.move({
        //     from: source,
        //     to: target,
        //     promotion: 'q' // NOTE: always promote to a queen for example simplicity
        // });

        // // illegal move
        // if (move === null) return 'snapback';

        // updateStatus()
    }

    onSnapEnd() {
        console.log("onSnapEnd");
        // this.board.position(this.game.fen());
    }

    updateStatus() {
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
    }


    setConfig() {
        this.gameConfig = new GameConfig("saa");
        // this.gameConfig.config.orientation = 'black';
        this.gameConfig.config.onDragStart = this.onDragStart;
        this.gameConfig.config.onSnapEnd = this.onSnapEnd;
        this.gameConfig.config.onDrop = this.onDrop;
    }

    initBorder() {
        this.board = Chessboard(this.boardSelector, this.gameConfig.config);
    }

    initGame() {
        return new Chess();
        // this.game = new Chess();
    }
}

export default ChessGame;