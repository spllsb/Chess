/**
 * A move 
 * @typedef {Object} Move
 * @property{number} id - Move Index
 * @property{string} fen - Fen
 * @property{string} display_move - Move display name
 */
/**
* @type {Move}
*/
class Move {
    constructor(index, fen, display_move, orientation) {
        this.index = index;
        this.fen = fen;
        this.display_move = display_move;
        this.orientation = orientation;
    };
    /**
     * Move 
     * @returns {string} - Display all information about variable
     */
    getInformation() {
        return 'Move: ' + this.index + ' Fen: ' + this.fen + ' Display_move ' + this.display_move + ' Orientation ' + this.orientation;
    }
}

class ChessGame {
    constructor(html_id_name, config){
        this.game = new Chess();
        this.board = Chessboard(html_id_name, config);
        this.moves_array = [];
        this.current_active_index = -1; 
    };
}

document.addEventListener('DOMContentLoaded', () => {
    const active_move_class_name = "active";
    const move_list_content = document.querySelector('.game__controls__wrapper');



    let moves = document.querySelectorAll('.game__controls__wrapper div');

    config = {
        pieceTheme: '../Chess.Frontend/chessboardjs/img/chesspieces/wikipedia/{piece}.png',
        draggable: isDraggable(),
        // orientation: getOrientation(),
        onSnapEnd: onSnapEnd,
        onDrop: onDrop,
        position: 'start'
    }

    var chess_game = new ChessGame('chessboard', config);

    //query selector 
    const playerDisplay = document.querySelector('#player');


    moves.forEach(move => {
        move.addEventListener('click', clickSelectMoveToDisplay);
    })

    //add list element
    function clickSelectMoveToDisplay(e) {
        const moveArray = Array.from(moves);
        const index = moveArray.indexOf(e.target);

        clearClassFromDiv(active_move_class_name);
        console.log('Clicked ' + index + ' move from moves array');
        console.log(chess_game.moves_array[index].getInformation());

        moves[index].classList.add(active_move_class_name);
        chess_game.board.position(chess_game.moves_array[index].fen);
    }

    /**
     * Move 
     * @type {class_name: string}
     */
    //remove all active move in move list
    function clearClassFromDiv(class_name) {
        let div_with_move_active_class = document.querySelectorAll('.' + class_name);
        [].forEach.call(div_with_move_active_class, function (el) {
            el.classList.remove(class_name);
        });
    }




    //#region chess function  

    //#region chess move function  
    function onDrop(source, target, orientation) {
        // see if the move is legal
        var move = chess_game.game.move({
            from: source,
            to: target,
            promotion: 'q' // NOTE: always promote to a queen for example simplicity
        });
        // illegal move
        if (move === null) return 'snapback';

        //remove active class
        clearClassFromDiv(active_move_class_name);
        //create new element from html with active class
        let new_element_list = document.createElement('div');
        new_element_list.addEventListener('click', clickSelectMoveToDisplay);
        new_element_list.className = active_move_class_name;
        move_list_content.appendChild(new_element_list);

        moves = document.querySelectorAll('.game__controls__wrapper div');
        chess_game.moves_array.push(new Move(chess_game.moves_array.length, chess_game.game.fen(), move.san, orientation));
    };

    function onSnapEnd() {
        chess_game.board.position(chess_game.game.fen())
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




    const flipBoardIcon = document.querySelector(".fa-retweet");
    const downloadIcon = document.querySelector(".fa-download");
    const shareIcon = document.querySelector(".fa-share-alt");
    
    const previousIcon = document.querySelector(".fa-chevron-left");
    const nextIcon = document.querySelector(".fa-chevron-right");
    
    downloadIcon.addEventListener("click",function(){
        alert("downloadIcon");
    });
    
    shareIcon.addEventListener("click",function(){
        alert("shareIcon");
    });
    
    previousIcon.addEventListener("click",function(){
        
        alert("previousIcon");
    });
    
    nextIcon.addEventListener("click",function(){
        alert("nextIcon");
    });
    flipBoardIcon.addEventListener("click",function(){
        console.log('Board orientation is: ' + chess_game.board.orientation());
        chess_game.board.flip();
    });



    function getActiveElementIndex() {
        
    }
})







