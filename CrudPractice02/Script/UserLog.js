$(document).ready(function () {
    $("#btnsave").click(function () {
        UserLogin();
    });
});
function UserLogin() {
    try {
        $.post("/Ragistation/InsertLoginPage", {
            UserName: $("#username").val(),
            PassWord: $("#password").val()
        },
            function (data) {
                if (data.Status == "1") {
                    alert(data.Message);
                    window.location.href = "/Ragistation/Form/";
                } else {
                    alert(data.Message);
                }
            }
        )
    } catch {
        alert("Error in Controller : " + e.message);
    }
}