var express = require('express');
var http = require('http');
var socketIO = require('socket.io');

var app = express();
var server = http.Server(app);
server.listen(5000);

var io = socketIO(server);
var randomNum = Math.floor(Math.random() * (99 - 0)) + 0;

io.on('connection', function (socket) {
    console.log('client connected');
    console.log("number = " + randomNum);

    socket.emit('GameStarted');

    socket.on('Send', function (data) {
        console.log(data.Name + " : " + data.Value );

        if (data.Value > randomNum) {
            socket.emit('TooMuch', { status: 'Too Much' });
        }

        if (data.Value < randomNum) {
            socket.emit('TooLess', { status: 'Too Less' });
        }

        if (data.Value == randomNum)
        {
            socket.broadcast.emit('win', { name: data.Name });
            socket.emit('win', { name: data.Name });

            randomNum = Math.floor(Math.random() * (99 - 0)) + 0;
            console.log("new number "+ randomNum);

        }

    });

});

console.log('server started');