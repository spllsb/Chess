
class GameConfig{
    constructor(gameMode)
    {
        this.gameMode = gameMode;

        //opcje uzaleznic od paramertu gameMode (zrbic zwykłaego case)
        this.setOptions();
    }

    setOptions(){
        this.config = {
            pieceTheme: '../Chess.Frontend/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
            draggable: true,
            orientation: 'black',
            position:'start',
            onDragStart:null,
        }
    }

}


export default GameConfig;