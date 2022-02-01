//this server is 100% useless, but I added it in because why not
var express = require("express");
var app = express();

app.use(express.static("public"));

app.get("/", function (req, res) {
    res.send("NotepadV2_by_Jurij15 API")
});
app.get("/serverstat", function (req, res) {
    res.sendStatus(200)
});
app.get("/serverofflinetest", function (req, res) {
    res.sendStatus(500)
});
app.get("/api/versioncheck/latestversion", function (req, res) {
    res.send("0.4 alpha")
    console.log("Retrieved versioninfo (/api/versioncheck/latestversion)")
});
app.listen(process.env.PORT || 3000,
    () => console.log("Server is now listening"));

