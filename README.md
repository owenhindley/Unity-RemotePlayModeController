This is a small utility I made for controlling the playmode of several Unity projects across several machines (or the same machine) simultanously; for example if you're creating a multiplayer game, or have some sort of client/server arrangement. 

It calls the server every second (you can edit this value in the Editor Window script) to check whether the global Play Mode is enabled. You can also change play mode from any editor that's connected.

To use:
1) Plonk the Editor Window script inside an Editor/* folder in your Unity project
2) Install the node packages (type `npm install` in the terminal of your choice)
3) Run the node server (type `node index.js` in the terminal of your choice)

Hope you might find this useful!