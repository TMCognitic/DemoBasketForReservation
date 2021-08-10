// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function AddGuest() {
    let form = new FormData();
    form.append('lastname', $('#lastname').val());
    form.append('firstname', $('#firstname').val());
    $.ajax({
        url: "/Home/AddGuest",
        type: "POST",
        data: form,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            $('#GuestDiv').html(data);
            $('#lastname').val("");
            $('#firstname').val("");
        },
        error: function (data) {
            $('#AjaxErrorDiv').show();
            $('#AjaxErrorDiv').html("<p class=\"alert-danger\">Bad request (" + data.responseText + ")...</p>");
        }
    });
}

function RemoveGuest(index) {
    $.ajax({
        url: "/Home/RemoveGuest/" + index,
        dataType: "html",
        type: "get",
        success: function (data) {
            $('#GuestDiv').html(data);
        },
        error: function (data) {
            $('#AjaxErrorDiv').show();
            $('#AjaxErrorDiv').html("<p class=\"alert-danger\">Bad request (" + data.responseText + ")...</p>");
        }
    });
}

