function changerole(id, role) {
    $.post({
        url: "/Admin/changeRole",
        data: {
            id: id,
            role: role
        }
    })
        .done(function (data) {
            console.log("Sent change role\n", data);
        });
}