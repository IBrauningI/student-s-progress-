"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ratingNotificationHub").build();

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveNotification", function (rating) {
    document.getElementById("subject-span").innerHTML = rating.subject;
    document.getElementById("semestr-span").innerHTML = rating.semestrPoints;
    document.getElementById("sum-span").innerHTML = rating.sumPoints;

    $(document).ready(function () {
        $('.toast').toast('show');
    });
});

if (document.getElementById("edit-rating")) {

    document.getElementById("edit-rating").addEventListener("click", function (event) {
        var user = document.getElementById("user").value;
        var rating = {
            SemestrPoints: parseInt(document.getElementById("semestr-points").value),
            SumPoints: parseInt(document.getElementById("sum-points").value),
            Subject: document.getElementById("subject-name").value
        };

        connection.invoke("SendNotification", user, rating).catch(function (err) {
            return console.error(err.toString());
        });
    });
}