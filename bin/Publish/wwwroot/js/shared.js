$(document).ready(function (e) {
    $('#createNewModal').on('hidden.bs.modal', function (e) {
        $(this)
            .find("input")
            .val('')
            .end()
            .find("select")
            .val($("#Dept option:first").val())
            .end();
    })

    $(".btnSave").click(function (event) {
        var salary = $("#Salary").val();
        if (salary != "") {
            if (salary <= 0) {
                alert("Invalid Salary");
                event.preventDefault();
            }
        }
    });
});

