
var connection = new signalR.HubConnectionBuilder().withUrl("/roomHub").build();

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});






document.addEventListener('DOMContentLoaded', () => {
    document.getElementById("enterName").addEventListener("click", enterName);
    document.getElementById("enterRoom").addEventListener("click", enterRoom);

    
    connection.on("ReceiveConnID", function (room) {
        console.log("Welcome");
    })
    
    connection.on("ReceiveCommunication1", function (room) {
        console.log(room);
    })
    connection.on("TestMessage", function (room) {
        console.log(room);
    })
    
})


function enterName(){
    const userName = document.getElementById("playerName").value;
    document.getElementById("playerContent").innerHTML = userName;
    // document.getElementById("nameForm").style = "display: none";

    connection.invoke("SendCommunication1", "aa").catch(function (err) {
        return console.error(err.toString());
    })
}

function enterRoom(){
    const roomName = document.getElementById("roomName").value;
    document.getElementById("roomContent").innerHTML = roomName;
    // document.getElementById("nameForm").style = "display: none";

    connection.invoke("ConnectToRoom", roomName).catch(function (err) {
        return console.error(err.toString());
    })
}
