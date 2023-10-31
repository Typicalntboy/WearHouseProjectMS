document.addEventListener("DOMContentLoaded", function(){
    getCtgData();
})

$("#boxID").hide();
$("#boxID2").hide();
$("#boxID3").hide();

function ctgAdd() {
    $("#boxID").show();
}

var CatId = "";
var newCtg = "";

function ctgEdit(index) {
    $("#boxID2").show();
    localStorage.setItem("msCtgID", $("#Categoryid" + index).text());
}

function ctgDlt(index) {
    $("#boxID3").show();
    localStorage.setItem("msCtgID", $("#Categoryid" + index).text());
}


function tombolClose() {
    $("#boxID").hide();
    $("#boxID2").hide();
    $("#boxID3").hide();
    localStorage.removeItem("CatId");
    localStorage.removeItem("newCtg");
}

function getCtgData(){
    $.ajax({
        type: "get",
        url: "https://localhost:7013/api/MsCategory/GetCategory",
        contentType: "application/json",
        data: {
            UserId: localStorage.getItem("msUserID")
        },
        success: function(response) {
            // Tangani respons dari server
            console.log(response, "masuk kah?");

            // Hapus semua baris yang ada di tabel
            var tabel = $(".tabel");
            tabel.find("tr:gt(0)").remove();

            // Loop melalui data dari respons dan tambahkan setiap entri ke tabel
            $.each(response, function (index, item) {
                var newRow = $("<tr></tr>");
                newRow.append('<td>' + (index + 1) + '.</td>');
                newRow.append('<td id="Category' + (index + 1) + '">' + item.msCtg + '</td>');
                newRow.append('<td style="display: none;" id="Categoryid' + (index + 1) + '">' + item.msCtgID + '</td>');
                newRow.append('<td><button id="editbtn" onclick="ctgEdit(' + (index + 1) + ')">Edit</button><button id="dltbtn" onclick="ctgDlt(' + (index + 1) + ')">Delete</button></td>');
                tabel.append(newRow);
            });
            $("#boxID").hide();
        },
        error: function(error) {
            alert(error);
        }
    });
}

function addCategory(){
    console.log('Adding category');
    console.log(localStorage.getItem('msUserID'));
    $.ajax({
        type: "post",
        url: "https://localhost:7013/api/MsCategory/AddCategory",
        contentType: "application/json",
        data: JSON.stringify({
            msUserID: localStorage.getItem('msUserID'),
            msCtg: $("#boxInp").val(),
        }),
        contentType: "application/json",
        success: function() {
            getCtgData();
        },
        error: function(error) {
            console.error(error);
        }
    }); 
}

function editCategory(){
    $.ajax({
        type: "post",
        url: "https://localhost:7013/api/MsCategory/EditCategory",
        contentType: "application/json",
        data: JSON.stringify({
            msUserID: localStorage.getItem('msUserID'),
            msCtgID: localStorage.getItem('msCtgID'),
            msCtg: $("#boxInp2").val(),
        }),
        contentType: "application/json",
        success: function() {
            getCtgData();
            location.reload();
        },
        error: function(error) {
            console.error(error);
        }
    }); 
}


function deleteCategory(){
    $.ajax({
        type: "POST",
        url: "https://localhost:7013/api/MsCategory/DeleteCategory",
        contentType: "application/json",
        data: JSON.stringify({
            msUserID: localStorage.getItem('msUserID'),
            msCtgID: localStorage.getItem('msCtgID')
        }),
        success: function() {
            localStorage.removeItem("msCtgID");
            getCtgData();
            location.reload();
        },
        error: function(){
            console.log("Failed");
        }
    });
}

