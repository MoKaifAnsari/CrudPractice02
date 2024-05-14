$(document).ready(function () {
    ShowData();
    $("#BtnSave").click(function () {
        DataInsert();
    });
});
function DataInsert() {
    try {
        $.post("/Ragistation/InsertUpdateFormData",
            {
                id: $("#id").val(),
                fname: $("#fname").val(),
                lname: $("#lname").val(),
                email: $("#email").val(),
                number: $("#number").val()
            },
            function (data) {
                if (data.Status == "1" || data.Status== "2") {
                    alert(data.Message);
                    ShowData();
                    claere();
                } else {
                    alert(data.Message);
                }
            }
        )
    } catch {
        alert("Error in Comtroller " + e.Message);
    }
}
function claere() {
    $("#id").val("0");
    $("#fname").val("");
    $("#lname").val("");
    $("#email").val("");
    $("#number").val("");
}
function ShowData() {
    try {
        $.post("/Ragistation/ShowFormData", {},
            function (data) {
                if (data.Message !== "") {
                    alert(data.Message);
                }
                if (data.Grid !== "") {
                    $("#dvGrid").html(data.Grid);
                }
            }
        );
    } catch (e) {
        alert("Error in ShowStateMaster: " + e.message)
    }
}
function DeleteData(id) {
    try {
        $.post("/Ragistation/DeleteFormData",
            { id: id },
            function (data) {
                alert(data.Message);
                ShowData();
            },
        );
    } catch (e) {
        alert("Error in Controller: " + e.message)
    }
}
function EditData(id) {
    try {
        $.post("/Ragistation/EditFormData",
            { id: id },
            function (data) {
                if (data.Message != "") {
                    alert(data.Message);
                }
                if (data.Status == "1") {
                    $("#id").val(data.id);
                    $("#fname").val(data.fname);
                    $("#lname").val(data.lname);
                    $("#email").val(data.email);
                    $("#number").val(data.number);
                }
            }
        );
    } catch (e) {
        alert("Error in Controller: " + e.message)
    }
}