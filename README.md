# Unity Remote Play Mode Controller

This is a small utility I made for controlling the playmode of several Unity projects across several machines (or the same machine) simultanously; for example if you're creating a multiplayer game, or have some sort of client/server arrangement. 

It calls the server every second (you can edit this value in the Editor Window script) to check whether the global Play Mode is enabled. You can also change play mode from any editor that's connected.

### To use:
1) Plonk the Editor Window script inside an Editor/* folder in your Unity project
2) Install the node packages (type `npm install` in the terminal of your choice)
3) Run the node server (type `node index.js` in the terminal of your choice)
4) Open Editor Window in Unity (Window -> RemotePlayModeControl), fill in port (default 88) & IP (run `ipconfig` on Windows, `ifconfig` on OSX to find yours).
5) Click Toggle Play Mode to toggle play mode on all connected editors (that have the window open).

If you close the window, that editor will stop listening to the server.
If you enter Play Mode via the normal button and the server is set to 'Stop', the editor will stop playing after a second (in case this catches anyone out).

Hope you might find this useful!
