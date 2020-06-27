$(document).ready(function () {
    if (document.getElementById("LogOut") != null) {
        document.getElementById("LogOut").onclick = function () {
            document.getElementById("logoutForm").submit();
        }
    }
});