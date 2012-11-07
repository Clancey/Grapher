var WebSocketServer = require('websocket').server;
var http = require('http');

var server = http.createServer(function(request, response) {
    // process HTTP request. Since we're writing just WebSockets server
    // we don't have to implement anything.
});
server.listen(8080, function() { });

// create the server
wsServer = new WebSocketServer({
    httpServer: server
});

console.log("...");
console.log("...");
console.log("...");
console.log("Starting WebSocket Service...");

// WebSocket server
wsServer.on('request', function(request) {
    var connection = request.accept(null, request.origin);

    // This is the most important callback for us, we'll handle
    // all messages from users here.
    connection.on('message', function(message) {
        if (message.type === 'utf8') {
        	
            console.log(message);
            connection.send('hello');
        }

    });

    setInterval(function() { 
    	var now = new Date(); 
    	var now_utc = new Date(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate(),  now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds());

    	var o2 = {
    		SourceId: 1,
    		Name: "O2",
    		Value: Math.random(),
    		TimeStamp: now_utc
    	};

    	connection.send(JSON.stringify(o2));
    	
    }, 1000);



    setInterval(function() { 
    	var now = new Date(); 
    	var now_utc = new Date(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate(),  now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds());

    	var co2 = {
    		SourceId: 1,
    		Name: "CO2",
    		Value: Math.random(),
    		TimeStamp: now_utc
    	};

    	connection.send(JSON.stringify(co2));
    	
    }, 1000);


    connection.on('close', function(connection) {
        // close user connection
    });
});