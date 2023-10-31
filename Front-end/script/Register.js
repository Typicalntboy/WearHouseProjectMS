function runRegister() {
    // Membuat nilai masing-masing variable
    // console.log($("#uname").val(), "ada gako");
    const userName = $("#uname").val();
    const userEmail = $("#uemail").val();
    const userPassword = $("#upassword").val();
    const userCPassword = $("#ucpassword").val();
    const confirmAcc = $("#uconfirmAcc").prop('checked');
    console.log($('#uconfirmAcc').val(), "uconfirmAcc");
    console.log("enter");
    console.log(userName, userEmail, userPassword, userCPassword , "ini apa");
    // Membuat kondisi agar semua input box wajib diisi
    if(userName == "" || userEmail == "" || userPassword == "" || userCPassword == ""){
        alert('Data harus di isi!')
        return;
    }

    // Membuat Kondisi agar Email yang dipakai sesuai dengan ketetuan pada validateEmail
    if (!validateEmail(userEmail)) {
        alert('Format email tidak valid!');
        return;
    }

    // Membuat Kondisi agar Password yang dibuat sesuai dengan ketetuan pada validatePassword
    if (!validatePassword(userPassword, userCPassword)) {
        alert('Password harus terdiri dari angka, symbol, huruf besar, huruf kecil, dan minimal 8 karakter!');
        return;
    }

    // Membuat Validasi agar nilai CPassword sama dengan Password yang dibuat
    if(userCPassword !== userPassword){
        alert('Konfirmasi password salah');
        return;
    }

    // Membuat validasi agar check box terms and conditions wajib dicentang
    if (!confirmAcc) {
        alert("Anda harus menyetujui syarat dan ketentuan.");
        return
    }

    // membuat sambungan ke Back-end system
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7013/api/MsUser/AddUser',
        contentType: 'application/json',
        data: JSON.stringify({
            MsEmail: userEmail,
            MsName: userName,
            MsPassword: userPassword
        }),
        success: function() {
            window.location.href = "../Login/login.html";
        },
        error: function() {
            console.log("kosong");
        }
    });
}

// Fungsi Validasi Email
function validateEmail(email) {
    // Disesuaikan dengan standar keamanan email agar tidak dapat dibruteforce dengan cara SQL Injection Command
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

// Fungsi validasi password
function validatePassword(password) {
    // Sesuaikan persyaratan sesuai kebutuhan Anda
    const passwordRegex = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
    return passwordRegex.test(password);
}