function getusers() {
    $.get({
        url: "/Admin/GetUsers",
        data: {
            Name: Name,
            Email: Email
        }
    })
    .done(function (data) {
        console.log("Sent change role\n", data);
    });
}