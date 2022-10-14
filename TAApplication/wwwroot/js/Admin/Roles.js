/**
  Author:    Nasser Mughrabi
  Partner:   None   
  Date:      13-October-2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Nasser Mughrabi - This work may not be copied for use in Academic Coursework.
  I, Nasser Mughrabi, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source. Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This class is AJAX to connect view to controller
 */

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