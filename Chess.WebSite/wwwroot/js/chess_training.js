"use strict";



// losowanie kto jest jakim kolorem ? 

function getOrientation() {
    return 'black';
}

//opcja false, ustawiona jest np w przypadku zdjęć
function isDraggable() {
    return false;
}



$(document).ready(function () {
    var board_id_name = 'drills__board_Syrup';
    var board = Chessboard(board_id_name);
    var game = new Chess();

    var config = {
        pieceTheme: '/lib/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
        draggable: isDraggable(),
        orientation: getOrientation()
    };
    board = Chessboard(board_id_name, config);
    board.position('r1bqkbnr/pppp1ppp/2n5/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R');
    $(window).resize(board.resize);
}
) 


