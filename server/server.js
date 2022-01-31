//this server is 100% useless, but I added it in because why not
var express = require("express");
var path = require("path");
var app = express();

app.use(express.static("public"));

app.get("/", function (req, res) {
    res.sendFile(path.join(__dirname + "/index.html"));
});
app.get("/serverstat", function (req, res) {
    res.sendStatus(200)
});
app.get("/serverofflinetest", function (req, res) {
    res.sendStatus(500)
});
app.get("/sitenotfoundtest", function (req, res) {
    res.sendStatus(404)
    throw new Error("404 Not found")
});
app.get("/testfile", function (req, res) {
    if (err) {
        throw new Error("Broken")
    } else {
        res.send("testresponse")
    }
});
app.get("/api/versioncheck/latestversion", function (req, res) {
    res.send("0.3 alpha")
    console.log("Retrieved versioninfo (/api/versioncheck/latestversion)")
});
var server = app.listen(4000, function () {
    var host = server.address().address;
    var port = server.address().port;

    console.log("Started listening on port", port);
});
