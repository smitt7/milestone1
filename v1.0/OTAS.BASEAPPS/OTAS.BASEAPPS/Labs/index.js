$(function () {
    var endPoint = "../API/api/values",
        settings = {
            url: endPoint,
            dataType: "json"
        };

    $.ajax (settings).done (function (data) {
        $("#values").html (data.join (" "));
    }).fail (function (jqXHR, textStatus) {
        var message = jqXHR.statusText;

        if (jqXHR.responseJSON && jqXHR.responseJSON.message)
            message = jqXHR.responseJSON.message;

        $("#values").html ("Request failed: " + message );
    });
});