$("#dashHomeLogin").hide(); 
document.addEventListener("DOMContentLoaded", function () {
 console.log("dash");
    if (localStorage.getItem('msUserID') == null) {
        if (window.location.href.indexOf('home.html') === -1 && window.location.href.indexOf('login.html') === -1 && window.location.href.indexOf('../Component/Register/Register.html') === -1) {
            window.location.href = '../MainPage/home.html';
        }
        $("#dashHomeLogin").hide();
    } else {
        $("#userLog").text(localStorage.getItem('msName'));
        $(".loginRegisterOut").hide();
        $("#dashHomeLogin").show();
        $("#dropDown").show();
        $("#userLog").show();
    }
});
 
 $("#triangle").on("change", function (){
     if($(this).is(":checked")){
         $('#logOut').show();
     }else { 
         $('#logOut').hide();
     }
 })

function runLogOut(){
    $('#userLog').text(null);
    $('.loginRegisterOut').show();
    $("#dashHomeLogin").hide();
    $('#dropDown').hide();
    $('#userLog').hide();
    $('#logOut').hide();
    localStorage.removeItem('msUserID');
    localStorage.removeItem ('msName');
    location.reload();
}