const express = require('express');

const port = 88;

var playMode = false;

const playModeServer = express();

playModeServer.get("/stateJSON", (req, res)=>{
    res.statusCode = 200;
    
    responseObj = {
        'playMode' : playMode
    };

    res.end(JSON.stringify(responseObj));
});

playModeServer.get("/state", (req, res)=>{
    res.statusCode = 200;   

    res.end(playMode ? "1" : "0");
});

playModeServer.get("/start", (req, res)=>{
    console.log("started playmode")
    playMode = true;
    res.statusCode = 200;
    res.end();
});

playModeServer.get("/stop", (req, res)=>{
    console.log("stopped playmode")
    playMode = false;
    res.statusCode = 200;
    res.end();
});

playModeServer.get("/control", (req, res)=>{
    res.statusCode = 200;
    res.sendFile("index.html", { root : __dirname });
});

playModeServer.listen(port, ()=>{
    console.log("Playmode server running on port " + port);
});