
function runLogin(){
    // Membuat nilai masing-masing variable
    var userEmail = $("#emailinput").val();
    var userPassword = $("#passwordinput").val();
    // Membuat validasi agar data yang diperlukan harus lengkap
    if(userEmail == "" || userPassword == ""){
        alert('Mohon Lengkapi Data');
        return;
    }

    // Membuat sambungan ke Back-end system
    $.ajax({
        type: 'post',
        url: 'https://localhost:7013/api/MsUser/LoginUser',
        contentType: 'application/json',
        data: JSON.stringify({
            msEmail: userEmail,
            msPassword: userPassword
        }),
        success: function(data) {
            console.log(data, "Test Ini apa");
            if(data != null ){
                localStorage.setItem('msUserID', data.msUserID);
                localStorage.setItem('msName', data.msName);
                window.location.href = '../MainPage/home.html';
                return;
            }else { 
                alert("Not Found");
            }
        },
        error: function() {
            alert("Error");
        }
    });
}